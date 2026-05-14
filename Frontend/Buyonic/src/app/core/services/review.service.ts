import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ProductReviewDTO, CreateReviewDTO } from '../models/models';

@Injectable({ providedIn: 'root' })
export class ReviewService {
  private http = inject(HttpClient);
  private api = `${environment.apiUrl}/review`;

  getProductReviews(productId: number): Observable<ProductReviewDTO[]> {
    return this.http.get<ProductReviewDTO[]>(`${this.api}/product/${productId}`);
  }

  submitReview(payload: CreateReviewDTO): Observable<string> {
    return this.http.post<string>(this.api, payload);
  }
}
