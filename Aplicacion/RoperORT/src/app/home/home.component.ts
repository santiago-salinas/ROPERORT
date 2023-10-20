import { CommonModule } from '@angular/common';
import { Component, OnInit, ChangeDetectorRef} from '@angular/core';
import { DataService } from '../data.service';
import { ProductCardComponent } from '../product-card/product-card.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule,ProductCardComponent],
  template: `
    <p>
      home works!
    </p>
    <app-product-card
    *ngFor="let product of data; let i= index"
        [name] = product.name
        [priceUYU]=product.price
        [description]=product.description
        [brand]=product.brand.name
        [category]=product.category.name
        [colours]=product.colours>
    </app-product-card>
  `,
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  data: any;

  constructor(private dataService: DataService) {
  }

  ngOnInit(): void {
    this.dataService.getData().subscribe((data) => {
      console.log(data);
      this.data = data;
    });
  }
}
