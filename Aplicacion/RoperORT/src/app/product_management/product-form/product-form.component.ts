import { Component, OnInit, Input, Inject} from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormBuilder, FormGroup, ReactiveFormsModule  } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { Product } from 'src/app/models/product.model';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import {MAT_DIALOG_DATA, MatDialogRef, } from '@angular/material/dialog';


@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.scss'],
  standalone: true,
  imports: [MatInputModule, MatButtonModule, FormsModule, MatSelectModule, MatFormFieldModule, ReactiveFormsModule, CommonModule],

})
export class ProductFormComponent implements OnInit{
  productForm: FormGroup = new FormGroup({});
  availableColors: string[] = ['Red', 'Blue', 'Green', 'Yellow', 'Black'];
  productDetails: any = new Product();
  name: string = 'asd';

  constructor(private formBuilder: FormBuilder, public dialogRef: MatDialogRef<ProductFormComponent>, @Inject(MAT_DIALOG_DATA) public data: Product) {
    this.productDetails = this.data;
    this.productForm = this.formBuilder.group({
      name: [this.productDetails.product.name],
      colors: [this.productDetails.product.colours],
    });
  }

  ngOnInit(): void {
    this.name = this.productDetails.product.name;

  }

  submitForm() {}
}
