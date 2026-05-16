import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { WishlistDTO, AddToWishlistDTO } from '../models/models';

@Injectable({ providedIn: 'root' })
export class WishlistService {
  private http = inject(HttpClient);
  private api = `${environment.apiUrl}/wishlist`;

  getWishlist(): Observable<WishlistDTO> {
    return this.http.get<WishlistDTO>(this.api);
  }

  addToWishlist(payload: AddToWishlistDTO): Observable<string> {
    return this.http.post(`${this.api}/add`, payload, { responseType: 'text' });
  }

  removeFromWishlist(productId: number): Observable<string> {
    return this.http.delete(`${this.api}/remove/${productId}`, {
      responseType: 'text'
    });
  }

  clearWishlist(): Observable<string> {
    return this.http.delete(`${this.api}/clear`, { responseType: 'text' });
  }
}
