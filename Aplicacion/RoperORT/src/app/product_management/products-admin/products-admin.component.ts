import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Product } from 'src/app/models/product.model';
import { ProductService } from 'src/app/services/product.service';
import { ProductAdminCardComponent } from 'src/app/product_management/product-admin-card/product-admin-card.component/.';
import { MatSnackBar } from '@angular/material/snack-bar';
import {MatDialog, MatDialogModule} from '@angular/material/dialog';
import { ProductFormComponent } from '../product-form/product-form.component';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';



@Component({
  selector: 'app-product-admin',
  templateUrl: './products-admin.component.html',
  styleUrls: ['./products-admin.component.scss'],
  standalone: true,
  imports: [CommonModule, ProductAdminCardComponent, MatButtonModule, MatDialogModule]
})
export class ProductsAdminComponent {
  productList : Product[];

  constructor(private productService: ProductService, private snackBar: MatSnackBar, private dialog: MatDialog, private router: Router) {
    this.productList = [];
  }

  async ngOnInit(): Promise<void> {
    this.productService.updateProducts().then(() => {
      this.productList = this.productService.availableProducts;
    });
  }

  getNameList(colors: Product[]): string {
    return colors.map((color) => color.name).join(', ');
  }

  createDialog(): void {
    let dialogRef = this.dialog.open(ProductFormComponent, {
      data:  null ,
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.productService.updateProducts().then(() => {
          this.productList = this.productService.availableProducts;
        });
      }
    });
  }

}
