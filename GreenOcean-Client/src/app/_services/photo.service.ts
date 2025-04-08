import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PhotoService {
  baseURL: string = environment.apiURL;

  constructor(private http: HttpClient) {}

  changePhoto(formData: FormData, id: string) {
    return this.http.post(this.baseURL + 'plantphoto/changephtoto/' + id, formData);
  }

  deletePhoto(id: string) {
    return this.http.delete(this.baseURL + 'plantphoto/deletephtoto/' + id);
  }
}
