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
import { MatRadioModule} from '@angular/material/radio';
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
    MatRadioModule
  ]
})

export class ProductsComponent {
  productList : Product[];
  autoComplete: FormControl = new FormControl('');
  filteredOptions: Observable<string[]> | undefined;

  name: string | null = null;
  category: string | null = null;
  brand: string | null = null;
  minPrice: number | null = null;
  maxPrice: number | null = null;
  promotionsFilter: string = 'both';


  availableCategories: string[] = [];
  availableBrands: string[] = [];

  constructor(private dataService: ProductService,
    ) {
    this.productList = [];
    this.filteredOptions = this.autoComplete.valueChanges.pipe(
      startWith(''),
      map(value => this.autoCompleteSearch(value))
    );
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
      let name = this.autoComplete.value == "" ?  null : this.autoComplete.value;
      let category = this.category == "" ? null : this.category;
      let brand = this.brand == "" ? null : this.brand;
      let minPrice = this.minPrice == undefined ? null : this.minPrice;
      let maxPrice = this.maxPrice == undefined ? null : this.maxPrice;
      let excludedFromPromos;
      if(this.promotionsFilter == 'enabled') excludedFromPromos = false;
      else if(this.promotionsFilter == 'excluded') excludedFromPromos = true;
      else excludedFromPromos = null;

      this.dataService.getFilteredProducts(name, category, brand, minPrice, maxPrice, excludedFromPromos).then((products) => {
        this.productList = products;
      });
  }
}
