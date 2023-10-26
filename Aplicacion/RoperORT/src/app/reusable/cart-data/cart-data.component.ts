import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-cart-data',
  templateUrl: './cart-data.component.html',
  styleUrls: ['./cart-data.component.scss'],
  imports:[CommonModule],
  standalone: true
})
export class CartDataComponent {
  @Input() promoData: any;

  constructor(){
    console.log("HOLA!")

    console.log(this.promoData)
  }
}
