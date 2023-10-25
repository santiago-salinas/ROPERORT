import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginService } from 'src/app/services/data.service';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  standalone: true,
  imports: [CommonModule,MatInputModule,MatButtonModule,FormsModule],
})

export class LoginComponent {
  email: string = "";
  password: string = "";
  loggedIn: boolean = false;
  service: LoginService;

  constructor(private dataService: LoginService, private router: Router) {
    this.service = dataService;
  }

  logIn(){
    this.service.logIn(this.email, this.password).subscribe(
      (data) => {
        console.log(data);
        this.service.storeToken(data.token);
        this.loggedIn = true;
      },
      (error) => {
        alert(error.error);
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
