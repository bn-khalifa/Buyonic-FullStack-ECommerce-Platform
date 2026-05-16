import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { switchMap } from 'rxjs';
import { ProductService } from '../../../core/services/product.service';
import { SellerService } from '../../../core/services/seller.service';
import { AuthService } from '../../../core/services/auth.service';
import { ProductDTO } from '../../../core/models/models';

@Component({
  selector: 'app-seller-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './seller-dashboard.component.html',
  styleUrls: ['./seller-dashboard.component.scss']
})
export class SellerDashboardComponent implements OnInit {
  private productService = inject(ProductService);
  private sellerService = inject(SellerService);
  private auth = inject(AuthService);

  products: ProductDTO[] = [];
  loading = true;
  get storeName() {
    const d = this.auth.getDecodedToken();
    return d ? `${d.given_name}'s Store` : 'Your Store';
  }

  get totalStock()    { return this.products.reduce((s, p) => s + p.stockQuantity, 0); }
  get avgRating()     { const r = this.products.filter(p => p.rating); return r.length ? (r.reduce((s,p) => s + (p.rating ?? 0), 0) / r.length).toFixed(1) : '—'; }
  get totalReviews()  { return this.products.reduce((s, p) => s + (p.reviewCount || 0), 0); }
  get outOfStock()    { return this.products.filter(p => p.stockQuantity === 0).length; }

  ngOnInit(): void {
    this.sellerService.getMe().pipe(
      switchMap(seller => this.productService.getBySeller(seller.id))
    ).subscribe({
      next: (data) => { this.products = data; this.loading = false; },
      error: () => { this.loading = false; }
    });
  }
}
