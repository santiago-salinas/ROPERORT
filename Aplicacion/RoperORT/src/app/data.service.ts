import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DataService {
  activeToken: string = "";

  constructor(private http: HttpClient) {}

  getProducts(): Observable<any> {
    return this.http.get('https://localhost:7207/product');
  }

  logIn(email: string, password: string): Observable<any> {
    return this.http.post('https://localhost:7207/login',
      {
        "email": email,
        "password": password
      }
    )
  }

  storeToken(newToken: string){
    this.activeToken = newToken;
  }
}