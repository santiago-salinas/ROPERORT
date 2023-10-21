import { CommonModule } from '@angular/common';
import { Component, OnInit, ChangeDetectorRef} from '@angular/core';
import { DataService } from '../data.service';
import { ProductCardComponent } from '../product-card/product-card.component';
import { TabComponent } from '../tab/tab.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule,ProductCardComponent,TabComponent],
  template: `
    <app-tab></app-tab>
    <!--
    <app-product-card
    *ngFor="let product of data; let i= index"
        [name] = product.name
        [priceUYU]=product.priceUYU
        [description]=product.description
        [brand]=product.brand.name
        [category]=product.category.name
        [colours]=getNameList(product.colours)>
    </app-product-card>
-->
  `,
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  data: any;

  constructor(private dataService: DataService) {
  }

  ngOnInit(): void {
    this.dataService.getData().subscribe(
      (data) => {
        console.log(data);
        this.data = data;
      },
      (error) => {
        alert('API Is Not Responding. Reloading after OK');
        location.reload();
      }
    );
  }

  getNameList(colors: any[]): string {
    return colors.map((color) => color.name).join(', ');
  }
}
