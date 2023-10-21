import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DataService } from '../data.service';
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  standalone: true,
  imports: [MatInputModule,MatButtonModule,FormsModule],
})

export class LoginComponent {
  email: string = "";
  password: string = "";
  service: DataService;

  constructor(private dataService: DataService) {
    this.service = dataService;
  }

  logIn(){
    this.service.logIn(this.email, this.password).subscribe(
      (data) => {
        console.log(data);
        this.service.storeToken(data.token);
      },
      (error) => {
        alert('API Is Not Responding. Reloading after OK');
        location.reload();
      }
    );
  }
}
