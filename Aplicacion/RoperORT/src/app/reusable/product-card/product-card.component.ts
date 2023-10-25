import {Component, Input} from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatButtonModule} from '@angular/material/button';
import {MatSnackBar, MatSnackBarModule} from '@angular/material/snack-bar';
import {MatCardModule} from '@angular/material/card';
import {MatIconModule} from '@angular/material/icon';
import { Product } from 'src/app/models/product.model';

import { Router } from '@angular/router';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss'],
  standalone: true,
  imports: [CommonModule,MatCardModule, MatButtonModule,MatIconModule,MatSnackBarModule],
})


export class ProductCardComponent {
  constructor(private _snackBar: MatSnackBar, private router: Router) {}

  @Input() productDetails: Product = new Product();

  addToCart() {
    const message = `Not Implemented`;
    const action = 'Close';

    // Open the snack bar
    this._snackBar.open(message, action, {
      duration: 3000, // Specify the duration in milliseconds
    });
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
