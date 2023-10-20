import {Component, Input} from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import {MatIconModule} from '@angular/material/icon';



@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss'],
  standalone: true,
  imports: [MatCardModule, MatButtonModule,MatIconModule],
})
export class ProductCardComponent {
  @Input() name:string = "Example name";
  @Input() priceUYU:number = 0;
  @Input() description:string = "Example description";
  @Input() brand:string = "NullBrand";
  @Input() category:string = "nullCategory";
  @Input() colours = "nullColours";
}
