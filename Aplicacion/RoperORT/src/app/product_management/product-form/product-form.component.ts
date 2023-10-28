import { Component, OnInit, Input, Inject} from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormBuilder, FormGroup, ReactiveFormsModule  } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { Product } from 'src/app/models/product.model';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MAT_DIALOG_DATA, MatDialogRef, } from '@angular/material/dialog';
import { ProductService } from 'src/app/services/product.service';
import { MatCardModule } from '@angular/material/card';


@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.scss'],
  standalone: true,
  imports: [MatInputModule, MatButtonModule, FormsModule, MatSelectModule, MatFormFieldModule, ReactiveFormsModule, MatCheckboxModule, CommonModule, MatCardModule],

})
export class ProductFormComponent implements OnInit{
  productForm: FormGroup = new FormGroup({});
  availableColors: string[] = [];
  availableCategories: string[] = [];
  availableBrands: string[] = [];
  productDetails: any = new Product();
  dataService: ProductService;

  constructor(private formBuilder: FormBuilder,
              public dialogRef: MatDialogRef<ProductFormComponent>,
              @Inject(MAT_DIALOG_DATA) public currentProductData: Product,
              dataService: ProductService) {
    this.productDetails = currentProductData;
    this.dataService = dataService;

    this.productForm = this.formBuilder.group({
      name: this.productDetails.product.name,
      price: this.productDetails.product.priceUYU,
      stock: this.productDetails.product.stock,
      description: this.productDetails.product.description,
      brand: this.productDetails.product.brand.name,
      category: this.productDetails.product.category.name,
      exclude: [this.productDetails.product.exclude],
      colours: [],
    });
  }

  ngOnInit(): void {
    this.dataService.updateBrands;
    this.dataService.updateCategories;
    this.dataService.updateColours;

    this.availableBrands = this.dataService.availableBrands;
    this.availableCategories = this.dataService.availableCategories;
    this.availableColors = this.dataService.availableColours;

    this.productForm.get('colours')!.setValue(this.getNames(this.productDetails.product.colours));
  }

  getNames(coloursList: {name:string}[]): string[] {
    return coloursList.map((colour) => colour.name);
  }


  async submitForm() {
    let newProduct: Product = new Product();
    newProduct.colours.pop();

    newProduct.id = this.productDetails.product.id;
    newProduct.name = this.productForm.get('name')!.value;
    newProduct.priceUYU = this.productForm.get('price')!.value;
    newProduct.stock = this.productForm.get('stock')!.value;
    newProduct.description = this.productForm.get('description')!.value;
    newProduct.brand.name = this.productForm.get('brand')!.value;
    newProduct.category.name = this.productForm.get('category')!.value;
    newProduct.exclude = this.productForm.get('exclude')!.value;

    for (let colour of this.productForm.get('colours')!.value) {
      newProduct.addColour(colour);
    }

    await this.dataService.updateProduct(newProduct);
    this.dialogRef.close(newProduct);
  }
}
