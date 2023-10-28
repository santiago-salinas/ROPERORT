import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule, FormBuilder  } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

//Pages
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { ProductAdminCardComponent } from './product_management/product-admin-card/product-admin-card.component';
import { ProductsAdminComponent } from './product_management/products-admin/products-admin.component';
import { ProductFormComponent } from './product_management/product-form/product-form.component';
import { MatCardModule } from '@angular/material/card';
import { DeleteConfirmationComponent } from './product_management/delete-confirmation/delete-confirmation.component';
import { MatDialogModule } from '@angular/material/dialog';



@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule, //Se necesita?
    FormsModule, //Se necesita?
    ReactiveFormsModule, //Se necesita?
    BrowserAnimationsModule,
    ProductsAdminComponent,
    MatCardModule,
    MatDialogModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
