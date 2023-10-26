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
  cartProducts : any = [];
  cartData : any = [];
  emptyCart : boolean = true;


  constructor(private cartService: CartService) {
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
        if(error.error == "Empty Cart"){
          this.emptyCart = true;
        }else{
          console.log(error);
        }
      }
    );
  }
}
