import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from 'src/app/_model/user';
import { AccountService } from 'src/app/_services/account.service';
import { of } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  invalidUsernameOrPassword: boolean = false;
  model: any = {}
  currentUser$: Observable<User | null> = of(null);

  constructor(private accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$;
  }

  login() {
    this.accountService.login(this.model).subscribe({
      next: _ => {
        this.model.username = '';
        this.model.password = '';
        this.router.navigateByUrl('/');
      },
      error: _ => {
        this.invalidUsernameOrPassword = true;
      } 
    });
  }
}
