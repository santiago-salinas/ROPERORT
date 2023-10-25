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

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss'],
  standalone: true,
  imports: [CommonModule,MatCardModule, MatButtonModule,MatIconModule,MatSnackBarModule, FormsModule,MatInputModule],
})


export class ProductCardComponent {
  constructor(private _snackBar: MatSnackBar, private router: Router) {
    this.value = 1;
  }

  @Input() productDetails: Product = new Product();
  value:number

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
    this.snackFloat("Not Implemented");
  }

  correctValue() {
    if (this.value < 1) {
      this.value = 1;
    }
  }

  async decrementValue(){
    if(this.value <= 1){
      this.value=1;
      await this.snackFloat("To delete an item, use delete button");
    }else{
      this.value-=1;
    }
  }

  async incrementValue(){
    if(this.value >= 99){
      this.value=98;
      this.snackFloat("Cortala hermano");
    }else{
      this.value+=1;
    }
  }

  removeFromCart() {
    const message = `Not Implemented`;
    const action = 'Close';

    // Open the snack bar
    this._snackBar.open(message, action, {
      duration: 3000, // Specify the duration in milliseconds
    });
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
