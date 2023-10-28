import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import {MatSnackBar, MatSnackBarModule} from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user.model';
import { LoginService } from 'src/app/services/data.service';
import { UserCardComponent } from 'src/app/reusable/user-card/user-card.component';

@Component({
  selector: 'app-user-admin',
  templateUrl: './user-admin.component.html',
  styleUrls: ['./user-admin.component.scss'],
  standalone: true,
  imports: [CommonModule,MatIconModule,MatButtonModule,UserCardComponent,MatSnackBarModule]
})

export class UserAdminComponent {
  userList : User[];

  constructor(private dataService: LoginService, private router: Router, private _snackBar: MatSnackBar) {
    this.userList = [];
  }

  ngOnInit(): void {
    this.dataService.getAllUsers().subscribe(
      (data: User[]) => {
        this.userList = data;
      },
      (error:any) => {
        if(error.status === 403){
          this.showSnackbar("Access is exclusive to Admins", "Close", 3000);
          this.router.navigate(['/home']);
        } else {
          alert('API Is Not Responding. Reloading after OK');
          location.reload();
        }
      }
    );
  }

  addUser(){
    this.router.navigate(['/user-creation']);
  }

  showSnackbar(message: string, action: string, duration: number){
    this._snackBar.open(message, action, {
      duration: duration,
    });
  }
}
