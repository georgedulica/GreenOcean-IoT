import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { map } from 'rxjs';

export const notAuthGuard: CanActivateFn = (route, state) => {
  const router = inject(Router)
  const accountService = inject(AccountService)
  
  return accountService.currentUser$.pipe(map(user => {
    if (user) { 
      router.navigateByUrl('/');
      return false;
    } else {
      return true;
    }
  }));
};
