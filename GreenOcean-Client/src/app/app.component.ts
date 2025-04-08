import { Component } from '@angular/core';
import { User } from './_model/user';
import { AccountService } from './_services/account.service';
import { ResetPasswordService } from './_services/reset-password.service';
import { ResetPassword } from './_model/resetPassword';
import { ValidateAccountService } from './_services/validate-account.service';
import { ValidateAccount } from './_model/validateAccount';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'GreenOcean';

  constructor(private accountService: AccountService, 
    private resetPasswordService: ResetPasswordService, 
      private validateAccountService: ValidateAccountService) {}

  ngOnInit():void{
    this.setCurrentUser();
    this.setResetPassword();
    this.deleteResetPassword();
    this.setValidateAccount();
    this.deleteValidateAccount();
  }

  setCurrentUser(){
    const userString = localStorage.getItem('user');
    if(!userString) return;

    const user: User = JSON.parse(userString)
    this.accountService.setCurrentUser(user);
  }

  setResetPassword(){
    const resetString = localStorage.getItem('reset');
    if(!resetString) return;

    const resetPassword: ResetPassword = JSON.parse(resetString)
    this.resetPasswordService.setResetPassword(resetPassword);
  }
  
  deleteResetPassword() {
    const resetString = localStorage.getItem('reset');
    if(!resetString) return;

    setTimeout(() => {
      this.resetPasswordService.deletePasswordReset()
    }, 30000);
  }

  setValidateAccount(){
    const validateString = localStorage.getItem('validate');
    if(!validateString) return;

    const validateAccount: ValidateAccount = JSON.parse(validateString)
    this.validateAccountService.setValidateAccount(validateAccount);
  }
  
  deleteValidateAccount() {
    const validateString = localStorage.getItem('validate');
    if(!validateString) return;

    setTimeout(() => {
      this.validateAccountService.deleteValidateAccount()
    }, 30000);
  }
}