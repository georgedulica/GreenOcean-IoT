import { Directive, Input, ViewContainerRef, TemplateRef } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { take } from 'rxjs';
import { User } from '../_model/user';

@Directive({
  selector: '[role]'
})
export class RoleDirective {
  @Input() role: string | null = null;
  user: User = {} as User;

  constructor(private accountService: AccountService, private viewContainerRef: ViewContainerRef,
    private templateRef: TemplateRef<any>) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) {
          this.user = user;
        }
      }
    });
   }

   ngOnInit(): void {
    if (this.user.role === this.role) {
      this.viewContainerRef.createEmbeddedView(this.templateRef);
    } else {
      this.viewContainerRef.clear();
    }
   }
}
