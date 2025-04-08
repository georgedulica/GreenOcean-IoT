import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ValidateAccountService } from 'src/app/_services/validate-account.service';

@Component({
  selector: 'app-validate-account',
  templateUrl: './validate-account.component.html',
  styleUrls: ['./validate-account.component.css']
})
export class ValidateAccountComponent {
  model: any = {};

  constructor(private route: ActivatedRoute, private router: Router,
    private validateAccountService: ValidateAccountService) {}

  validateAccount() {
    const id = this.route.snapshot.params['id'];

    this.validateAccountService.validateAccount(this.model, id).subscribe({
      next: _ => {
        this.model.username = '';
        this.model.password = '';
        this.model.confirmedPassword = '';
        this.validateAccountService.deleteValidateAccount();
        this.router.navigateByUrl('/');
      },
      error: _ => {
        
      }
    });
  }
}
