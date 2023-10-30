import {Component, Input} from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatButtonModule} from '@angular/material/button';
import {MatSnackBar, MatSnackBarModule} from '@angular/material/snack-bar';
import {MatCardModule} from '@angular/material/card';
import {MatIconModule} from '@angular/material/icon';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user.model';
import { LoginService } from '../../services/data.service';

@Component({
  selector: 'app-user-card',
  templateUrl: './user-card.component.html',
  styleUrls: ['./user-card.component.scss'],
  standalone: true,
  imports: [CommonModule,MatCardModule, MatButtonModule,MatIconModule,MatSnackBarModule,FormsModule,MatInputModule],
})

export class UserCardComponent {
  service: LoginService;

  constructor(private _snackBar: MatSnackBar, private router: Router, private dataService: LoginService) {
    this.service = dataService;
  }

  @Input() userDetails: User = new User();

  getRoles(){
    let roles = "";
    if(this.userDetails.roles != null){
      roles = this.userDetails.roles.map((role) => role.name).join(', ');
    }
    return roles;
  }

  editUser(){
    this.router.navigate(['/admin-editing', this.userDetails.id]);
  }

  deleteUser(){
    this.service.deleteUser(this.userDetails.id).subscribe(
      (data) => {
        console.log(data);
        const text = "User was deleted";
        this.showSnackbar(text, "Close", 3000);
        if(this.userDeletedItself(this.userDetails.token)){
          this.service.logOut();
          this.router.navigate(['/home']);
        } else {
          location.reload();
        }
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

  userDeletedItself(deletedToken: string): boolean{
    return deletedToken == this.service.getToken();
  }
}
