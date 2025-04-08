import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ValidateAccountService } from 'src/app/_services/validate-account.service';


@Component({
  selector: 'app-confirm-code-validate-account',
  templateUrl: './confirm-code-validate-account.component.html',
  styleUrls: ['./confirm-code-validate-account.component.css']
})
export class ConfirmCodeValidateAccountComponent {
  model: any = {};
  invalidCode: boolean = false;
  constructor(private route: ActivatedRoute, private router: Router,
    private validateAccountService: ValidateAccountService) {}

    confirmCode() {
      const id = this.route.snapshot.params['id'];

      this.validateAccountService.confirmCode(this.model, id).subscribe({
        next: response => {
          this.model.code = '';
          this.router.navigate(['/validateaccount/' + id]);
        },
        error: _ => {
          this.invalidCode = true;
        }
      });
    }
}
