import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { catchError, forkJoin, of, switchMap } from 'rxjs';
import { ProductService } from '../../../core/services/product.service';
import { CartService } from '../../../core/services/cart.service';
import { WishlistService } from '../../../core/services/wishlist.service';
import { ReviewService } from '../../../core/services/review.service';
import { CustomerService } from '../../../core/services/customer.service';
import { CategoryService } from '../../../core/services/category.service';
import {
  ProductDTO,
  CategoryDTO,
  ProductReviewDTO,
  ReviewEligibilityDTO,
  CreateReviewDTO
} from '../../../core/models/models';
import { apiErrorMessage } from '../../../core/utils/api-error';
import { StarRatingComponent } from '../../shared/star-rating/star-rating.component';

@Component({
  selector: 'app-product-detail',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, StarRatingComponent],
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss']
})
export class ProductDetailComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private productService = inject(ProductService);
  private cartService = inject(CartService);
  private wishlistService = inject(WishlistService);
  private reviewService = inject(ReviewService);
  private customerService = inject(CustomerService);
  private categoryService = inject(CategoryService);

  product: ProductDTO | null = null;
  categoryName = '';
  reviews: ProductReviewDTO[] = [];
  eligibility: ReviewEligibilityDTO | null = null;
  loading = true;
  toast = '';
  quantity = 1;

  reviewRating = 5;
  reviewComment = '';
  submittingReview = false;

  ngOnInit(): void {
    this.route.paramMap.pipe(
      switchMap(params => {
        const id = Number(params.get('id'));
        if (!id || Number.isNaN(id)) {
          throw new Error('Invalid product id');
        }
        return forkJoin({
          product: this.productService.getById(id),
          reviews: this.reviewService.getProductReviews(id),
          eligibility: this.reviewService.getReviewEligibility(id).pipe(
            catchError(() => of({ canReview: false, hasReviewed: false, message: undefined }))
          ),
          categories: this.categoryService.getAll()
        });
      })
    ).subscribe({
      next: ({ product, reviews, eligibility, categories }) => {
        this.product = product;
        this.reviews = reviews;
        this.eligibility = eligibility;
        const category = (categories as CategoryDTO[]).find(c => c.id === product.categoryId);
        this.categoryName = category?.name ?? '';
        this.quantity = product.stockQuantity > 0 ? 1 : 0;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
        this.product = null;
      }
    });
  }

  getDiscountedPrice(): number {
    if (!this.product) return 0;
    return this.product.price * (1 - this.product.discount / 100);
  }

  decreaseQuantity(): void {
    if (this.quantity > 1) this.quantity--;
  }

  increaseQuantity(): void {
    if (this.product && this.quantity < this.product.stockQuantity) {
      this.quantity++;
    }
  }

  addToCart(): void {
    if (!this.product || this.product.stockQuantity === 0) return;

    this.cartService.addToCart(this.product.id, this.quantity).subscribe({
      next: () => this.showToast('Added to cart! 🛒'),
      error: (err) => this.showToast(apiErrorMessage(err, 'Failed to add to cart.'))
    });
  }

  addToWishlist(): void {
    if (!this.product) return;

    this.wishlistService.addToWishlist({ productId: this.product.id }).subscribe({
      next: () => this.showToast('Added to wishlist! ❤️'),
      error: (err) => this.showToast(apiErrorMessage(err, 'Failed to add to wishlist.'))
    });
  }

  submitReview(): void {
    if (!this.product || !this.eligibility?.canReview || this.submittingReview) return;

    this.submittingReview = true;
    const productId = this.product.id;
    this.customerService.getMe().pipe(
      switchMap(customer => {
        const payload: CreateReviewDTO = {
          productId,
          customerId: customer.id,
          rating: this.reviewRating,
          comment: this.reviewComment.trim() || undefined
        };
        return this.reviewService.submitReview(payload);
      }),
      switchMap(updatedProduct => forkJoin({
        product: of(updatedProduct),
        reviews: this.reviewService.getProductReviews(productId),
        eligibility: this.reviewService.getReviewEligibility(productId).pipe(
          catchError(() => of({ canReview: false, hasReviewed: true, message: 'You have already reviewed this product.' }))
        )
      }))
    ).subscribe({
      next: ({ product, reviews, eligibility }) => {
        this.product = product;
        this.productService.notifyProductUpdated(product);
        this.reviews = reviews;
        this.eligibility = eligibility;
        this.reviewComment = '';
        this.reviewRating = 5;
        this.submittingReview = false;
        this.showToast('Review submitted! ⭐');
      },
      error: (err) => {
        this.submittingReview = false;
        this.showToast(apiErrorMessage(err, 'Could not submit review.'));
      }
    });
  }

  formatDate(date?: string): string {
    if (!date) return '';
    return new Date(date).toLocaleDateString(undefined, {
      year: 'numeric',
      month: 'short',
      day: 'numeric'
    });
  }

  showToast(msg: string): void {
    this.toast = msg;
    setTimeout(() => this.toast = '', 3000);
  }
}
