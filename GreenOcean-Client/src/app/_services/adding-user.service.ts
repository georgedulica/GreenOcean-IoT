import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserRole } from '../_model/userRole';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})

export class AddingUserService {
  baseURL: string = environment.apiURL;
  constructor(private http: HttpClient) { }

  getUserRoles() {
    return this.http.get<string[]>(this.baseURL + 'getUserRoles');
  }

  createUser(user: any) {
    return this.http.post(this.baseURL + 'createUser', user);
  }
}
