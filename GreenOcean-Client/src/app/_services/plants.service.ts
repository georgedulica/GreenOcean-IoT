import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Plant } from '../_model/plant';

@Injectable({
  providedIn: 'root'
})
export class PlantsService {
  baseURL: string = environment.apiURL;
  constructor(private http: HttpClient) {}

  getPlant(id: string) {
    return this.http.get<Plant>(this.baseURL + 'plants/getplant/' + id);
  }

  getPlants(id: string) {
    return this.http.get<Plant[]>(this.baseURL + 'plants/getplants/' + id);
  }

  addPlant(plant: any) {
    return this.http.post(this.baseURL + 'plants/createplant', plant);
  }

  editPlant(plant: Plant, id: string) {
    return this.http.put(this.baseURL + 'plants/editplant/' + id, plant);
  }

  deletePlant(id: string) {
    return this.http.delete(this.baseURL + 'plants/deleteplant/' + id);
  }
}