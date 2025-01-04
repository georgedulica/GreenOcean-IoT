import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ValidateAccount } from '../_model/validateAccount';
import { BehaviorSubject, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ValidateAccountService {
  baseURL: string = environment.apiURL;
  private validateAccountSource = new BehaviorSubject<ValidateAccount | null>(null);
  validateAccount$ = this.validateAccountSource.asObservable();

  constructor(private http: HttpClient) { }

  confirmCode(model: any, id: string) {
    return this.http.post<ValidateAccount>(this.baseURL + "validateaccount/confirmcode/" + id, model).pipe( 
        map((response: ValidateAccount) => {
          const validate = response;
          
          if(validate) {
          localStorage.setItem('validate', JSON.stringify(validate));
          this.validateAccountSource.next(validate);
        }
      })
    )
  }

  validateAccount(model: any, id: string) {
    return this.http.put(this.baseURL + "validateaccount/" + id, model)
  }

  setValidateAccount(validateAccount: ValidateAccount) {
    this.validateAccountSource.next(validateAccount);
  }

  deleteValidateAccount() {
    localStorage.removeItem('validate');
    this.validateAccountSource.next(null);
  }
}
