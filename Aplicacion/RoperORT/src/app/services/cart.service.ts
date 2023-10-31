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
  paymentMethod: string = "";
  paymentID : string = "";
  paymentBank: string = "";
  paymentCompany: string = "";
  paymentName: string = "";

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

   getToken(){
    return localStorage.getItem("activeToken");
  }

   processPayment(): Observable<any> {
    return this.http.post('https://localhost:7207/cart/buy',
    {Products : this.productsInCart,
      PaymentMethod : this.paymentMethod,
      PaymentId : this.paymentID,
      Bank : this.paymentBank,
      Company : this.paymentBank
    },
    {
      headers: { "Auth": this.getToken() || "" }
    });
  }

   getPayment(){
    return {paymentMethod:this.paymentMethod,
      paymentID:this.paymentID,
      paymentBank:this.paymentBank,
      paymentCompany:this.paymentCompany,
      paymentName:this.paymentName
    }
   }
   paymentIdSet(id: string) {
    this.paymentID = id;
   }

   paymentDataSet(metodo: string) {
    switch (metodo) {
      case "PAGANZA":
        this.paymentName = "Paganza"
        this.paymentMethod = "PAGANZA";
        this.paymentBank = "";
        this.paymentCompany = "";
        break;

      case "PAYPAL":
        this.paymentName = "PayPal"
        this.paymentMethod = "PAYPAL";
        this.paymentBank = "";
        this.paymentCompany = "";
        break;

      case "BBVA":
        this.paymentName = "Debit - BBVA"
        this.paymentMethod = "DEBIT";
        this.paymentBank = "BBVA";
        this.paymentCompany = "";
        break;

        case "ITAU":
        this.paymentName = "Debit - ITAU"
        this.paymentMethod = "DEBIT";
        this.paymentBank = "ITAU";
        this.paymentCompany = "";
        break;

        case "SANTANDER":
        this.paymentName = "Debit - Santander"
        this.paymentMethod = "DEBIT";
        this.paymentBank = "SANTANDER";
        this.paymentCompany = "";
        break;

      case "VISA":
        this.paymentName = "Credit - VISA"
        this.paymentMethod = "CREDITCARD";
        this.paymentBank = "";
        this.paymentCompany = "VISA";
        break;

        case "MASTERCARD":
        this.paymentName = "Credit - Mastercard"
        this.paymentMethod = "CREDITCARD";
        this.paymentBank = "";
        this.paymentCompany = "MASTERCARD";
        break;

      default:
        this.paymentName= "";
        this.paymentMethod = "";
        this.paymentBank = "";
        this.paymentCompany = "";
        break;
    }
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

  Buy(): Observable<any> {
    return this.http.post('https://localhost:7207/cart/buy',{Products : this.productsInCart});
  }

}
