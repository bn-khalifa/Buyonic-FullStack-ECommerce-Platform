import { Routes } from '@angular/router';

export const sellerRoutes: Routes = [
  {
    path: '',
    loadComponent: () => import('./layout/seller-layout.component').then(m => m.SellerLayoutComponent),
    children: [
      { path: '', loadComponent: () => import('./dashboard/seller-dashboard.component').then(m => m.SellerDashboardComponent) },
      { path: 'my-products', loadComponent: () => import('./my-products/my-products.component').then(m => m.MyProductsComponent) },
      { path: 'add-product', loadComponent: () => import('./add-product/add-product.component').then(m => m.AddProductComponent) },
      { path: 'edit-product/:id', loadComponent: () => import('./add-product/add-product.component').then(m => m.AddProductComponent) },
      { path: 'orders', loadComponent: () => import('./orders-management/orders-management.component').then(m => m.OrdersManagementComponent) },
      { path: 'profile', loadComponent: () => import('./profile/seller-profile.component').then(m => m.SellerProfileComponent) },
    ]
  }
];
