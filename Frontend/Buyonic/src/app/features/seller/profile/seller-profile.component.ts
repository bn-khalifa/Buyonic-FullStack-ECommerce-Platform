import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SellerService } from '../../../core/services/seller.service';
import { SellerDTO } from '../../../core/models/models';

@Component({
  selector: 'app-seller-profile',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './seller-profile.component.html',
  styleUrls: ['./seller-profile.component.scss']
})
export class SellerProfileComponent implements OnInit {
  private sellerService = inject(SellerService);
  private fb = inject(FormBuilder);

  profile: SellerDTO | null = null;
  private sellerNumericId: number | null = null;
  form!: FormGroup;
  loading = true;
  saving = false;
  toast = '';
  toastType = 'success';

  ngOnInit(): void {
    this.sellerService.getMe().subscribe({
      next: (data) => {
        this.profile = data;
        this.sellerNumericId = data.id;
        this.loading = false;
        this.form = this.fb.group({
          storeName: [data.storeName, [Validators.required, Validators.maxLength(100)]],
          rating: [data.rating ?? '', [Validators.min(0), Validators.max(5)]]
        });
      },
      error: () => { this.loading = false; }
    });
  }

  get f() { return this.form.controls; }

  onSave(): void {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    if (this.sellerNumericId == null) return;
    this.saving = true;
    const payload = {
      storeName: this.form.value.storeName,
      rating: this.form.value.rating !== '' ? Number(this.form.value.rating) : undefined
    };
    this.sellerService.update(this.sellerNumericId, payload).subscribe({
      next: () => {
        this.saving = false;
        this.showToast('Store updated! ✅', 'success');
        if (this.profile) this.profile.storeName = payload.storeName!;
      },
      error: (err) => {
        this.saving = false;
        this.showToast(err.error || 'Failed to update profile.', 'error');
      }
    });
  }

  showToast(msg: string, type: string): void {
    this.toast = msg;
    this.toastType = type;
    setTimeout(() => this.toast = '', 3500);
  }
}
