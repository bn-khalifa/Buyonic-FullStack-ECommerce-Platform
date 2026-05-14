import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { roleGuard } from './core/guards/role.guard';

export const routes: Routes = [
  { path: '', redirectTo: '/auth/login', pathMatch: 'full' },

  // Auth feature (public)
  {
    path: 'auth',
    loadChildren: () => import('./features/auth/auth.routes').then(m => m.authRoutes)
  },

  // Customer feature (protected)
  {
    path: 'customer',
    canActivate: [authGuard, roleGuard],
    data: { role: 'Customer' },
    loadChildren: () => import('./features/customer/customer.routes').then(m => m.customerRoutes)
  },

  // Seller feature (protected)
  {
    path: 'seller',
    canActivate: [authGuard, roleGuard],
    data: { role: 'Seller' },
    loadChildren: () => import('./features/seller/seller.routes').then(m => m.sellerRoutes)
  },

  // Shared components
  {
    path: 'unauthorized',
    loadComponent: () => import('./features/shared/unauthorized/unauthorized.component').then(m => m.UnauthorizedComponent)
  },

  // Fallback
  { path: '**', redirectTo: '/auth/login' }
];
