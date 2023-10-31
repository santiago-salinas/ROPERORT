import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { LoginService } from 'src/app/services/data.service';

@Component({
  selector: 'app-user-menu',
  templateUrl: './user-menu.component.html',
  styleUrls: ['./user-menu.component.scss'],
  standalone: true,
  imports: [CommonModule]
})
export class UserMenuComponent {
  loggedIn: boolean = false;
  service: LoginService;

  constructor(private dataService: LoginService, private router: Router) {
    this.service = dataService;
    this.loggedIn = dataService.getToken() != "";

    if(!this.loggedIn)
      router.navigate(['/login']);
  }
}
