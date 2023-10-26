import { Component, OnInit, Input, Inject} from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormBuilder, FormGroup, ReactiveFormsModule  } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { Product } from 'src/app/models/product.model';
import { Brand, Category, Colour } from 'src/app/models/basics.model';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import {MAT_DIALOG_DATA, MatDialogRef, } from '@angular/material/dialog';
import { ProductService } from 'src/app/services/product.service';


@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.scss'],
  standalone: true,
  imports: [MatInputModule, MatButtonModule, FormsModule, MatSelectModule, MatFormFieldModule, ReactiveFormsModule, CommonModule],

})
export class ProductFormComponent {
  productForm: FormGroup = new FormGroup({});
  availableColors: string[] = [];
  availableCategories: string[] = [];
  availableBrands: string[] = [];
  productDetails: any = new Product();

  constructor(private formBuilder: FormBuilder, public dialogRef: MatDialogRef<ProductFormComponent>, @Inject(MAT_DIALOG_DATA) public currentProductData: Product, dataService: ProductService) {
    this.productDetails = currentProductData;

    dataService.getColours().subscribe(
      (colours:Colour[]) => {
        console.log(colours);
        this.availableColors = this.getNames(colours);
      },
      (error:any) => {
        alert('API Is Not Responding. Reloading after OK');
        location.reload();
      }
    );

    dataService.getBrands().subscribe(
      (brands:Brand[]) => {
        console.log(brands);
        this.availableBrands = this.getNames(brands);
      },
      (error:any) => {
        alert('API Is Not Responding. Reloading after OK');
        location.reload();
      }
    );

    dataService.getCategories().subscribe(
      (categories:Category[]) => {
        console.log(categories);
        this.availableCategories = this.getNames(categories);
      },
      (error:any) => {
        alert('API Is Not Responding. Reloading after OK');
        location.reload();
      }
    );


    this.productForm = this.formBuilder.group({
      name: this.productDetails.product.name,
      price: this.productDetails.product.priceUYU,
      stock: this.productDetails.product.stock,
      description: this.productDetails.product.description,
      brand: this.productDetails.product.brand.name,
      category: this.productDetails.product.category.name,
      colors: this.getNames(this.productDetails.product.colours),
    });
  }

  getNames(coloursList: {name:string}[]): string[] {
    return coloursList.map((colour) => colour.name);
  }


  submitForm() {}
}
