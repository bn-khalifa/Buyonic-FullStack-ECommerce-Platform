import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { switchMap } from 'rxjs';
import { OrderService } from '../../../core/services/order.service';
import { CustomerService } from '../../../core/services/customer.service';
import { OrderDTO } from '../../../core/models/models';

@Component({
  selector: 'app-customer-orders',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './customer-orders.component.html',
  styleUrls: ['./customer-orders.component.scss']
})
export class CustomerOrdersComponent implements OnInit {
  private orderService = inject(OrderService);
  private customerService = inject(CustomerService);

  orders: OrderDTO[] = [];
  loading = true;
  error = '';
  expandedOrder: number | null = null;

  ngOnInit(): void {
    this.customerService.getMe().pipe(
      switchMap(c => this.orderService.getByCustomer(c.id))
    ).subscribe({
      next: (data) => { this.orders = data; this.loading = false; },
      error: (err) => {
        this.loading = false;
        this.error = err.status === 404 ? 'No orders found.' : 'Failed to load orders.';
      }
    });
  }

  toggleExpand(orderId: number): void {
    this.expandedOrder = this.expandedOrder === orderId ? null : orderId;
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

  getStatusIcon(status: string): string {
    switch (status?.toLowerCase()) {
      case 'delivered': return '✅';
      case 'pending': return '⏳';
      case 'cancelled': return '❌';
      case 'shipped': return '🚚';
      case 'processing': return '⚙️';
      default: return '📦';
    }
  }
}
