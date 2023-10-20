import {Component, Input} from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatSnackBar, MatSnackBarModule} from '@angular/material/snack-bar';
import {MatCardModule} from '@angular/material/card';
import {MatIconModule} from '@angular/material/icon';



@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss'],
  standalone: true,
  imports: [MatCardModule, MatButtonModule,MatIconModule,MatSnackBarModule],
})


export class ProductCardComponent {
  constructor(private _snackBar: MatSnackBar) {}

  @Input() name:string = "Example name";
  @Input() priceUYU:string = "0";
  @Input() description:string = "Example description";
  @Input() brand:string = "NullBrand";
  @Input() category:string = "nullCategory";
  @Input() colours = "nullColours";

  addToCart() {
    const message = `Not Implemented`;
    const action = 'Close';

    // Open the snack bar
    this._snackBar.open(message, action, {
      duration: 3000, // Specify the duration in milliseconds
    });
  }
}
