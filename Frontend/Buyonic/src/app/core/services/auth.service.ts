import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import { decodeBuyonicToken } from '../utils/jwt-claims';
import { StorageService } from './storage.service';
import {
  LoginRequest, LoginResponse,
  RegisterRequest, ResetPasswordDTO, DecodedToken
} from '../models/models';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private http = inject(HttpClient);
  private storage = inject(StorageService);
  private router = inject(Router);
  private apiUrl = `${environment.apiUrl}/auth`;

  register(payload: RegisterRequest): Observable<string> {
    return this.http.post(`${this.apiUrl}/register`, payload, {
      responseType: 'text'
    });
  }

  login(payload: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}/login`, payload).pipe(
      tap(res => {
        this.storage.setToken(res.token);
      })
    );
  }

  forgotPassword(email: string): Observable<string> {
    return this.http.post(
      `${this.apiUrl}/forgot-password`,
      JSON.stringify(email),
      {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
        responseType: 'text'
      }
    );
  }

  resetPassword(payload: ResetPasswordDTO): Observable<string> {
    return this.http.post(`${this.apiUrl}/reset-password`, payload, {
      responseType: 'text'
    });
  }

  logout(): void {
    this.storage.removeToken();
    this.router.navigate(['/auth/login']);
  }

  getDecodedToken(): DecodedToken | null {
    const token = this.storage.getToken();
    if (!token) return null;
    return decodeBuyonicToken(token);
  }

  getRole(): string | null {
    const r = this.getDecodedToken()?.role;
    return r ? r : null;
  }

  getUserId(): string | null {
    const id = this.getDecodedToken()?.nameid;
    return id ? id : null;
  }

  getEmail(): string | null {
    const e = this.getDecodedToken()?.email;
    return e ? e : null;
  }

  getFullName(): string {
    const d = this.getDecodedToken();
    if (!d) return '';
    return `${d.given_name} ${d.family_name}`;
  }

  isLoggedIn(): boolean {
    const token = this.storage.getToken();
    if (!token) return false;
    const decoded = decodeBuyonicToken(token);
    if (!decoded) return false;
    return decoded.exp * 1000 > Date.now();
  }

  redirectByRole(): void {
    const role = this.getRole();
    switch (role) {
      case 'Customer': this.router.navigate(['/customer']); break;
      case 'Seller':   this.router.navigate(['/seller']);   break;
      case 'Admin':    this.router.navigate(['/admin']);    break;
      default:         this.router.navigate(['/auth/login']); break;
    }
  }
}
