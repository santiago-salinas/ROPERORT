import {Component, Input} from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatButtonModule} from '@angular/material/button';
import {MatSnackBar, MatSnackBarModule} from '@angular/material/snack-bar';
import {MatCardModule} from '@angular/material/card';
import {MatIconModule} from '@angular/material/icon';
import { Product } from 'src/app/models/product.model';

import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';

import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';

import { CartService } from '../../services/cart.service';
@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss'],
  standalone: true,
  imports: [CommonModule,MatCardModule, MatButtonModule,MatIconModule,MatSnackBarModule, FormsModule,MatInputModule, RouterModule],
})


export class ProductCardComponent {
  constructor(private _snackBar: MatSnackBar, private router: Router, private cartService: CartService) {
    this.quantity = 1;
  }

  @Input() productDetails: Product = new Product();
  @Input() quantity:number = 0;

  async snackFloat(message: string) {
    return new Promise<void>((resolve) => {
      const action = 'Close';

      // Open the snack bar
      this._snackBar.open(message, action, {
        duration: 3000, // Specify the duration in milliseconds
      });

      // Delay for a short time (e.g., 100ms) before resolving the Promise
      setTimeout(() => {
        resolve();
      }, 100);
    });
  }

  addToCart() {
    this.cartService.addProduct(this.productDetails, 1);
    this.snackFloat("Se ha añadido uno más al carrito");
    }

  removeFromCart() {
    this.cartService.removeProduct(this.productDetails);
  }

  correctValue() {
    if (this.quantity < 1) {
      this.quantity = 1;
      this.cartService.modifyProduct(this.productDetails, this.quantity);
    }
  }

  async setValue(){
    if(this.quantity <= 1){
      this.quantity=1;
      await this.snackFloat("Negative Number");
    }else if (this.quantity >= this.productDetails.stock){
      this.quantity=this.productDetails.stock;
    }
    this.cartService.modifyProduct(this.productDetails, this.quantity);
  }

  async decrementValue(){
    if(this.quantity <= 1){
      this.quantity=1;
      await this.snackFloat("To delete an item, use delete button");
    }else{
      this.quantity-=1;
    }
    this.cartService.modifyProduct(this.productDetails, this.quantity);
  }

  async incrementValue(){
    if(this.quantity >= 99){
      this.quantity=98;
      this.snackFloat("Cortala hermano");
    }else{
      this.quantity+=1;
    }
    this.cartService.modifyProduct(this.productDetails, this.quantity);
  }


  getColoursNames(): string {
    return this.productDetails.colours.map((colour) => colour.name).join(', ');
  }

  isProductsRoute(): boolean {
    return this.router.url === '/products';
  }

  isCartRoute(): boolean {
    return this.router.url === '/cart';
  }
}
