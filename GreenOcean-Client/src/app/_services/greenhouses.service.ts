import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Greenhouse } from '../_model/greenhouse';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GreenhousesService {
  baseURL:string = environment.apiURL;

  constructor(private http: HttpClient) { }

  getGreenhouses() {
    const username = this.getUsername();
    return this.http.get<Greenhouse[]>(this.baseURL + 'greenhouses/' + username);
  }

  getGreenhouse(id: string) {
    return this.http.get<Greenhouse>(this.baseURL + 'greenhouses/greenhouse/' + id);
  }
  
  createGreenhouse(model: any) {
    const username = this.getUsername();
    return this.http.post(this.baseURL + 'greenhouses/creategreenhouse/' + username, model);
  }

   editGreenhouse(greenhouse: Greenhouse, id: string){
    return this.http.put(this.baseURL + 'greenhouses/editgreenhouse/' + id, greenhouse);
  }

  deleteGreenhouse(id: string){
    return this.http.delete(this.baseURL + 'greenhouses/deletegreenhouse/' + id);
  }

  getUsername() {
    const userString = localStorage.getItem('user');
    if (!userString) return;

    const user = JSON.parse(userString);
    return user.username;
  }
}
