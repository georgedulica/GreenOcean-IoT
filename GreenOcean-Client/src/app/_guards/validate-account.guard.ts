import { CanActivateFn } from '@angular/router';
import { ValidateAccountService } from '../_services/validate-account.service';
import { inject } from '@angular/core';
import { map } from 'rxjs';
import { Router } from '@angular/router';

export const validateAccountGuard: CanActivateFn = (route, state) => {
  
  const router = inject(Router)
  const validateAccountService = inject(ValidateAccountService)
  
  return validateAccountService.validateAccount$.pipe(map(validateAccount => {
    if (validateAccount) { 
      return true;
    } else {
      router.navigateByUrl('/');
      return false;
    }
  }));
};

