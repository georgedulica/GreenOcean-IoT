import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { map } from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {
  
  const router = inject(Router)
  const accountService = inject(AccountService)
  
  return accountService.currentUser$.pipe(map(user => {
    if (user && user.role === 'Member') { 
      return true;
    } else {
      router.navigateByUrl('/');
      return false;
    }
  }));
};
