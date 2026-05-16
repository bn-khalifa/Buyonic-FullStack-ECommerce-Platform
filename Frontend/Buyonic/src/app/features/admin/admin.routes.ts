import { Routes } from '@angular/router';

export const adminRoutes: Routes = [
  {
    path: '',
    loadComponent: () => import('./layout/admin-layout.component').then(m => m.AdminLayoutComponent),
    children: [
      { path: '', pathMatch: 'full', redirectTo: 'dashboard' },
      {
        path: 'dashboard',
        loadComponent: () => import('./dashboard/admin-dashboard.component').then(m => m.AdminDashboardComponent)
      },
      {
        path: 'users',
        loadComponent: () => import('./users/admin-users.component').then(m => m.AdminUsersComponent)
      },
      {
        path: 'orders',
        loadComponent: () => import('./orders/admin-orders.component').then(m => m.AdminOrdersComponent)
      },
      {
        path: 'admins',
        loadComponent: () => import('./admins/admin-add-admin.component').then(m => m.AdminAddAdminComponent)
      }
    ]
  }
];
