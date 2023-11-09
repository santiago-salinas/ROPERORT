import { CanActivateFn, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { LoginService } from '../services/data.service';
import { Router } from '@angular/router';
import { inject } from '@angular/core';
    
export const AdminGuard: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean | UrlTree> => {
  const loginService = inject(LoginService);
  const router = inject(Router);
  return loginService.isAdmin().then((isAdmin) => {
    if (isAdmin) {
      return true; 
    } else {
      return router.createUrlTree(['/home']); 
    }
  });
}; 