import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { CustomerDTO, UpdateCustomerDTO, CustomerWithOrdersDTO } from '../models/models';

@Injectable({ providedIn: 'root' })
export class CustomerService {
  private http = inject(HttpClient);
  private api = `${environment.apiUrl}/customer`;

  getAll(includeOrders = false): Observable<CustomerDTO[] | CustomerWithOrdersDTO[]> {
    return this.http.get<any[]>(`${this.api}`, { params: { includeOrders } });
  }

  getMe(): Observable<CustomerDTO> {
    return this.http.get<CustomerDTO>(`${this.api}/me`);
  }

  getById(id: number): Observable<CustomerDTO> {
    return this.http.get<CustomerDTO>(`${this.api}/${id}`);
  }

  searchByEmail(email: string): Observable<CustomerDTO> {
    return this.http.get<CustomerDTO>(`${this.api}/search`, { params: { email } });
  }

  update(payload: UpdateCustomerDTO): Observable<string> {
    return this.http.put(`${this.api}/update`, payload, { responseType: 'text' });
  }

  delete(id: number): Observable<string> {
    return this.http.delete(`${this.api}/delete/${id}`, { responseType: 'text' });
  }
}
