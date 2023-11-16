import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { ProductsComponent } from './pages/products/products.component';
import { ProductComponent } from './pages/product/product.component';
import { ProductsAdminComponent } from './product_management/products-admin/products-admin.component';
import { SignUpComponent } from './pages/sign-up/sign-up.component';
import { CartComponent } from './pages/cart/cart.component';
import { EditUserComponent } from './pages/edit-user/edit-user.component';
import { BuyComponent } from './pages/buy/buy.component';
import { UserAdminComponent } from './pages/user-admin/user-admin.component';
import { UserCreationComponent } from './pages/user-creation/user-creation.component';
import { AdminEditingComponent } from './pages/admin-editing/admin-editing.component';
import { UserMenuComponent } from './pages/user-menu/user-menu.component';
import { UsersPurchasesComponent } from './pages/users-purchases/users-purchases.component';
import { AdminPurchasesComponent } from './pages/admin-purchases/admin-purchases.component';
import { AdminGuard } from './guards/admin.guard';


const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'products', component: ProductsComponent },
  { path: 'products/:id', component: ProductComponent },

  { path: 'admin/products', component: ProductsAdminComponent, canActivate: [AdminGuard] },
  { path: 'sign-up', component: SignUpComponent },
  { path: 'cart', component: CartComponent },
  { path: 'cart/buy', component: BuyComponent },

  { path: 'edit-user', component: EditUserComponent },
  { path: 'admin', component: UserAdminComponent, canActivate: [AdminGuard] },
  { path: 'admin/add-user', component: UserCreationComponent, canActivate: [AdminGuard] },
  { path: 'admin/edit-user/:id', component: AdminEditingComponent, canActivate: [AdminGuard] },
  { path: 'user', component: UserMenuComponent },
  { path: 'purchases', component: UsersPurchasesComponent },
  { path: 'admin/purchases', component: AdminPurchasesComponent, canActivate: [AdminGuard] },
  { path: '', redirectTo: '/products', pathMatch: 'full' }, // Default route
  { path: '**', redirectTo: '/products' }, // Handle 404 errors
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }