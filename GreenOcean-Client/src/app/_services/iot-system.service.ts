import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IoTSystem } from '../_model/iotSystem';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class IoTSystemService {
  baseURL: string = environment.apiURL;

  constructor(private http: HttpClient) { }

  getSystem(id: string) {
    return this.http.get<IoTSystem>(this.baseURL + 'iotSystems/getSystem/' + id);
  }

  getSystems(id: string) {
    return this.http.get<IoTSystem[]>(this.baseURL + 'iotSystems/getSystems/' + id);
  }

  addSystem(iotSystem: any) {
    return this.http.post(this.baseURL + 'iotSystems/addSystem', iotSystem);
  }

  editSystem(iotSystem: IoTSystem, id: string) {
    return this.http.put(this.baseURL + 'iotSystems/editsystem/' + id, iotSystem);
  }

  deleteSystem(id: string) {
    return this.http.delete(this.baseURL + 'iotSystems/deleteSystem/' + id);
  }
}
