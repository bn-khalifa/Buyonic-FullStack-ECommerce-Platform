import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { PaymentMethodDTO, CustomerPaymentDTO, AddCustomerPaymentDTO } from '../models/models';

@Injectable({ providedIn: 'root' })
export class PaymentService {
  private http = inject(HttpClient);
  private api = `${environment.apiUrl}/payment`;

  getPaymentMethods(): Observable<PaymentMethodDTO[]> {
    return this.http.get<PaymentMethodDTO[]>(`${this.api}/methods`);
  }

  getPaymentMethodById(id: number): Observable<PaymentMethodDTO> {
    return this.http.get<PaymentMethodDTO>(`${this.api}/methods/${id}`);
  }

  getCustomerPayments(customerId: number): Observable<CustomerPaymentDTO[]> {
    return this.http.get<CustomerPaymentDTO[]>(`${this.api}/customer/${customerId}`);
  }

  addCustomerPayment(payload: AddCustomerPaymentDTO): Observable<CustomerPaymentDTO> {
    return this.http.post<CustomerPaymentDTO>(`${this.api}/add`, payload);
  }

  removeCustomerPayment(customerPaymentId: number): Observable<string> {
    return this.http.delete<string>(`${this.api}/${customerPaymentId}`);
  }
}
