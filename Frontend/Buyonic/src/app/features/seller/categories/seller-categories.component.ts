import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { CategoryService } from '../../../core/services/category.service';
import { CategoryDTO, CreateCategoryDTO } from '../../../core/models/models';
import { apiErrorMessage } from '../../../core/utils/api-error';

@Component({
  selector: 'app-seller-categories',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './seller-categories.component.html',
  styleUrls: ['./seller-categories.component.scss']
})
export class SellerCategoriesComponent implements OnInit {
  private fb = inject(FormBuilder);
  private categoryService = inject(CategoryService);

  form!: FormGroup;
  categories: CategoryDTO[] = [];
  loading = true;
  submitting = false;
  errorMessage = '';
  successMessage = '';

  ngOnInit(): void {
    this.form = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', [Validators.required, Validators.maxLength(500)]]
    });
    this.loadCategories();
  }

  get f() { return this.form.controls; }

  loadCategories(): void {
    this.loading = true;
    this.categoryService.getAll().subscribe({
      next: (data) => {
        this.categories = data.sort((a, b) => a.name.localeCompare(b.name));
        this.loading = false;
      },
      error: () => { this.loading = false; }
    });
  }

  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.submitting = true;
    this.errorMessage = '';
    this.successMessage = '';

    const payload = this.form.value as CreateCategoryDTO;

    this.categoryService.create(payload).subscribe({
      next: (created) => {
        this.submitting = false;
        this.successMessage = `Category "${created.name}" added successfully!`;
        this.form.reset();
        this.loadCategories();
        setTimeout(() => this.successMessage = '', 3000);
      },
      error: (err) => {
        this.submitting = false;
        this.errorMessage = apiErrorMessage(err, 'Failed to add category.');
      }
    });
  }
}
