import { Component, OnInit, Input, Inject} from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { Product } from 'src/app/models/product.model';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule} from '@angular/material/checkbox';
import { MAT_DIALOG_DATA, MatDialogRef, } from '@angular/material/dialog';
import { ProductService } from 'src/app/services/product.service';
import { MatCardModule } from '@angular/material/card';
import { NgIf } from '@angular/common';
import { MatSnackBar } from '@angular/material/snack-bar';



@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.scss'],
  standalone: true,
  imports: [MatInputModule,
    MatButtonModule,
    FormsModule,
    MatSelectModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    CommonModule,
    MatCardModule,
    NgIf],

})
export class ProductFormComponent implements OnInit{
  productForm: FormGroup = new FormGroup("",[Validators.required,]);
  isCreateForm: boolean = false;

  formIsValid: boolean = false;
  availableColors: string[] = [];
  availableCategories: string[] = [];
  availableBrands: string[] = [];
  productDetails: Product = new Product();


  constructor(
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<ProductFormComponent>,
    @Inject(MAT_DIALOG_DATA) public currentProductData: Product | null,
    private dataService: ProductService,
    private snackBar: MatSnackBar
  ) {

    if(currentProductData == null) {
      this.isCreateForm = true;
      this.productDetails = new Product();
    }else{
      this.productDetails = currentProductData;
    }

    this.productForm = this.formBuilder.group({
      name: [this.productDetails.name, Validators.required],
      price: [this.productDetails.priceUYU, [Validators.required, this.priceGreaterThanZeroValidator]],
      stock: [this.productDetails.stock, Validators.required],
      description: [this.productDetails.description],
      brand: [this.productDetails.brand.name],
      category: [this.productDetails.category.name],
      exclude: [this.productDetails.exclude],
      colours: [[]],
    });

  }

  ngOnInit(): void {
    this.loadData();

    this.productForm.valueChanges.subscribe(() => {
      this.formIsValid = this.productForm.valid;
    });

    this.productForm.get('colours')!.setValue(this.getNames(this.productDetails.colours));
  }

  getNames(coloursList: { name: string }[]): string[] {
    return coloursList.map((colour) => colour.name);
  }

  async submitForm() {
    if(!this.productForm.valid) return;

    const newProduct: Product = new Product();

    newProduct.id = this.productDetails.id;
    newProduct.name = this.productForm.get('name')!.value;
    newProduct.priceUYU = this.productForm.get('price')!.value;
    newProduct.stock = this.productForm.get('stock')!.value;
    newProduct.description = this.productForm.get('description')!.value;
    newProduct.brand = { name: this.productForm.get('brand')!.value };
    newProduct.category = { name: this.productForm.get('category')!.value };
    newProduct.exclude = this.productForm.get('exclude')!.value;
    newProduct.colours = this.productForm.get('colours')!.value.map((colorName: string) => ({ name: colorName }));

    try{
      if(this.isCreateForm){
        await this.dataService.createProduct(newProduct);
        this.showToast('Product created successfully.', true);
      }else{
        await this.dataService.updateProduct(newProduct);
        this.showToast('Update was successful.', true);
      }
      this.dialogRef.close(newProduct);
    }catch(error){
      return
    }
  }

  async loadData() {
    try {
      await this.dataService.updateBrands();
      this.availableBrands = this.dataService.availableBrands;
      await this.dataService.updateCategories();
      this.availableCategories = this.dataService.availableCategories;
      await this.dataService.updateColours();
      this.availableColors = this.dataService.availableColours;

    } catch (error) {
      alert('Error loading data');
    }
  }

  showToast(message: string, isSuccess: boolean): void {
    this.snackBar.open(message, 'Close', {
      duration: 3000,
      horizontalPosition: 'center',
      verticalPosition: 'bottom',
      panelClass: isSuccess ? ['success-toast'] : ['error-toast'],
    });
  }

  priceGreaterThanZeroValidator(control: FormControl): { [key: string]: any } | null {
    const value = control.value;
    if (value !== null && value <= 0) {
      return { 'priceLessThanZero': true };
    }
    return null;
  }
}

