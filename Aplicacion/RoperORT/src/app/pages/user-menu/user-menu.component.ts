import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatSnackBar, MatSnackBarModule} from '@angular/material/snack-bar';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';
import { LoginService } from 'src/app/services/data.service';

@Component({
  selector: 'app-user-menu',
  templateUrl: './user-menu.component.html',
  styleUrls: ['./user-menu.component.scss'],
  standalone: true,
  imports: [CommonModule,MatButtonModule]
})
export class UserMenuComponent {
  email: string = "";
  isAdmin: boolean = false;
  loggedIn: boolean = false;
  service: LoginService;

  constructor(private dataService: LoginService, private router: Router, private _snackBar: MatSnackBar) {
    this.service = dataService;
    this.loggedIn = dataService.getToken() != "";

    if(!this.loggedIn){
      router.navigate(['/login']);
    } else {
      this.service.getUser().subscribe(
        (data) => {
          this.email = data.email;
          for(let i = 0; i < data.roles.length; i++){
            const role = data.roles[i].name;
            if(role === "Admin")
              this.isAdmin = true;
          }
        },
        (error) => {
          this.showSnackbar(error.error, "Close", 3000);
        }
      );
    }
  }

  logOut(){
    this.service.logOut();
    this.email = "";
    this.loggedIn = false;
    this.showSnackbar("Logged Out", "Close", 3000);
    this.router.navigate(['/home']);
  }

  edit(){
    this.router.navigate(['/edit-user']);
  }

  manageUsers(){
    this.router.navigate(['/user-administration']);
  }

  manageProducts(){
    this.router.navigate(['/product-administration']);
  }

  showSnackbar(message: string, action: string, duration: number){
    this._snackBar.open(message, action, {
      duration: duration,
    });
  }
}
