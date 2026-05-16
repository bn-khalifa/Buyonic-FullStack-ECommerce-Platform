import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { OrderDTO, CreateOrderDTO } from '../models/models';

@Injectable({ providedIn: 'root' })
export class OrderService {
  private http = inject(HttpClient);
  private api = `${environment.apiUrl}/order`;

  getByCustomer(customerId: number): Observable<OrderDTO[]> {
    return this.http.get<OrderDTO[]>(`${this.api}/customer/${customerId}`);
  }

  /** Orders that include at least one product from this seller (Seller or Admin JWT). */
  getBySeller(sellerId: number): Observable<OrderDTO[]> {
    return this.http.get<OrderDTO[]>(`${this.api}/seller/${sellerId}`);
  }

  /** All platform orders (Admin JWT). */
  getAll(): Observable<OrderDTO[]> {
    return this.http.get<OrderDTO[]>(`${this.api}/all`);
  }

  getById(orderId: number): Observable<OrderDTO> {
    return this.http.get<OrderDTO>(`${this.api}/${orderId}`);
  }

  create(payload: CreateOrderDTO): Observable<OrderDTO> {
    return this.http.post<OrderDTO>(this.api, payload);
  }

  updateStatus(orderId: number, status: string): Observable<OrderDTO> {
    const params = new HttpParams().set('status', status);
    return this.http.put<OrderDTO>(`${this.api}/${orderId}/status`, null, { params });
  }
}
