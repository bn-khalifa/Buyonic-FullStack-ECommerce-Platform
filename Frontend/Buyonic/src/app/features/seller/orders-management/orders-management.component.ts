import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { switchMap } from 'rxjs';
import { OrderService } from '../../../core/services/order.service';
import { SellerService } from '../../../core/services/seller.service';
import { OrderDTO } from '../../../core/models/models';

@Component({
  selector: 'app-orders-management',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './orders-management.component.html',
  styleUrls: ['./orders-management.component.scss']
})
export class OrdersManagementComponent implements OnInit {
  private orderService = inject(OrderService);
  private sellerService = inject(SellerService);

  orders: OrderDTO[] = [];
  loading = true;
  toast = '';
  updatingOrderId: number | null = null;

  readonly statuses = ['Pending', 'Processing', 'Shipped', 'Delivered', 'Cancelled'];

  ngOnInit(): void {
    this.sellerService.getMe().pipe(
      switchMap(seller => this.orderService.getBySeller(seller.id))
    ).subscribe({
      next: (orders) => {
        this.orders = orders;
        this.loading = false;
      },
      error: () => { this.loading = false; }
    });
  }

  updateStatus(orderId: number, status: string): void {
    this.updatingOrderId = orderId;
    this.orderService.updateStatus(orderId, status).subscribe({
      next: (updated) => {
        this.updatingOrderId = null;
        const idx = this.orders.findIndex(o => o.id === orderId);
        if (idx !== -1) this.orders[idx] = updated;
        this.showToast(`Order #${orderId} updated to ${status}.`);
      },
      error: () => {
        this.updatingOrderId = null;
        this.showToast('Failed to update order status.');
      }
    });
  }

  showToast(msg: string): void {
    this.toast = msg;
    setTimeout(() => this.toast = '', 3000);
  }

  getStatusClass(status: string): string {
    switch (status?.toLowerCase()) {
      case 'delivered': return 'badge-success';
      case 'pending': return 'badge-warning';
      case 'cancelled': return 'badge-danger';
      case 'shipped': return 'badge-info';
      default: return 'badge-primary';
    }
  }
}
