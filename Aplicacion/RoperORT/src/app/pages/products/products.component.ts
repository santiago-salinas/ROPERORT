import { CommonModule } from '@angular/common';
import { Product } from 'src/app/models/product.model';
import { ProductService } from 'src/app/services/product.service';
import { ProductCardComponent } from 'src/app/reusable/product-card/product-card.component';
import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule} from '@angular/forms';
import {Observable} from 'rxjs';
import {find, map, max, startWith} from 'rxjs/operators';
import {NgFor, AsyncPipe} from '@angular/common';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {MatInputModule} from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatSliderModule, MatSlider, MatSliderThumb} from '@angular/material/slider';
import { MatCheckboxModule} from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { Input, Inject} from '@angular/core';

import { FormBuilder, Validators} from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef, } from '@angular/material/dialog';
import { MatCardModule } from '@angular/material/card';
import { NgIf } from '@angular/common';
import { MatSnackBar } from '@angular/material/snack-bar';



@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss'],
  standalone: true,
  imports: [CommonModule, ProductCardComponent, FormsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatAutocompleteModule,
    ReactiveFormsModule,
    NgFor,
    AsyncPipe,
    MatSliderModule,
    MatSelectModule,
    MatCheckboxModule,
  ]
})

export class ProductsComponent {
  productList : Product[];
  filterForm: FormGroup = new FormGroup('');
  filteredOptions: Observable<string[]> | undefined;

  name: string = '';
  category: string = '';
  brand: string = '';
  minPrice: number = 0;
  maxPrice: number = 1000;

  availableCategories: string[] = [];
  availableBrands: string[] = [];

  constructor(private dataService: ProductService,
    private formBuilder: FormBuilder,
    ) {
    this.productList = [];

    this.filterForm = this.formBuilder.group({
      name: [""],
      brand: [""],
      category: [""],
    });
  }

  async ngOnInit(): Promise<void> {
    await this.loadData();

    this.dataService.updateProducts().then(() => {
      this.productList = this.dataService.availableProducts;
    });
  }

  getNameList(colors: Product[]): string {
    return colors.map((color) => color.name).join(', ');
  }

  private autoCompleteSearch(value: string): string[] {
    const filterValue = value.toLowerCase();
    let filteredList = this.productList.filter(option => option.name.toLowerCase().includes(filterValue));

    return filteredList.map((product) => product.name);
  }

  private async loadData() {
    try {
      await this.dataService.updateBrands();
      this.availableBrands = this.dataService.availableBrands;
      await this.dataService.updateCategories();
      this.availableCategories = this.dataService.availableCategories;
    } catch (error) {
      alert('Error loading filter options');
    }
  }

  submitFilters() {
    if(!this.filterForm.valid) return;


    console.log(this.filterForm.get('name')!.value);
    console.log(this.filterForm.get('category')!.value);
    console.log(this.filterForm.get('brand')!.value);
    console.log(this.filterForm.get('minPrice')!.value);
    console.log(this.filterForm.get('maxPrice')!.value);
  }
}
