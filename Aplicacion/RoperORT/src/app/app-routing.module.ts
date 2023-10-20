import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';


const routes: Routes = [
  {path:'', component: HomeComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes),
    MatCardModule,
    MatButtonModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
