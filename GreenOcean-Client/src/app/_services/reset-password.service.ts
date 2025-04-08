import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ResetPassword } from '../_model/resetPassword';
import { BehaviorSubject, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ResetPasswordService {
  baseURL = environment.apiURL;
  private resetPasswordSource = new BehaviorSubject<ResetPassword | null>(null);
  resetPassword$ = this.resetPasswordSource.asObservable();

  constructor(private http: HttpClient) { }

  sendEmail(model: any) {
    return this.http.post<ResetPassword>(this.baseURL + "resetpassword", model).pipe( 
      map((response: ResetPassword) => {
        const reset = response;
          
        if(reset) {
          localStorage.setItem('reset', JSON.stringify(reset));
          this.resetPasswordSource.next(reset);
        }
      })
    )
  }

  confirmCode(model: any) {
    return this.http.post<string>(this.baseURL + "resetpassword/confirmcode", model);
  }

  changePassword(model: any, id: string) {
    return this.http.post<string>(this.baseURL + "resetpassword/confirmcode/changepassword/" + id, model);
  }

  setResetPassword(resetPassword: ResetPassword) {
    this.resetPasswordSource.next(resetPassword);
  }

  deletePasswordReset() {
    localStorage.removeItem('reset');
    this.resetPasswordSource.next(null);
  }
}