import { Component } from '@angular/core';
import { AccountService } from 'src/app/_services/account.service';
import { User } from 'src/app/_model/user';
import { Observable, of } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
  collapsed: boolean = true;
  currentUser$: Observable<User | null> = of(null);

  constructor(private accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$;
  }
  
  toggleCollapsed(): void {
    this.collapsed = !this.collapsed;
  }

  logout(){
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}