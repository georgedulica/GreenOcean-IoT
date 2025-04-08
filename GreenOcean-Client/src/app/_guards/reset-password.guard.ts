import { CanActivateFn, Router } from '@angular/router';
import { ResetPasswordService } from '../_services/reset-password.service';
import { inject } from '@angular/core';
import { map } from 'rxjs';

export const resetPasswordGuard: CanActivateFn = (route, state) => {

  const router = inject(Router)
  const resetPasswordService = inject(ResetPasswordService)
  
  return resetPasswordService.resetPassword$.pipe(map(resetPassword => {
    if (resetPassword) { 
      return true;
    } else {
      router.navigateByUrl('/');
      return false;
    }
  }));

};
