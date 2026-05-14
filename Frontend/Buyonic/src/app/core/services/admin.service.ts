import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { RegisterRequest, UserLookupResultDTO, SetUserActiveDTO } from '../models/models';

@Injectable({ providedIn: 'root' })
export class AdminService {
  private http = inject(HttpClient);
  private api = `${environment.apiUrl}/user`;

  lookup(q: string): Observable<UserLookupResultDTO> {
    return this.http.get<UserLookupResultDTO>(`${this.api}/lookup`, {
      params: new HttpParams().set('q', q.trim())
    });
  }

  setUserActive(applicationUserId: number, payload: SetUserActiveDTO): Observable<void> {
    return this.http.put<void>(`${this.api}/${applicationUserId}/active`, payload);
  }

  createAdmin(payload: RegisterRequest): Observable<string> {
    return this.http.post(`${this.api}/admin`, payload, { responseType: 'text' });
  }
}
