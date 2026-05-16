import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProductService } from '../../../core/services/product.service';
import { CategoryService } from '../../../core/services/category.service';
import { CartService } from '../../../core/services/cart.service';
import { WishlistService } from '../../../core/services/wishlist.service';
import { ProductDTO, CategoryDTO } from '../../../core/models/models';
import { apiErrorMessage } from '../../../core/utils/api-error';

@Component({
  selector: 'app-browse-products',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './browse-products.component.html',
  styleUrls: ['./browse-products.component.scss']
})
export class BrowseProductsComponent implements OnInit {
  private productService = inject(ProductService);
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

  getStars(rating?: number): string[] {
    const r = Math.round(rating ?? 0);
    return Array.from({ length: 5 }, (_, i) => i < r ? '★' : '☆');
  }
}
