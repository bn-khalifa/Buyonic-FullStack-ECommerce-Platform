import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { switchMap } from 'rxjs';
import { ProductService } from '../../../core/services/product.service';
import { CategoryService } from '../../../core/services/category.service';
import { SellerService } from '../../../core/services/seller.service';
import { CategoryDTO, CreateProductDTO } from '../../../core/models/models';

@Component({
  selector: 'app-add-product',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.scss']
})
export class AddProductComponent implements OnInit {
  private fb = inject(FormBuilder);
  private productService = inject(ProductService);
  private categoryService = inject(CategoryService);
  private sellerService = inject(SellerService);
  private router = inject(Router);

  form!: FormGroup;
  categories: CategoryDTO[] = [];
  loading = false;
  errorMessage = '';
  successMessage = '';

  ngOnInit(): void {
    this.form = this.fb.group({
      name:          ['', [Validators.required, Validators.maxLength(200)]],
      description:   ['', [Validators.required, Validators.maxLength(2000)]],
      imageUrl:      [''],
      price:         [null, [Validators.required, Validators.min(1), Validators.max(1000000)]],
      discount:      [0,    [Validators.required, Validators.min(0), Validators.max(100)]],
      stockQuantity: [0,    [Validators.required, Validators.min(0)]],
      categoryId:    [null, Validators.required],
      sellerId:      [{ value: null as number | null, disabled: true }]
    });

    this.sellerService.getMe().subscribe({
      next: (s) => this.form.patchValue({ sellerId: s.id }),
      error: () => {}
    });

    this.categoryService.getAll().subscribe({
      next: (cats) => { this.categories = cats; },
      error: () => {}
    });
  }

  get f() { return this.form.controls; }

  onSubmit(): void {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    this.loading = true;
    this.errorMessage = '';

    const raw = this.form.getRawValue() as Record<string, unknown>;
    const { sellerId: _sid, ...rest } = raw;

    this.sellerService.getMe().pipe(
      switchMap(seller =>
        this.productService.create({ ...rest, sellerId: seller.id } as CreateProductDTO)
      )
    ).subscribe({
      next: () => {
        this.loading = false;
        this.successMessage = 'Product added successfully! 🎉';
        setTimeout(() => this.router.navigate(['/seller/my-products']), 1500);
      },
      error: (err) => {
        this.loading = false;
        this.errorMessage = err.error?.title || err.error || 'Failed to create product.';
      }
    });
  }
}
