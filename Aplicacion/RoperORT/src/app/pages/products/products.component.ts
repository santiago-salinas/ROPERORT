import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Product } from 'src/app/models/product.model';
import { ProductService } from 'src/app/services/product.service';
import { ProductCardComponent } from 'src/app/reusable/product-card/product-card.component';



@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss'],
  standalone: true,
  imports: [CommonModule, ProductCardComponent]
})

export class ProductsComponent {
  productList : Product[];

  constructor(private dataService: ProductService) {
    this.productList = [];
  }

  ngOnInit(): void {
    this.dataService.updateProducts().then(() => {
      this.productList = this.dataService.availableProducts;
    });
  }

  getNameList(colors: Product[]): string {
    return colors.map((color) => color.name).join(', ');
  }
}
