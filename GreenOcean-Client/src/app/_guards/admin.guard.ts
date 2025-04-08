import { inject } from '@angular/core';
import { map } from 'rxjs';
import { Router } from '@angular/router';
import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';

export const adminGuard: CanActivateFn = (route, state) => {
  const router = inject(Router)
  const accountService = inject(AccountService);
  
  return accountService.currentUser$.pipe(
    map(user => {
      if (!user) return false;
      if (user.role === 'Admin') {
        return true;
      }
      else {
        router.navigateByUrl('/');
        return false;
      }
    })
  );
};
