import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { Product } from '../models/product.model';
import { throwError } from 'rxjs';
import { catchError, last } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  availableColours: string[] = [];
  availableCategories: string[] = [];
  availableBrands: string[] = [];
  availableProducts: Product[] = [];

  constructor(private http: HttpClient, private snackBar: MatSnackBar) {
  }

  async initializeData() {
    await this.updateColours();
    await this.updateCategories();
    await this.updateBrands();
    await this.updateProducts();
  }

  async updateColours() {
    const coloursList = await this.getColours();
    this.availableColours = this.getNames(coloursList);
  }

  async updateCategories() {
    const categoriesList = await this.getCategories();
    this.availableCategories = this.getNames(categoriesList);
  }

  async updateBrands() {
    const brandsList = await this.getBrands();
    this.availableBrands = this.getNames(brandsList);
  }

  async updateProducts() {
    const productsList = await this.getProducts();
    this.availableProducts = productsList;
  }

  async getProducts(): Promise<any> {
    return lastValueFrom(this.http.get('https://localhost:7207/product'));
  }

  async getFilteredProducts(name: string | null,
    category: string | null,
    brand: string | null,
    minPrice: number | null,
    maxPrice: number | null,
    excludedFromPromos: boolean | null): Promise<any> {
    let body = {
      "name": name,
      "category": category,
      "brand": brand,
      "minimumPrice": minPrice,
      "maximumPrice": maxPrice,
      "excludedFromPromos": excludedFromPromos
    }

    return lastValueFrom(this.http.post('https://localhost:7207/product/filtered', body));
  }

  async getProduct(id: number): Promise<any> {
    return lastValueFrom(this.http.get(`https://localhost:7207/product/${id}`));
}


  async getColours(): Promise<any> {
    return lastValueFrom(this.http.get('https://localhost:7207/colour'));
  }

  async getCategories(): Promise<any> {
    return lastValueFrom(this.http.get('https://localhost:7207/category'));
  }

  async getBrands(): Promise<any> {
    return lastValueFrom(this.http.get('https://localhost:7207/brand'));
  }

  async createProduct(product: Product): Promise<any> {
    try {
      const response = await lastValueFrom(this.http.post('https://localhost:7207/product', product, {
        headers: { auth: this.getToken() ?? "" },
      }));

      return response;
    } catch (error: any) {
        if (error.status === 400) {
          this.showErrorToast(error.error);
        }
        throw error;
    }
  }

  async updateProduct(product: Product): Promise<any> {
    try {
      const response = await lastValueFrom(this.http.put('https://localhost:7207/product/' + product.id, product, {
        headers: { auth: this.getToken() ?? "" },
      }));

      return response;
    } catch (error: any) {
        if (error.status === 400) {
          this.showErrorToast(error.error);
        }
        else if (error.status === 404) {
          this.showErrorToast('Product not found');
        }
        throw error;
    }
  }

  async deleteProduct(id: number): Promise<any> {
    return lastValueFrom(this.http.delete('https://localhost:7207/product/'+id, {headers: {auth: this.getToken() || ""}}));
  }


  private getNames(dataList: { name: string }[]): string[] {
    return dataList.map((item) => item.name);
  }

  private showErrorToast(message: string): void {
    this.snackBar.open(message, 'Close', {
      duration: 3000,
      horizontalPosition: 'center',
      verticalPosition: 'bottom',
      panelClass: ['error-toast'],
    });
  }

  getToken(){
    return localStorage.getItem("activeToken");
  }

  async checkIfAdmin(): Promise<boolean>{
    try{
      const response : any = await lastValueFrom(this.http.get('https://localhost:7207/user', { headers: { "auth": this.getToken() ?? "" } }));
      if(response.roles.some((item: { name: string; }) => item.name !== 'Admin')){
        this.showErrorToast("Not an admin");
        return false;
      }
      return true;
    }catch(error : any){
      if (error.status === 403 || error.status === 401) {
        this.showErrorToast("Not properly logged in");
      }
      return false;
    }
  }
}
