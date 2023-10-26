import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {MatSnackBar, MatSnackBarModule} from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { LoginService } from 'src/app/services/data.service';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss'],
  standalone: true,
  imports: [CommonModule,MatInputModule,MatButtonModule,FormsModule,MatSnackBarModule],
})

export class SignUpComponent {
  email: string = "";
  password: string = "";
  confirmation: string = "";
  address: string = "";
  service: LoginService;

  constructor(private dataService: LoginService, private router: Router, private _snackBar: MatSnackBar) {
    this.service = dataService;
  }

  signUp(){
    if(this.password != this.confirmation){
      const message = "Passwords are different";
      const action = "Close";
      this._snackBar.open(message, action, {
        duration: 3000,
      });
    }
    else {
      this.service.signUp(this.email, this.password, this.address).subscribe(
        (data) => {
          console.log(data);
          this.service.logIn(this.email, this.password).subscribe(
            (data) => {
              console.log(data);
              this.router.navigate(['/home']);
            },
            (error) => {
              const message = error.error;
              const action = "Close";
              this._snackBar.open(message, action, {
                duration: 3000,
              });
            }
          )
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
  }
}
