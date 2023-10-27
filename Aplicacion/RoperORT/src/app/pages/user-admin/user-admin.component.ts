import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import { User } from 'src/app/models/user.model';
import { LoginService } from 'src/app/services/data.service';
import { UserCardComponent } from 'src/app/reusable/user-card/user-card.component';

@Component({
  selector: 'app-user-admin',
  templateUrl: './user-admin.component.html',
  styleUrls: ['./user-admin.component.scss'],
  standalone: true,
  imports: [CommonModule,MatIconModule,MatButtonModule,UserCardComponent]
})

export class UserAdminComponent {
  userList : User[];

  constructor(private dataService: LoginService) {
    this.userList = [];
  }

  ngOnInit(): void {
    this.dataService.getAllUsers().subscribe(
      (data: User[]) => {
        this.userList = data;
      },
      (error:any) => {
        alert('API Is Not Responding. Reloading after OK');
        location.reload();
      }
    );
  }

  addUser(){

  }
}
