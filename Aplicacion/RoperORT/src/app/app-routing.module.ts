import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { ProductsComponent } from './pages/products/products.component';
import { ProductsAdminComponent } from './product_management/products-admin/products-admin.component';
import { SignUpComponent } from './pages/sign-up/sign-up.component';
import { CartComponent } from './pages/cart/cart.component';
import { EditUserComponent } from './pages/edit-user/edit-user.component';


const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'products', component: ProductsComponent },
  { path: 'admin/products', component: ProductsAdminComponent},
  { path: 'sign-up', component: SignUpComponent },
  { path: 'cart', component: CartComponent },
  { path: 'edit-user', component: EditUserComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' }, // Default route
  { path: '**', redirectTo: '/home' }, // Handle 404 errors
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
