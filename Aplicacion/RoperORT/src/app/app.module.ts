import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule, FormBuilder  } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

//Pages
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/home.component';

import { ProductsAdminComponent } from './product_management/products-admin/products-admin.component';
import { MatCardModule } from '@angular/material/card';
import { MatDialogModule } from '@angular/material/dialog';
import {MatIconModule} from '@angular/material/icon';
import { SignUpComponent } from './pages/sign-up/sign-up.component';
import { EditUserComponent } from './pages/edit-user/edit-user.component';
import { UserAdminComponent } from './pages/user-admin/user-admin.component';
import { UserCardComponent } from './reusable/user-card/user-card.component';
import { UserCreationComponent } from './pages/user-creation/user-creation.component';
import { AdminEditingComponent } from './pages/admin-editing/admin-editing.component';
import { UserMenuComponent } from './pages/user-menu/user-menu.component';
import { MatSliderModule } from '@angular/material/slider';
import { PurchaseCardComponent } from './reusable/purchase-card/purchase-card.component';
import { UsersPurchasesComponent } from './pages/users-purchases/users-purchases.component';
import { AdminPurchasesComponent } from './pages/admin-purchases/admin-purchases.component';
import { ProductComponent } from './pages/product/product.component';



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
    MatIconModule,
    BrowserAnimationsModule,
    ProductsAdminComponent,
    MatCardModule,
    MatDialogModule,
    SignUpComponent,
    EditUserComponent,
    UserCardComponent,
    UserAdminComponent,
    UserCreationComponent,
    AdminEditingComponent,
    UserMenuComponent,
    MatSliderModule,
    PurchaseCardComponent,
    UsersPurchasesComponent,
    AdminPurchasesComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
