import { Component } from '@angular/core';
import {Input} from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import {MatIconModule} from '@angular/material/icon';
import { Product } from 'src/app/models/product.model';
import {MatDividerModule} from '@angular/material/divider';




@Component({
  selector: 'app-product-admin-card',
  templateUrl: './product-admin-card.component.html',
  styleUrls: ['./product-admin-card.component.scss'],
  imports: [MatCardModule, MatButtonModule,MatIconModule,MatDividerModule],
  standalone: true,
})
export class ProductAdminCardComponent {
  constructor() {}

  @Input() productDetails: Product = new Product();

  getColoursNames(): string {
    return this.productDetails.colours.map((colour) => colour.name).join(', ');
  }
}


