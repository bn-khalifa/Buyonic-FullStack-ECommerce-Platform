import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { switchMap } from 'rxjs';
import { ProductService } from '../../../core/services/product.service';
import { SellerService } from '../../../core/services/seller.service';
import { ProductDTO } from '../../../core/models/models';

@Component({
  selector: 'app-my-products',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './my-products.component.html',
  styleUrls: ['./my-products.component.scss']
})
export class MyProductsComponent implements OnInit {
  private productService = inject(ProductService);
  private sellerService = inject(SellerService);

  products: ProductDTO[] = [];
  loading = true;
  deletingId: number | null = null;
  toast = '';
  toastType = 'success';

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.loading = true;
    this.sellerService.getMe().pipe(
      switchMap(seller => this.productService.getBySeller(seller.id))
    ).subscribe({
      next: (data) => { this.products = data; this.loading = false; },
      error: () => { this.loading = false; }
    });
  }

  delete(id: number): void {
    if (!confirm('Delete this product?')) return;
    this.deletingId = id;
    this.productService.delete(id).subscribe({
      next: () => {
        this.deletingId = null;
        this.showToast('Product deleted.', 'success');
        this.load();
      },
      error: () => {
        this.deletingId = null;
        this.showToast('Failed to delete product.', 'error');
      }
    });
  }

  showToast(msg: string, type: string): void {
    this.toast = msg;
    this.toastType = type;
    setTimeout(() => this.toast = '', 3000);
  }

  getDiscountedPrice(p: ProductDTO): number {
    return p.price * (1 - p.discount / 100);
  }
}
