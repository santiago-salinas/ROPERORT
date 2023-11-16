import { CommonModule } from '@angular/common';
import { Product } from 'src/app/models/product.model';
import { CartService } from 'src/app/services/cart.service';
import { ProductCardComponent } from 'src/app/reusable/product-card/product-card.component';
import { CartDataComponent } from 'src/app/reusable/cart-data/cart-data.component';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss'],
  standalone: true,
  imports: [CommonModule, ProductCardComponent, CartDataComponent,MatButtonModule,MatIconModule]
})

export class CartComponent {
  cartProducts : any = [];
  cartData : any = [];
  emptyCart : boolean = true;

  private destroy$ = new Subject<void>();

  constructor(private cartService: CartService, private router: Router) {
   this.cartService.cartData$
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => {
        this.refreshCart();
      });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  navigateToCartBuy() {
    this.router.navigate(['/cart/buy']);
  }

  getNameList(colors: Product[]): string {
    return colors.map((color) => color.name).join(', ');
  }

  async refreshCart(): Promise<void> {
    try {
      const data = await this.cartService.getCart();
      console.log('Consiguiendo el carrito');
      console.log(data);
      console.log(data.products);
      this.emptyCart = false;
      this.cartData = data;
      this.cartProducts = data.products;
    } catch (error: any) {
      console.log((error as HttpErrorResponse).status);
      if ((error as HttpErrorResponse).status === 420) {
        alert('El carrito ha sufrido cambios en relación a la disponibilidad por stock de ciertos items.');
        this.emptyCart = false;
        this.cartData = (error as HttpErrorResponse).error;
        this.cartProducts = (error as HttpErrorResponse).error.products;
        localStorage.setItem('cart', JSON.stringify(this.transformObject(this.cartProducts)));
      } else if ((error as HttpErrorResponse).error == 'Empty Cart') {
        this.emptyCart = true;
      } else {
        console.log(error);
      }
    }
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
