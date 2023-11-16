import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {MatSnackBar, MatSnackBarModule} from '@angular/material/snack-bar';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { Router } from '@angular/router';
import { LoginService } from 'src/app/services/data.service';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-user-creation',
  templateUrl: './user-creation.component.html',
  styleUrls: ['./user-creation.component.scss'],
  standalone: true,
  imports: [CommonModule,MatInputModule,MatButtonModule,FormsModule,MatCheckboxModule,MatSnackBarModule],
})

export class UserCreationComponent {
  email: string = "";
  password: string = "";
  address: string = "";
  admin: boolean = false;
  customer: boolean = false;
  service: LoginService;

  constructor(private dataService: LoginService, private router: Router, private _snackBar: MatSnackBar) {
    this.service = dataService;
  }

  create(){
    let roles = [];
    if(this.admin) roles.push("Admin");
    if(this.customer) roles.push("Customer");
    this.service.createUser(this.email, this.password, this.address, roles).subscribe(
      (data) => {
        console.log(data);
        const text = "Usuario fué creado exitosamente";
        this.showSnackbar(text, "Close", 3000);
        this.router.navigate(['/user-administration']);
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
