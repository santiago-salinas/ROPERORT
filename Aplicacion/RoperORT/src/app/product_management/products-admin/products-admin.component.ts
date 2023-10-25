import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Product } from 'src/app/models/product.model';
import { ProductService } from 'src/app/services/product.service';
import { ProductAdminCardComponent } from 'src/app/product_management/product-admin-card/product-admin-card.component/.';


@Component({
  selector: 'app-product-admin',
  templateUrl: './products-admin.component.html',
  styleUrls: ['./products-admin.component.scss'],
  standalone: true,
  imports: [CommonModule, ProductAdminCardComponent]
})
export class ProductsAdminComponent {
  productList : Product[];

  constructor(private dataService: ProductService) {
    this.productList = [];
  }

  ngOnInit(): void {
    this.dataService.getProducts().subscribe(
      (data:Product[]) => {
        console.log(data);
        this.productList = data;
      },
      (error:any) => {
        alert('API Is Not Responding. Reloading after OK');
        location.reload();
      }
    );
  }

  getNameList(colors: Product[]): string {
    return colors.map((color) => color.name).join(', ');
  }
}
