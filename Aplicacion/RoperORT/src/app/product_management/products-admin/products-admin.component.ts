import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Product } from 'src/app/models/product.model';
import { ProductService } from 'src/app/services/product.service';
import { ProductAdminCardComponent } from 'src/app/product_management/product-admin-card/product-admin-card.component/.';
import { MatSnackBar } from '@angular/material/snack-bar';



@Component({
  selector: 'app-product-admin',
  templateUrl: './products-admin.component.html',
  styleUrls: ['./products-admin.component.scss'],
  standalone: true,
  imports: [CommonModule, ProductAdminCardComponent]
})
export class ProductsAdminComponent {
  productList : Product[];

  constructor(private dataService: ProductService, private snackBar: MatSnackBar) {
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
