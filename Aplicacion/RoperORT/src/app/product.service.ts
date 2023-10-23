import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  activeToken: string = "";

  constructor(private http: HttpClient) { }

  getProducts(): Observable<any> {
    return this.http.get('https://localhost:7207/product');
  }

  storeToken(newToken: string){
    this.activeToken = newToken;
  }
}
