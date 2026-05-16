import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { CartDTO } from '../models/models';

@Injectable({ providedIn: 'root' })
export class CartService {
  private http = inject(HttpClient);
  private api = `${environment.apiUrl}/cart`;

  /** Current user's cart (resolved server-side from JWT email). */
  getCart(): Observable<CartDTO> {
    return this.http.get<CartDTO>(this.api);
  }

  addToCart(productId: number, quantity: number): Observable<string> {
    const params = new HttpParams()
      .set('productId', productId)
      .set('quantity', quantity);
    return this.http.post(`${this.api}/add`, null, {
      params,
      responseType: 'text'
    });
  }

  updateCartItem(productId: number, quantity: number): Observable<string> {
    const params = new HttpParams()
      .set('productId', productId)
      .set('quantity', quantity);
    return this.http.put(`${this.api}/update`, null, {
      params,
      responseType: 'text'
    });
  }

  removeFromCart(productId: number): Observable<string> {
    const params = new HttpParams().set('productId', productId);
    return this.http.delete(`${this.api}/remove`, {
      params,
      responseType: 'text'
    });
  }

  clearCart(): Observable<string> {
    return this.http.delete(`${this.api}/clear`, { responseType: 'text' });
  }
}
