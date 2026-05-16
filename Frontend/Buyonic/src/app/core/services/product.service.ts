import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ProductDTO, ProductWithCategoryDTO, ProductWithSellerDTO, CreateProductDTO, UpdateProductDTO } from '../models/models';

@Injectable({ providedIn: 'root' })
export class ProductService {
  private http = inject(HttpClient);
  private api = `${environment.apiUrl}/product`;
  private readonly productUpdated = new Subject<ProductDTO>();
  readonly productUpdated$ = this.productUpdated.asObservable();

  notifyProductUpdated(product: ProductDTO): void {
    this.productUpdated.next(product);
  }

  getAll(): Observable<ProductDTO[]> {
    return this.http.get<ProductDTO[]>(this.api);
  }

  getAllWithSellers(): Observable<ProductWithSellerDTO[]> {
    return this.http.get<ProductWithSellerDTO[]>(`${this.api}/with-sellers`);
  }

  getAllWithCategories(): Observable<ProductWithCategoryDTO[]> {
    return this.http.get<ProductWithCategoryDTO[]>(`${this.api}/with-categories`);
  }

  getById(id: number): Observable<ProductDTO> {
    return this.http.get<ProductDTO>(`${this.api}/${id}`);
  }

  getByCategory(categoryId: number): Observable<ProductDTO[]> {
    return this.http.get<ProductDTO[]>(`${this.api}/by-category/${categoryId}`);
  }

  getBySeller(sellerId: number): Observable<ProductDTO[]> {
    return this.http.get<ProductDTO[]>(`${this.api}/by-seller/${sellerId}`);
  }

  create(payload: CreateProductDTO): Observable<ProductDTO> {
    return this.http.post<ProductDTO>(this.api, payload);
  }

  update(id: number, payload: UpdateProductDTO): Observable<void> {
    return this.http.put<void>(`${this.api}/${id}`, payload);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.api}/${id}`);
  }
}
