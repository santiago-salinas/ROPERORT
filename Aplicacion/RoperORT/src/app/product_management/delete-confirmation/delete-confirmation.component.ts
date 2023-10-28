import { MatDialogRef, MAT_DIALOG_DATA, } from '@angular/material/dialog';
import { Component, Inject} from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { ProductService } from 'src/app/services/product.service';
import { MatCardModule } from '@angular/material/card';


@Component({
  selector: 'app-delete-confirmation',
  templateUrl: './delete-confirmation.component.html',
  styleUrls: ['./delete-confirmation.component.scss'],
  standalone: true,
  imports: [MatButtonModule, CommonModule, MatCardModule],
})
export class DeleteConfirmationComponent {
  productName: string = '';
  productId: number = 0;
  productService: ProductService;

  constructor(
    public dialogRef: MatDialogRef<DeleteConfirmationComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { productName: string, productId: number },
    productService: ProductService) {

      this.productName = data.productName;
      this.productId = data.productId;
      this.productService = productService;
  }

  cancel() {
    this.dialogRef.close(false);
  }

  confirmDelete() {
    this.productService.deleteProduct(this.productId);
    this.dialogRef.close(true);
  }
}
