import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Product } from 'src/app/models/product.model';
import { CartService } from 'src/app/services/cart.service';
import { ProductCardComponent } from 'src/app/reusable/product-card/product-card.component';
import { CartDataComponent } from 'src/app/reusable/cart-data/cart-data.component';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import { Router } from '@angular/router';


@Component({
  selector: 'app-buy',
  templateUrl: './buy.component.html',
  styleUrls: ['./buy.component.scss'],
  standalone: true,
  imports: [CommonModule, ProductCardComponent, CartDataComponent,MatButtonModule,MatIconModule]
})

export class BuyComponent {
  cartProducts : any = [];
  cartData : any = [];
  emptyCart : boolean = true;
  paymentData : any = [];

  constructor(private cartService: CartService, private router: Router) {
  }

  selectPayment(metodo : string){
    this.cartService.payment(metodo, 1);
    this.paymentData = this.cartService.getPayment();
  }
  navigateToCartBuy() {
    this.router.navigate(['/cart/buy']);
  }

  ngOnInit(): void {
    this.cartService.cartData$.subscribe(() => {
      this.refreshCart();
    });
  }

  getNameList(colors: Product[]): string {
    return colors.map((color) => color.name).join(', ');
  }

  refreshCart():void{
    this.cartService.getCart().subscribe(
      (data:any) => {
        console.log("Consiguiendo el carrito");
        console.log(data);
        console.log(data.products);
        this.emptyCart = false;
        this.cartData = data;
        this.cartProducts = data.products;

      },
      (error:any) => {
        console.log(error.status);
        if (error.status === 420) {
          alert("El carrito ha sufrido cambios en relación a la disponibilidad por stock de ciertos items.");
          this.emptyCart = false;
          this.cartData = error.error;
          this.cartProducts = error.error.products;
          localStorage.setItem('cart', JSON.stringify(this.transformObject(this.cartProducts)));
        }
        else if(error.error == "Empty Cart"){
          this.emptyCart = true;
        }else{
          console.log(error);
        }
      }
    );
  }

  transformObject(inputArray:any):any {
    return inputArray.map((item:any) => {
      const { product, quantity } = item;
      return {
        Id: product.id,
        Quantity: quantity,
      };
    });
  }
}
