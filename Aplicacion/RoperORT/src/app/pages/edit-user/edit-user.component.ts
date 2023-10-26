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
        const message = error.error;
        const action = "Close";
        this._snackBar.open(message, action, {
          duration: 3000,
        });
      }
    );
  }

  accept(){
    this.service.logIn(this.email, this.password).subscribe(
      (data) => {
        if(data.token == this.dataService.getToken()){
          this.accepted = true;
        } else {
          const message = "No puede editar a otro usuario";
          const action = "Close";
          this._snackBar.open(message, action, {
            duration: 3000,
          });
        }
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

  update(){

  }

  logIn(){

  }
}
