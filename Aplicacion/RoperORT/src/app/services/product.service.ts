import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  constructor(private http: HttpClient) { }

  getProducts(): Observable<any> {
    return this.http.get('https://localhost:7207/product');
  }

  getColours(): Observable<any> {
    return this.http.get('https://localhost:7207/colour');
  }

  getBrands(): Observable<any> {
    return this.http.get('https://localhost:7207/brand');
  }

  getCategories(): Observable<any> {
    return this.http.get('https://localhost:7207/category');
  }

}
