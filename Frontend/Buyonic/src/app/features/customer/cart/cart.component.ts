import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { switchMap } from 'rxjs';
import { CartService } from '../../../core/services/cart.service';
import { CustomerService } from '../../../core/services/customer.service';
import { OrderService } from '../../../core/services/order.service';
import { CartDTO, CreateOrderDTO } from '../../../core/models/models';
import { apiErrorMessage } from '../../../core/utils/api-error';

/** Seeded payment methods (see DbInitializer): 1 Credit Card, 2 Cash on Delivery, 3 PayPal */
const DEFAULT_CHECKOUT_PAYMENT_METHOD_ID = 2;

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  private cartService = inject(CartService);
  private customerService = inject(CustomerService);
  private orderService = inject(OrderService);
  private router = inject(Router);

  cart: CartDTO | null = null;
  loading = true;
  error = '';
  toast = '';
  updatingItem: number | null = null;
  checkingOut = false;

  ngOnInit(): void {
    this.loadCart();
  }

  loadCart(): void {
    this.loading = true;
    this.cartService.getCart().subscribe({
      next: (raw) => {
        this.cart = this.normalizeCart(raw);
        this.loading = false;
      },
      error: (err) => {
        this.loading = false;
        this.error = err.status === 404 ? 'Your cart is empty.' : 'Failed to load cart.';
      }
    });
  }

  /** Ensure line items and totals are usable if JSON shape varies slightly. */
  private normalizeCart(raw: CartDTO): CartDTO {
    const items = raw.cartItems ?? [];
    const total =
      typeof raw.totalPrice === 'number' && !Number.isNaN(raw.totalPrice)
        ? raw.totalPrice
        : items.reduce((sum, i) => {
            const line = i.subTotal ?? i.productPrice * i.quantity;
            return sum + line;
          }, 0);
    return { ...raw, cartItems: items, totalPrice: total };
  }

  updateQuantity(productId: number, quantity: number): void {
    if (!this.cart || quantity < 1) return;
    this.updatingItem = productId;
    this.cartService.updateCartItem(productId, quantity).subscribe({
      next: () => { this.updatingItem = null; this.loadCart(); },
      error: () => { this.updatingItem = null; }
    });
  }

  removeItem(productId: number): void {
    if (!this.cart) return;
    this.cartService.removeFromCart(productId).subscribe({
      next: () => {
        this.showToast('Item removed from cart.');
        this.loadCart();
      },
      error: () => this.showToast('Failed to remove item.')
    });
  }

  clearCart(): void {
    if (!this.cart) return;
    this.cartService.clearCart().subscribe({
      next: () => {
        this.showToast('Cart cleared.');
        this.cart = null;
        this.error = 'Your cart is empty.';
      },
      error: () => this.showToast('Failed to clear cart.')
    });
  }

  checkout(): void {
    if (!this.cart?.cartItems?.length || this.checkingOut) return;

    this.checkingOut = true;
    this.customerService.getMe().pipe(
      switchMap(customer => {
        const address = (customer.address ?? '').trim() || 'Please add your shipping address in Profile.';
        const order: CreateOrderDTO = {
          customerId: customer.id,
          paymentMethodId: DEFAULT_CHECKOUT_PAYMENT_METHOD_ID,
          shippingAddress: address,
          orderItems: this.cart!.cartItems.map(i => ({
            productId: i.productId,
            quantity: i.quantity
          }))
        };
        return this.orderService.create(order).pipe(
          switchMap(() => this.cartService.clearCart())
        );
      })
    ).subscribe({
      next: () => {
        this.checkingOut = false;
        this.showToast('Order placed successfully!');
        this.router.navigate(['/customer/orders']);
      },
      error: (err) => {
        this.checkingOut = false;
        this.showToast(apiErrorMessage(err, 'Checkout failed. Please try again.'));
      }
    });
  }

  showToast(msg: string): void {
    this.toast = msg;
    setTimeout(() => this.toast = '', 3000);
  }
}
