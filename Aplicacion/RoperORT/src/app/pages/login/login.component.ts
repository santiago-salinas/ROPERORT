import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {MatSnackBar, MatSnackBarModule} from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { LoginService } from 'src/app/services/data.service';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  standalone: true,
  imports: [CommonModule,MatInputModule,MatButtonModule,FormsModule,MatSnackBarModule],
})

export class LoginComponent {
  email: string = "";
  password: string = "";
  loggedIn: boolean;
  service: LoginService;

  constructor(private dataService: LoginService, private router: Router, private _snackBar: MatSnackBar) {
    this.service = dataService;
    this.loggedIn = dataService.getToken() != null;
  }

  logIn(){
    this.service.logIn(this.email, this.password).subscribe(
      (data) => {
        console.log(data);
        this.service.storeToken(data.token);
        this.loggedIn = true;
      },
      (error) => {
        const message = error.error;
        const action = "Close";
        this._snackBar.open(message, action, {
          duration: 3000,
        });
      }
    );
  }

  logOut(){
    this.service.logOut();
    this.email = "";
    this.password = "";
    this.loggedIn = false;
  }

  signUp(){
    this.router.navigate(['/sign-up']);
  }
}
