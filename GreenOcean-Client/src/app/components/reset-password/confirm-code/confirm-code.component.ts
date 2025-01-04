import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ResetPasswordService } from 'src/app/_services/reset-password.service';

@Component({
  selector: 'app-confirm-code',
  templateUrl: './confirm-code.component.html',
  styleUrls: ['./confirm-code.component.css']
})
export class ConfirmCodeComponent {

  model : any = {};
  invalidCode: boolean = false;

  constructor(private resetPasswordService: ResetPasswordService, private router: Router) { }

  confirmCode() {
    this.resetPasswordService.confirmCode(this.model).subscribe({
      next: response => {
        this.model.code = '';
        this.router.navigate(['/resetpassword/confirmcode/changepassword', response]);
      },
      error: _ => {
        this.invalidCode = true;
      }
    });
  }
}