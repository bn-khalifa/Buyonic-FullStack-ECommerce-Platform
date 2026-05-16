import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WishlistService } from '../../../core/services/wishlist.service';
import { WishlistDTO } from '../../../core/models/models';
import { apiErrorMessage } from '../../../core/utils/api-error';

@Component({
  selector: 'app-wishlist',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './wishlist.component.html',
  styleUrls: ['./wishlist.component.scss']
})
export class WishlistComponent implements OnInit {
  private wishlistService = inject(WishlistService);

  wishlist: WishlistDTO | null = null;
  loading = true;
  error = '';
  toast = '';

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.loading = true;
    this.wishlistService.getWishlist().subscribe({
      next: (data) => { this.wishlist = data; this.loading = false; },
      error: (err) => {
        this.loading = false;
        this.error = err.status === 404 ? 'Your wishlist is empty.' : apiErrorMessage(err, 'Failed to load wishlist.');
      }
    });
  }

  remove(productId: number): void {
    this.wishlistService.removeFromWishlist(productId).subscribe({
      next: () => { this.showToast('Removed from wishlist.'); this.load(); },
      error: (err) => this.showToast(apiErrorMessage(err, 'Failed to remove item.'))
    });
  }

  clear(): void {
    this.wishlistService.clearWishlist().subscribe({
      next: () => {
        this.wishlist = null;
        this.error = 'Your wishlist is empty.';
        this.showToast('Wishlist cleared.');
      },
      error: (err) => this.showToast(apiErrorMessage(err, 'Failed to clear wishlist.'))
    });
  }

  showToast(msg: string): void {
    this.toast = msg;
    setTimeout(() => this.toast = '', 3000);
  }

  getStars(rating?: number): string[] {
    const r = Math.round(rating ?? 0);
    return Array.from({ length: 5 }, (_, i) => i < r ? '★' : '☆');
  }
}
