import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {MatSnackBar, MatSnackBarModule} from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { LoginService } from 'src/app/services/data.service';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.scss'],
  standalone: true,
  imports: [CommonModule,MatInputModule,MatButtonModule,FormsModule,MatSnackBarModule],
})

export class EditUserComponent {
  email: string = "";
  password: string = "";
  confirmation: string = "";
  address: string = "";
  accepted: boolean = false;
  logged: boolean;
  user: any;
  service: LoginService;

  constructor(private dataService: LoginService, private router: Router, private _snackBar: MatSnackBar) {
    this.service = dataService;
    this.logged = dataService.getToken() != "";
    this.service.getUser().subscribe(
      (data) => {
        this.user = data;
        this.address = this.user.address;
      },
      (error) => {
        this.showSnackbar(error.error, "Close", 3000);
      }
    );
  }

  accept(){
    this.service.logIn(this.email, this.password).subscribe(
      (data) => {
        if(data.token == this.dataService.getToken()){
          this.accepted = true;
        } else {
          this.showSnackbar("No puedes modificar a otro usuario", "Close", 3000);
        }
      },
      (error) => {
        this.showSnackbar(error.error, "Close", 3000);
      }
    );
  }

  update(){
    if(this.password != this.confirmation){
      this.showSnackbar("Contraseñas son diferentes", "Close", 3000);
    } else {
      this.service.updateUser(this.email, this.password, this.address).subscribe(
        (data) => {
          this.logIn();
          this.showSnackbar("Usuario modificado exitosamente", "Close", 3000);
          this.router.navigate(['/home']);
        },
        (error) => {
          this.showSnackbar(error.error, "Close", 3000);
        }
      );
    }
  }

  deleteUser(){
    this.service.deleteUser().subscribe(
      (data) => {
        console.log(data);
        const text = "Usuario fué eliminado exitosamente";
        this.showSnackbar(text, "Close", 3000);
        this.service.logOut();
        this.router.navigate(['/home']);
      },
      (error) => {
        this.showSnackbar(error.error, "Close", 3000);
      }
    );
  }

  logIn(){
    this.service.logIn(this.email, this.password).subscribe(
      (data) => {
        console.log(data);
        this.service.storeToken(data.token);
      },
      (error) => {
        this.showSnackbar(error.error, "Close", 3000);
      }
    );
  }

  showSnackbar(message: string, action: string, duration: number){
    this._snackBar.open(message, action, {
      duration: duration,
    });
  }
}
