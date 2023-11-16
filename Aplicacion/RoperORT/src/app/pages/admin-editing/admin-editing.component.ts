import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {MatSnackBar, MatSnackBarModule} from '@angular/material/snack-bar';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { Router, ActivatedRoute } from '@angular/router';
import { LoginService } from 'src/app/services/data.service';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-admin-editing',
  templateUrl: './admin-editing.component.html',
  styleUrls: ['./admin-editing.component.scss'],
  standalone: true,
  imports: [CommonModule,MatInputModule,MatButtonModule,FormsModule,MatCheckboxModule,MatSnackBarModule],
})
export class AdminEditingComponent {
  email: string = "";
  password: string = "";
  address: string = "";
  admin: boolean = false;
  customer: boolean = false;
  userId: string;
  user: any;
  service: LoginService;

  constructor(private dataService: LoginService, private _snackBar: MatSnackBar, 
      private router: Router, private route: ActivatedRoute) {
    this.service = dataService;
    this.userId = this.route.snapshot.paramMap.get("id") || "";
    this.service.getUserById(this.userId).subscribe(
      (data) => {
        this.user = data;
        this.email = this.user.email;
        this.password = this.user.password;
        this.address = this.user.address;
        const roles = this.user.roles;
        for(let i = 0; i < roles.length; i++){
          const role = roles[i].name;
          if(role === "Admin")
            this.admin = true;
          if(role === "Customer")
            this.customer = true;
        }
      },
      (error) => {
        this.showSnackbar(error.error, "Close", 3000);
      }
    );
  }

  update(){
    let roles = [];
    if(this.admin) roles.push("Admin");
    if(this.customer) roles.push("Customer");
    this.service.updateUserById(this.userId, this.email, this.password, this.user.token, this.address, roles).subscribe(
      (data) => {
        console.log(data);
        const text = "User was updated";
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
