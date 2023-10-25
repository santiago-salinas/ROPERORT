import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Product } from 'src/app/models/product.model';
import { CartService } from 'src/app/services/cart.service';
import { ProductCardComponent } from 'src/app/reusable/product-card/product-card.component';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss'],
  standalone: true,
  imports: [CommonModule, ProductCardComponent]
})

export class CartComponent {
  productList : Product[] = [];
  cartProductList : Product[] = [];

  constructor(private cartService: CartService) {
  }

  ngOnInit(): void {
    this.cartService.getProducts().subscribe(
      (data:Product[]) => {
        console.log(data);
        this.productList = data;
      },
      (error:any) => {
        alert('API Is Not Responding. Reloading after OK');
        location.reload();
      }
    );

    this.cartService.evaluateCart().subscribe(
      (data:any) => {
        console.log(data);
        this.cartProductList = data.Products;
      },
      (error:any) => {
        //alert('API Is Not Responding. Reloading after OK');
        //location.reload();
      }
    );
  }

  getNameList(colors: Product[]): string {
    return colors.map((color) => color.name).join(', ');
  }
}
