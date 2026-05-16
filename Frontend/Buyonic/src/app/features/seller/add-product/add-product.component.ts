import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { switchMap } from 'rxjs';
import { ProductService } from '../../../core/services/product.service';
import { CategoryService } from '../../../core/services/category.service';
import { SellerService } from '../../../core/services/seller.service';
import { CategoryDTO, CreateProductDTO, UpdateProductDTO } from '../../../core/models/models';

@Component({
  selector: 'app-add-product',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.scss']
})
export class AddProductComponent implements OnInit {
  private fb = inject(FormBuilder);
  private productService = inject(ProductService);
  private categoryService = inject(CategoryService);
  private sellerService = inject(SellerService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  form!: FormGroup;
  categories: CategoryDTO[] = [];
  loading = false;
  loadingProduct = false;
  errorMessage = '';
  successMessage = '';
  isEditMode = false;
  productId: number | null = null;

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

    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.isEditMode = true;
      this.productId = Number(idParam);
      this.loadProductForEdit(this.productId);
    }
  }

  get f() { return this.form.controls; }

  private loadProductForEdit(id: number): void {
    this.loadingProduct = true;
    this.productService.getById(id).subscribe({
      next: (product) => {
        this.form.patchValue({
          name: product.name,
          description: product.description,
          imageUrl: product.imageUrl ?? '',
          price: product.price,
          discount: product.discount,
          stockQuantity: product.stockQuantity,
          categoryId: product.categoryId,
          sellerId: product.sellerId
        });
        this.loadingProduct = false;
      },
      error: () => {
        this.loadingProduct = false;
        this.errorMessage = 'Product not found or you do not have access.';
      }
    });
  }

  onSubmit(): void {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    this.loading = true;
    this.errorMessage = '';

    const raw = this.form.getRawValue() as Record<string, unknown>;
    const { sellerId: _sid, ...rest } = raw;

    if (this.isEditMode && this.productId != null) {
      const payload: UpdateProductDTO = {
        name: rest['name'] as string,
        description: rest['description'] as string,
        imageUrl: (rest['imageUrl'] as string) || undefined,
        price: rest['price'] as number,
        discount: rest['discount'] as number,
        stockQuantity: rest['stockQuantity'] as number,
        categoryId: rest['categoryId'] as number
      };

      this.productService.update(this.productId, payload).subscribe({
        next: () => {
          this.loading = false;
          this.successMessage = 'Product updated successfully! ✅';
          setTimeout(() => this.router.navigate(['/seller/my-products']), 1500);
        },
        error: (err) => {
          this.loading = false;
          this.errorMessage = err.error?.message || err.error?.title || err.error || 'Failed to update product.';
        }
      });
      return;
    }

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
