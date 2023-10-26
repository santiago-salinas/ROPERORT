import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Product } from 'src/app/models/product.model';
import { CartLine } from 'src/app/models/cartLine.model';

import { ProductService } from 'src/app/services/product.service';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  productsInCart: CartLine[] = [];
  private cartDataSubject = new BehaviorSubject<any>(null);
  cartData$ = this.cartDataSubject.asObservable();

  constructor(private http: HttpClient, private productService: ProductService) {
    //Lo primero que hace el service es obtener de local storage el carrito
    const cartProductList = JSON.parse(localStorage.getItem('cart') || '[]');
    this.productsInCart = cartProductList || [];
   }

   update():void{
    localStorage.setItem('cart', JSON.stringify(this.productsInCart));
    this.cartDataSubject.next(this.productsInCart);
   }

   addProduct(product : Product, quantity : number){
    this.updateProductsLocal();

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
    //Si se modifica el carrito se guarda en local storage
    this.update();
   }

   removeProduct(product: Product) {
    this.updateProductsLocal();

    const index = this.productsInCart.findIndex(item => item.Id === product.id);

    if (index !== -1) {
      this.productsInCart.splice(index, 1);
    }
    this.update();
  }

  modifyProduct(product: Product, newQuantity: number) {
    this.updateProductsLocal();
    const existingCartItem = this.productsInCart.find(item => item.Id === product.id);

    if (existingCartItem) {
      existingCartItem.Quantity = newQuantity;
    } else {
      const cartLine: CartLine = {
        Id: product.id,
        Quantity: newQuantity
      };
      this.productsInCart.push(cartLine);
    }

    this.update();

  }
  updateProductsLocal(): void{
    this.productsInCart = JSON.parse(localStorage.getItem('cart') || '[]');
  }

  getCart(): Observable<any> {
    return this.http.post('https://localhost:7207/cart',{Products : this.productsInCart});
  }

}
