import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { User } from '../_model/user';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private baseURL: string = environment.apiURL;
  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();
  
  constructor(private http: HttpClient) {}

  login(model: any){
    return this.http.post<User>(this.baseURL + "login", model).pipe( 
        map(( response: User ) =>{
        const user = response;
        if(user) {
          const role = this.getDecodedToken(user.token).role;
          user.role = role;
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
     })
    )
  }
  
  setCurrentUser(user: User){
    this.currentUserSource.next(user);
  }

  logout(){
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }

  getDecodedToken(token: string) {
    return JSON.parse(atob(token.split('.')[1]));
  }
}
