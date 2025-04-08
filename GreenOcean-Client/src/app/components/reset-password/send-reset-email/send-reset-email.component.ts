import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ResetPassword } from 'src/app/_model/resetPassword';
import { ResetPasswordService } from 'src/app/_services/reset-password.service';
import { Observable } from 'rxjs';
import { of } from 'rxjs';

@Component({
  selector: 'app-send-reset-email',
  templateUrl: './send-reset-email.component.html',
  styleUrls: ['./send-reset-email.component.css']
})
export class SendResetEmailComponent {
  model: any = {};
  invalidEmail: boolean = false;

  constructor(private resetPasswordService: ResetPasswordService, private router: Router) { }

  sendEmail() {
    this.resetPasswordService.sendEmail(this.model).subscribe({
      next: _ => {
        this.router.navigateByUrl('/resetpassword/confirmcode');
        this.model.email = '';
      },
      error: _ => {
        this.invalidEmail = true;
      }    
    });
  }
}
