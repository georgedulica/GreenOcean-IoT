import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ResetPasswordService } from 'src/app/_services/reset-password.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent {

  constructor(private resetPasswordService: ResetPasswordService, private router: Router,
    private route: ActivatedRoute) { }

  model: any = {};

  changePassword() {
    const id = this.route.snapshot.params['id'];
    this.resetPasswordService.changePassword(this.model, id).subscribe({
      next: _ => {
        this.model.email = '';
        this.router.navigateByUrl('/');
        this.resetPasswordService.deletePasswordReset();
      },
      error: _ => {

      }    
    });
  }
}