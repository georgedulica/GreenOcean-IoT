import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Process } from '../_model/process';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProcessService {

  baseURL: string = environment.apiURL;
  
  constructor(private http: HttpClient) { }

  getProcess(id: string) {
    return this.http.get<Process>(this.baseURL + 'processes/getProcess/' + id);
  }

  getProcesses(greenhouseId: string, timestamp: string) {
    return this.http.get<Process[]>(this.baseURL + 'processes/getProcesses/' + timestamp + '/' + greenhouseId);
  }

  addProcess(process: any) {
    return this.http.post(this.baseURL + 'processes/createProcess', process);
  }

  editProcess(id: string, process: Process) {
    return this.http.put(this.baseURL + 'processes/editProcess/' + id, process);
  }

  deleteProcess(id: string) {
    return this.http.delete(this.baseURL + 'processes/deleteProcess/' + id);
  }
}
