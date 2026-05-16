import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { SellerDTO, SellerWithProductsDTO, CreateSellerDTO, UpdateSellerDTO } from '../models/models';

@Injectable({ providedIn: 'root' })
export class SellerService {
  private http = inject(HttpClient);
  private api = `${environment.apiUrl}/seller`;

  getAll(): Observable<SellerDTO[]> {
    return this.http.get<SellerDTO[]>(this.api);
  }

  getAllWithProducts(): Observable<SellerWithProductsDTO[]> {
    return this.http.get<SellerWithProductsDTO[]>(`${this.api}/with-products`);
  }

  /** Current seller (JWT email); use for seller Id, not AspNetUsers Id from the token. */
  getMe(): Observable<SellerDTO> {
    return this.http.get<SellerDTO>(`${this.api}/me`);
  }

  getById(id: number): Observable<SellerDTO> {
    return this.http.get<SellerDTO>(`${this.api}/${id}`);
  }

  getByIdWithProducts(id: number): Observable<SellerWithProductsDTO> {
    return this.http.get<SellerWithProductsDTO>(`${this.api}/${id}/with-products`);
  }

  searchByEmail(email: string): Observable<SellerDTO> {
    return this.http.get<SellerDTO>(`${this.api}/by-email`, { params: { email } });
  }

  create(payload: CreateSellerDTO): Observable<SellerDTO> {
    return this.http.post<SellerDTO>(this.api, payload);
  }

  update(id: number, payload: UpdateSellerDTO): Observable<void> {
    return this.http.put<void>(`${this.api}/${id}`, payload);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.api}/${id}`);
  }
}
