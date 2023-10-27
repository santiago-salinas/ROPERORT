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
import { SignUpComponent } from './pages/sign-up/sign-up.component';
import { EditUserComponent } from './pages/edit-user/edit-user.component';
import { UserAdminComponent } from './pages/user-admin/user-admin.component';
import { UserCardComponent } from './reusable/user-card/user-card.component';


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
    SignUpComponent,
    EditUserComponent,
    UserCardComponent,
    UserAdminComponent,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
