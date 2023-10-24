import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule  } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { Product } from '../models/product.model';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import {MatButtonModule} from '@angular/material/button';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.scss'],
  standalone: true,
  imports: [MatInputModule, MatButtonModule, FormsModule, MatSelectModule, MatFormFieldModule, ReactiveFormsModule],

})
export class ProductFormComponent implements OnInit {
  productForm: FormGroup = new FormGroup({});

  constructor(private formBuilder: FormBuilder) {}

  @Input() productDetails:Product = new Product();

  ngOnInit() {
    this.productForm = this.formBuilder.group({
      name: [this.productDetails.name],
      description: [this.productDetails.description],
      price: [this.productDetails.priceUYU],
      stock: [this.productDetails.stock],
      brand: [this.productDetails.brand],
      category: [this.productDetails.category],
      colors: [this.productDetails.colours],
    });
  }

  submitForm() {}
}
