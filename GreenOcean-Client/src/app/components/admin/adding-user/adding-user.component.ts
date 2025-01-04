import { Component, OnInit } from '@angular/core';
import { UserRole } from 'src/app/_model/userRole';
import { AddingUserService } from 'src/app/_services/adding-user.service';
@Component({
  selector: 'app-adding-user',
  templateUrl: './adding-user.component.html',
  styleUrls: ['./adding-user.component.css']
})
export class AddingUserComponent implements OnInit{

  userRoles: string[] = [];
  userRole: string | null = null;
  user: any = {};
  
  constructor(private addingUserService: AddingUserService) { }
  
  ngOnInit(): void {
    this.getUserRoles();
  }

  getUserRoles() {
    this.addingUserService.getUserRoles().subscribe({
      next: userRoles => {
        this.userRoles = userRoles;
        if (userRoles.length) {
          this.userRole = userRoles[0];
        }
      }
    })
  }

  createUser(user: any) {
    this.addingUserService.createUser(user).subscribe({
      next: _ => {

      },
      error: error => {
        if (error.error === "This email already exists") {
          
        }
      }
    })
  }
}
