import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomerService } from '../../../core/services/customer.service';
import { CustomerDTO } from '../../../core/models/models';
import { apiErrorMessage } from '../../../core/utils/api-error';

@Component({
  selector: 'app-customer-profile',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './customer-profile.component.html',
  styleUrls: ['./customer-profile.component.scss']
})
export class CustomerProfileComponent implements OnInit {
  private customerService = inject(CustomerService);
  private fb = inject(FormBuilder);

  profile: CustomerDTO | null = null;
  form!: FormGroup;
  loading = true;
  saving = false;
  error = '';
  toast = '';
  toastType = 'success';

  ngOnInit(): void {
    this.customerService.getMe().subscribe({
      next: (data) => {
        this.profile = data;
        this.loading = false;
        this.buildForm(data);
      },
      error: (err) => {
        this.loading = false;
        this.error = apiErrorMessage(err, 'Could not load your profile.');
      }
    });
  }

  buildForm(p: CustomerDTO): void {
    this.form = this.fb.group({
      id: [p.id],
      firstName: [p.firstName, [Validators.required, Validators.minLength(2)]],
      lastName:  [p.lastName,  [Validators.required, Validators.minLength(2)]],
      email:     [p.email,     [Validators.required, Validators.email]],
      address:   [p.address ?? ''],
      joinedAt:  [p.joinedAt],
      isActive:  [p.isActive]
    });
  }

  get f() { return this.form.controls; }

  onSave(): void {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    this.saving = true;
    this.customerService.update(this.form.value).subscribe({
      next: () => {
        this.saving = false;
        this.showToast('Profile updated successfully! ✅', 'success');
        if (this.profile) {
          const v = this.form.value;
          this.profile.firstName = v.firstName;
          this.profile.lastName = v.lastName;
          this.profile.email = v.email;
          this.profile.address = v.address;
        }
      },
      error: (err) => {
        this.saving = false;
        this.showToast(apiErrorMessage(err, 'Failed to update profile.'), 'error');
      }
    });
  }

  showToast(msg: string, type: string): void {
    this.toast = msg;
    this.toastType = type;
    setTimeout(() => this.toast = '', 3500);
  }
}
