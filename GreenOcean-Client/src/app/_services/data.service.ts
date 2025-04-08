import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Data } from 'src/app/_model/data';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  baseURL: string = environment.apiURL;

  constructor(private http: HttpClient) {}

  getData(id: string, timestamp: string) {
    return this.http.get<Data[]>(this.baseURL + 'sensorData/getData/' + timestamp + '/' + id);
  }

  deleteData(timestamp: string) {
    return this.http.delete(this.baseURL + 'sensorData/deleteData/' + timestamp)
  }
}
