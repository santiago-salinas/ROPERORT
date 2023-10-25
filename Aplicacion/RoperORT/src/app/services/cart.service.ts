import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Product } from 'src/app/models/product.model';
import { CartLine } from 'src/app/models/cartLine.model';

import { ProductService } from 'src/app/services/product.service';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  constructor(private http: HttpClient, private productService: ProductService) {
   }

   addProduct(product : Product, quantity : number){
    const cartLine: CartLine = {
      Id: product.id,
      Quantity: quantity
    };

    const existingCartItem = this.productsInCart.find(item => item.Id === cartLine.Id);

    if (existingCartItem) {
      existingCartItem.Quantity += quantity;
    } else {
      this.productsInCart.push(cartLine);
    }
   }

   productsInCart: CartLine[] = [];

  getProducts(): Observable<any> {
    return this.http.get('https://localhost:7207/product');
  }

  evaluateCart(): Observable<any> {
    console.log(this.productsInCart)
    return this.http.post('https://localhost:7207/cart',{Products : this.productsInCart});
  }
}
