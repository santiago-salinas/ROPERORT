import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from 'src/app/models/product.model';
import { ProductService } from 'src/app/services/product.service';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';

import { CartService } from '../../services/cart.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss'],
  standalone: true,
  imports: [MatIconModule,MatButtonModule]
})

export class ProductComponent implements OnInit {

  productId: number = -1;
  product: Product = new Product();

  constructor(private router: Router, private route: ActivatedRoute, private dataService: ProductService, private cartService: CartService) {
  }

  async ngOnInit() {
    this.route.params.subscribe(async (params) => {
      this.productId = parseInt(params['id'])
      this.product = await this.dataService.getProduct(this.productId);
    });
  }

  getColoursNames(): string {
    return this.product.colours.map((colour) => colour.name).join(', ');
  }

  addToCart() {
    this.cartService.addProduct(this.product, 1);
    this.router.navigate(['/cart']);
  }
}
