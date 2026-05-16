import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { ProductService } from '../../../core/services/product.service';
import { CartService } from '../../../core/services/cart.service';
import { ProductDTO } from '../../../core/models/models';
import { apiErrorMessage } from '../../../core/utils/api-error';

@Component({
  selector: 'app-customer-home',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './customer-home.component.html',
  styleUrls: ['./customer-home.component.scss']
})
export class CustomerHomeComponent implements OnInit {
  private auth = inject(AuthService);
  private productService = inject(ProductService);
  private cartService = inject(CartService);

  products: ProductDTO[] = [];
  featuredProducts: ProductDTO[] = [];
  loading = true;
  cartMessage = '';

  get firstName() { return this.auth.getDecodedToken()?.given_name ?? 'Customer'; }

  ngOnInit(): void {
    this.productService.getAll().subscribe({
      next: (data) => {
        this.products = data;
        this.featuredProducts = data.slice(0, 8);
        this.loading = false;
      },
      error: () => { this.loading = false; }
    });
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

  getStars(rating: number | undefined): string[] {
    const r = Math.round(rating ?? 0);
    return Array.from({ length: 5 }, (_, i) => i < r ? '★' : '☆');
  }
}
