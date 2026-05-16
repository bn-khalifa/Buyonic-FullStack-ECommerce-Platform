import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { CategoryDTO, CategoryWithProductsDTO, CreateCategoryDTO } from '../models/models';

@Injectable({ providedIn: 'root' })
export class CategoryService {
  private http = inject(HttpClient);
  private api = `${environment.apiUrl}/category`;

  getAll(): Observable<CategoryDTO[]> {
    return this.http.get<CategoryDTO[]>(this.api);
  }

  getAllWithProducts(): Observable<CategoryWithProductsDTO[]> {
    return this.http.get<CategoryWithProductsDTO[]>(`${this.api}/with-products`);
  }

  getById(id: number): Observable<CategoryDTO> {
    return this.http.get<CategoryDTO>(`${this.api}/${id}`);
  }

  create(payload: CreateCategoryDTO): Observable<CategoryDTO> {
    return this.http.post<CategoryDTO>(this.api, payload);
  }

  update(id: number, payload: CategoryDTO): Observable<void> {
    return this.http.put<void>(`${this.api}/${id}`, payload);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.api}/${id}`);
  }
}
