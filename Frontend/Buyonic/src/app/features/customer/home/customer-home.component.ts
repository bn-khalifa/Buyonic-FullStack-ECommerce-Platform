import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { ProductService } from '../../../core/services/product.service';
import { CategoryService } from '../../../core/services/category.service';
import { CartService } from '../../../core/services/cart.service';
import { CategoryDTO, ProductDTO } from '../../../core/models/models';
import { apiErrorMessage } from '../../../core/utils/api-error';
import { FooterComponent } from '../../shared/footer/footer.component';
import { StarRatingComponent } from '../../shared/star-rating/star-rating.component';

@Component({
  selector: 'app-customer-home',
  standalone: true,
  imports: [CommonModule, RouterLink, FooterComponent, StarRatingComponent],
  templateUrl: './customer-home.component.html',
  styleUrls: ['./customer-home.component.scss']
})
export class CustomerHomeComponent implements OnInit, OnDestroy {
  private auth = inject(AuthService);
  private productService = inject(ProductService);
  private categoryService = inject(CategoryService);
  private cartService = inject(CartService);
  private productUpdateSub?: Subscription;

  products: ProductDTO[] = [];
  featuredProducts: ProductDTO[] = [];
  categories: CategoryDTO[] = [];
  categoriesLoading = true;
  loading = true;
  cartMessage = '';

  get firstName() { return this.auth.getDecodedToken()?.given_name ?? 'Customer'; }

  ngOnInit(): void {
    this.loadProducts();
    this.loadCategories();
    this.productUpdateSub = this.productService.productUpdated$.subscribe(updated =>
      this.patchProduct(updated)
    );
  }

  private loadCategories(): void {
    this.categoryService.getAll().subscribe({
      next: (data) => {
        this.categories = data;
        this.categoriesLoading = false;
      },
      error: () => { this.categoriesLoading = false; }
    });
  }

  getCategoryIcon(name: string): string {
    const key = name.toLowerCase();
    if (key.includes('electronic') || key.includes('tech')) return '💻';
    if (key.includes('cloth') || key.includes('fashion') || key.includes('apparel')) return '👕';
    if (key.includes('home') || key.includes('furniture')) return '🏠';
    if (key.includes('book')) return '📚';
    if (key.includes('sport')) return '⚽';
    if (key.includes('beauty') || key.includes('health')) return '💄';
    if (key.includes('food') || key.includes('grocery')) return '🍎';
    return '🏷️';
  }

  ngOnDestroy(): void {
    this.productUpdateSub?.unsubscribe();
  }

  private loadProducts(): void {
    this.productService.getAll().subscribe({
      next: (data) => {
        this.products = data;
        this.featuredProducts = data.slice(0, 8);
        this.loading = false;
      },
      error: () => { this.loading = false; }
    });
  }

  private patchProduct(updated: ProductDTO): void {
    this.products = this.products.map(p => p.id === updated.id ? { ...p, ...updated } : p);
    this.featuredProducts = this.featuredProducts.map(p =>
      p.id === updated.id ? { ...p, ...updated } : p
    );
  }

  addToCart(productId: number): void {
    this.cartService.addToCart(productId, 1).subscribe({
      next: () => {
        this.cartMessage = 'Added to cart! 🛒';
        setTimeout(() => this.cartMessage = '', 2500);
      },
      error: (err) => {
        this.cartMessage = apiErrorMessage(err, 'Could not add to cart.');
        setTimeout(() => this.cartMessage = '', 3000);
      }
    });
  }

  getDiscountedPrice(product: ProductDTO): number {
    return product.price * (1 - product.discount / 100);
  }

}
