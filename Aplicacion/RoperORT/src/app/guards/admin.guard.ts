import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { CanActivateFn } from '@angular/router';
import { LoginService } from '../services/data.service';
import { inject } from '@angular/core';

export const AdminGuard: CanActivateFn =
    (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
      return inject(LoginService).isAdmin();
    };