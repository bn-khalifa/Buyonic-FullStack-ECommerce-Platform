import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { AdminService } from '../../../core/services/admin.service';

function passwordMatch(control: AbstractControl) {
  const pw = control.get('password')?.value;
  const cpw = control.get('confirmPassword')?.value;
  return pw === cpw ? null : { mismatch: true };
}

@Component({
  selector: 'app-admin-add-admin',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './admin-add-admin.component.html',
  styleUrls: ['./admin-add-admin.component.scss']
})
export class AdminAddAdminComponent {
  private fb = inject(FormBuilder);
  private adminApi = inject(AdminService);

  form: FormGroup = this.fb.group({
    firstName: ['', [Validators.required, Validators.minLength(2)]],
    lastName: ['', [Validators.required, Validators.minLength(2)]],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
    confirmPassword: ['', Validators.required]
  }, { validators: passwordMatch });

  loading = false;
  errorMessage = '';
  successMessage = '';
  showPassword = false;

  get f() { return this.form.controls; }

  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    this.loading = true;
    this.errorMessage = '';
    this.successMessage = '';
    const v = this.form.value;
    this.adminApi.createAdmin({
      firstName: v.firstName,
      lastName: v.lastName,
      email: v.email,
      password: v.password,
      confirmPassword: v.confirmPassword,
      accountType: 'Admin'
    }).subscribe({
      next: (msg) => {
        this.loading = false;
        this.successMessage = msg || 'Admin created successfully.';
        this.form.reset();
      },
      error: (err) => {
        this.loading = false;
        if (err.status === 400 && Array.isArray(err.error)) {
          this.errorMessage = err.error.map((e: { description?: string }) => e.description).join(' ');
        } else if (err.status === 400) {
          this.errorMessage = typeof err.error === 'string' ? err.error : 'Request failed.';
        } else {
          this.errorMessage = 'Server error. Try again later.';
        }
      }
    });
  }
}
