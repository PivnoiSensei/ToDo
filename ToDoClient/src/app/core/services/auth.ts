import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { API_URL } from '../api';

import { LoginRequest } from '../../shared/models/auth/login-request';
import { RegisterRequest } from '../../shared/models/auth/register-request';

@Injectable({
  providedIn: 'root',
})
export class Auth {
  private readonly http = inject(HttpClient);

  register(request: RegisterRequest): Observable<void>{
    return this.http.post<void>(
      `${API_URL}/auth/register`,
      request,
      {
        withCredentials: true
      }
    )
  }

  login(request: LoginRequest): Observable<void>{
    return this.http.post<void>(
      `${API_URL}/auth/login`,
      request,
      {
        withCredentials: true
      }
    )
  }
  
  check(): Observable<void> {
    return this.http.get<void>(
      `${API_URL}/auth/check`,
      {
        withCredentials: true
      }
    );
  }

  logout(): Observable<void> {
    return this.http.post<void>(
      `${API_URL}/auth/logout`,
      {},
      {
        withCredentials: true
      }
    );
  }
}
