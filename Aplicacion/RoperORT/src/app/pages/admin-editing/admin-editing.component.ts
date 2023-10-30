import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {MatSnackBar, MatSnackBarModule} from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { LoginService } from 'src/app/services/data.service';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';

import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-admin-editing',
  templateUrl: './admin-editing.component.html',
  styleUrls: ['./admin-editing.component.scss'],
  standalone: true,
  imports: [CommonModule,MatInputModule,MatButtonModule,FormsModule,MatSnackBarModule],
})
export class AdminEditingComponent implements OnInit {
  email: string = "";
  password: string = "";
  address: string = "";
  admin: boolean = false;
  customer: boolean = false;
  userId: string;
  user: any;
  service: LoginService;

  constructor(private dataService: LoginService, private router: Router, private _snackBar: MatSnackBar, private route: ActivatedRoute) {
    this.service = dataService;
    this.userId = this.route.snapshot.paramMap.get("id") || "";
    this.service.getUserById(this.userId).subscribe(
      (data) => {
        this.user = data;
        this.email = this.user.email;
        this.password = this.user.password;
        this.address = this.user.address;
      },
      (error) => {
        this.showSnackbar(error.error, "Close", 3000);
      }
    );
  }

  ngOnInit(): void {
  }

  update(){
  }

  showSnackbar(message: string, action: string, duration: number){
    this._snackBar.open(message, action, {
      duration: duration,
    });
  }
}
