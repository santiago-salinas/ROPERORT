import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { ProductService } from 'src/app/services/product.service';


@Injectable({
  providedIn: 'root'
})
export class CartService {
  constructor(private http: HttpClient, private productService: ProductService) { }

  getProducts(): Observable<any> {
    return this.http.get('https://localhost:7207/product');
  }
}
