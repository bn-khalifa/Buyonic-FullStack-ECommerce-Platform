import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderService } from '../../../core/services/order.service';
import { OrderDTO } from '../../../core/models/models';

@Component({
  selector: 'app-admin-orders',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './admin-orders.component.html',
  styleUrls: ['./admin-orders.component.scss']
})
export class AdminOrdersComponent implements OnInit {
  private ordersApi = inject(OrderService);

  orders: OrderDTO[] = [];
  loading = true;
  error = '';
  expandedId: number | null = null;

  ngOnInit(): void {
    this.ordersApi.getAll().subscribe({
      next: (data) => {
        this.orders = data;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
        this.error = 'Could not load orders.';
      }
    });
  }

  toggle(id: number): void {
    this.expandedId = this.expandedId === id ? null : id;
  }

  statusClass(status: string): string {
    switch (status?.toLowerCase()) {
      case 'delivered': return 'badge-success';
      case 'pending': return 'badge-warning';
      case 'cancelled': return 'badge-danger';
      case 'shipped': return 'badge-info';
      default: return 'badge-primary';
    }
  }
}
