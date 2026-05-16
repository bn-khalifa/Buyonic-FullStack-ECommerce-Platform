import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { forkJoin } from 'rxjs';
import { CustomerService } from '../../../core/services/customer.service';
import { SellerService } from '../../../core/services/seller.service';
import { OrderService } from '../../../core/services/order.service';
import { AdminService } from '../../../core/services/admin.service';
import { CustomerDTO, SellerDTO, OrderDTO, UserLookupResultDTO } from '../../../core/models/models';

@Component({
  selector: 'app-admin-users',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-users.component.html',
  styleUrls: ['./admin-users.component.scss']
})
export class AdminUsersComponent implements OnInit {
  private customersApi = inject(CustomerService);
  private sellersApi = inject(SellerService);
  private ordersApi = inject(OrderService);
  private adminApi = inject(AdminService);

  customers: CustomerDTO[] = [];
  sellers: SellerDTO[] = [];
  loading = true;
  searchQuery = '';
  searching = false;
  toast = '';

  /** Lookup / row detail panel */
  panelOpen = false;
  panelLookup: UserLookupResultDTO | null = null;
  panelOrders: OrderDTO[] = [];
  panelOrdersLoading = false;

  ngOnInit(): void {
    this.reloadLists();
  }

  reloadLists(): void {
    this.loading = true;
    forkJoin({
      customers: this.customersApi.getAll(false),
      sellers: this.sellersApi.getAll()
    }).subscribe({
      next: ({ customers, sellers }) => {
        this.customers = customers as CustomerDTO[];
        this.sellers = sellers;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
        this.showToast('Failed to load users.');
      }
    });
  }

  runSearch(): void {
    const q = this.searchQuery.trim();
    if (!q) return;
    this.searching = true;
    this.adminApi.lookup(q).subscribe({
      next: (res) => {
        this.searching = false;
        this.openPanel(res);
      },
      error: (err) => {
        this.searching = false;
        this.showToast(err.status === 404 ? 'No user found for that search.' : 'Search failed.');
      }
    });
  }

  openPanelFromCustomer(c: CustomerDTO): void {
    this.adminApi.lookup(String(c.id)).subscribe({
      next: (res) => this.openPanel(res),
      error: () => this.showToast('Could not load user details.')
    });
  }

  openPanelFromSeller(s: SellerDTO): void {
    this.adminApi.lookup(String(s.id)).subscribe({
      next: (res) => this.openPanel(res),
      error: () => this.showToast('Could not load user details.')
    });
  }

  private openPanel(lookup: UserLookupResultDTO): void {
    this.panelLookup = lookup;
    this.panelOpen = true;
    this.panelOrders = [];
    this.panelOrdersLoading = true;
    if (lookup.customer) {
      this.ordersApi.getByCustomer(lookup.customer.id).subscribe({
        next: (o) => { this.panelOrders = o; this.panelOrdersLoading = false; },
        error: () => { this.panelOrdersLoading = false; }
      });
    } else if (lookup.seller) {
      this.ordersApi.getBySeller(lookup.seller.id).subscribe({
        next: (o) => { this.panelOrders = o; this.panelOrdersLoading = false; },
        error: () => { this.panelOrdersLoading = false; }
      });
    } else {
      this.panelOrdersLoading = false;
    }
  }

  closePanel(): void {
    this.panelOpen = false;
    this.panelLookup = null;
    this.panelOrders = [];
  }

  setActive(applicationUserId: number, active: boolean): void {
    this.adminApi.setUserActive(applicationUserId, { isActive: active }).subscribe({
      next: () => {
        this.showToast(active ? 'User activated.' : 'User deactivated.');
        this.reloadLists();
        if (this.panelLookup?.applicationUserId === applicationUserId) {
          this.panelLookup = {
            ...this.panelLookup,
            isActive: active,
            isDeleted: active ? false : this.panelLookup.isDeleted
          };
        }
      },
      error: (err) => {
        this.showToast(err.error?.message || err.error || 'Update failed.');
      }
    });
  }

  setActiveForCustomer(c: CustomerDTO, active: boolean): void {
    if (c.userId == null) return;
    this.setActive(c.userId, active);
  }

  deleteCustomer(c: CustomerDTO): void {
    if (!confirm(`Delete customer ${c.email}? This removes their customer profile and disables login.`)) return;
    this.customersApi.delete(c.id).subscribe({
      next: () => {
        this.showToast('Customer deleted.');
        this.closePanel();
        this.reloadLists();
      },
      error: () => this.showToast('Delete failed.')
    });
  }

  deleteSeller(s: SellerDTO): void {
    if (!confirm(`Delete seller "${s.storeName}"? This removes the seller profile.`)) return;
    this.sellersApi.delete(s.id).subscribe({
      next: () => {
        this.showToast('Seller profile deleted.');
        this.closePanel();
        this.reloadLists();
      },
      error: () => this.showToast('Delete failed.')
    });
  }

  private showToast(msg: string): void {
    this.toast = msg;
    setTimeout(() => (this.toast = ''), 4000);
  }
}
