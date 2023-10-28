import { Component, ViewContainerRef } from '@angular/core';
import {Input} from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { Product } from 'src/app/models/product.model';
import { MatDividerModule } from '@angular/material/divider';
import { CommonModule } from '@angular/common';
import {MatDialog, MatDialogModule} from '@angular/material/dialog';
import { ProductFormComponent } from '../product-form/product-form.component';
import { DeleteConfirmationComponent } from '../delete-confirmation/delete-confirmation.component';



@Component({
  selector: 'app-product-admin-card',
  templateUrl: './product-admin-card.component.html',
  styleUrls: ['./product-admin-card.component.scss'],
  imports: [MatCardModule, MatButtonModule,MatIconModule,MatDividerModule, CommonModule, MatDialogModule],
  standalone: true,
})
export class ProductAdminCardComponent {
  showCard: boolean = true;

  constructor(public dialog: MatDialog, private viewContainerRef: ViewContainerRef) {}

  @Input() productDetails: Product = new Product();

  getColoursNames(): string {
    return this.productDetails.colours.map((colour) => colour.name).join(', ');
  }

  editDialog(): void {
    let dialogRef = this.dialog.open(ProductFormComponent, {
      data: { product: this.productDetails },
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.productDetails = result;
      }
    });
  }

  deleteDialog(): void {
    let dialogRef = this.dialog.open(DeleteConfirmationComponent, {
      data: { productName: this.productDetails.name, productId: this.productDetails.id },
    });

    dialogRef.afterClosed().subscribe((result: boolean) => {
      if (result) {
        this.showCard = false;
      }
    });
  }
}


