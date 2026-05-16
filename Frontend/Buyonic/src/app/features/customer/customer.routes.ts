import { Routes } from '@angular/router';

export const customerRoutes: Routes = [
  {
    path: '',
    loadComponent: () => import('./layout/customer-layout.component').then(m => m.CustomerLayoutComponent),
    children: [
      { path: '', loadComponent: () => import('./home/customer-home.component').then(m => m.CustomerHomeComponent) },
      { path: 'browse', loadComponent: () => import('./browse-products/browse-products.component').then(m => m.BrowseProductsComponent) },
      { path: 'product/:id', loadComponent: () => import('./product-detail/product-detail.component').then(m => m.ProductDetailComponent) },
      { path: 'cart', loadComponent: () => import('./cart/cart.component').then(m => m.CartComponent) },
      { path: 'wishlist', loadComponent: () => import('./wishlist/wishlist.component').then(m => m.WishlistComponent) },
      { path: 'orders', loadComponent: () => import('./orders/customer-orders.component').then(m => m.CustomerOrdersComponent) },
      { path: 'profile', loadComponent: () => import('./profile/customer-profile.component').then(m => m.CustomerProfileComponent) },
    ]
  }
];
