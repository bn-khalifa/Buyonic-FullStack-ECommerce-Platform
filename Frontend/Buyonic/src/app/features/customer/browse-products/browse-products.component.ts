import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ProductService } from '../../../core/services/product.service';
import { CategoryService } from '../../../core/services/category.service';
import { CartService } from '../../../core/services/cart.service';
import { WishlistService } from '../../../core/services/wishlist.service';
import { ProductDTO, CategoryDTO } from '../../../core/models/models';
import { apiErrorMessage } from '../../../core/utils/api-error';
import { StarRatingComponent } from '../../shared/star-rating/star-rating.component';

@Component({
  selector: 'app-browse-products',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, StarRatingComponent],
  templateUrl: './browse-products.component.html',
  styleUrls: ['./browse-products.component.scss']
})
export class BrowseProductsComponent implements OnInit, OnDestroy {
  private productService = inject(ProductService);
  private route = inject(ActivatedRoute);
  private productUpdateSub?: Subscription;
  private categoryService = inject(CategoryService);
  private cartService = inject(CartService);
  private wishlistService = inject(WishlistService);

  products: ProductDTO[] = [];
  filtered: ProductDTO[] = [];
  categories: CategoryDTO[] = [];
  loading = true;
  toast = '';
  searchTerm = '';
  selectedCategory = 0;
  sortBy = 'name';

  ngOnInit(): void {
    this.applyCategoryFromQuery();

    this.productService.getAll().subscribe({
      next: (data) => {
        this.products = data;
        this.applyFilters();
        this.loading = false;
      },
      error: () => { this.loading = false; }
    });

    this.categoryService.getAll().subscribe({
      next: (cats) => { this.categories = cats; },
      error: () => {}
    });

    this.productUpdateSub = this.productService.productUpdated$.subscribe(updated =>
      this.patchProduct(updated)
    );

    this.route.queryParamMap.subscribe(() => this.applyCategoryFromQuery());
  }

  private applyCategoryFromQuery(): void {
    const categoryParam = Number(this.route.snapshot.queryParamMap.get('category'));
    if (!Number.isNaN(categoryParam) && categoryParam > 0) {
      this.selectedCategory = categoryParam;
      if (this.products.length) this.applyFilters();
    }
  }

  ngOnDestroy(): void {
    this.productUpdateSub?.unsubscribe();
  }

  private patchProduct(updated: ProductDTO): void {
    this.products = this.products.map(p => p.id === updated.id ? { ...p, ...updated } : p);
    this.applyFilters();
  }

  applyFilters(): void {
    let result = [...this.products];

    if (this.searchTerm.trim()) {
      const q = this.searchTerm.toLowerCase();
      result = result.filter(p => p.name.toLowerCase().includes(q) || p.description.toLowerCase().includes(q));
    }

    const categoryId = Number(this.selectedCategory);
    if (!Number.isNaN(categoryId) && categoryId > 0) {
      result = result.filter(p => Number(p.categoryId) === categoryId);
    }

    switch (this.sortBy) {
      case 'price-asc': result.sort((a, b) => a.price - b.price); break;
      case 'price-desc': result.sort((a, b) => b.price - a.price); break;
      case 'rating': result.sort((a, b) => (b.rating ?? 0) - (a.rating ?? 0)); break;
      default: result.sort((a, b) => a.name.localeCompare(b.name));
    }

    this.filtered = result;
  }

  addToCart(productId: number): void {
    this.cartService.addToCart(productId, 1).subscribe({
      next: () => this.showToast('Added to cart! 🛒'),
      error: (err) => this.showToast(apiErrorMessage(err, 'Failed to add to cart.'))
    });
  }

  addToWishlist(productId: number): void {
    this.wishlistService.addToWishlist({ productId }).subscribe({
      next: () => this.showToast('Added to wishlist! ❤️'),
      error: (err) => this.showToast(apiErrorMessage(err, 'Failed to add to wishlist.'))
    });
  }

  showToast(msg: string): void {
    this.toast = msg;
    setTimeout(() => this.toast = '', 3000);
  }

  getDiscountedPrice(p: ProductDTO): number {
    return p.price * (1 - p.discount / 100);
  }

}
