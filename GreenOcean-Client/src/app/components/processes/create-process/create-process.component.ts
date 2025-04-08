import { Component } from '@angular/core';
import { GreenhousesService } from 'src/app/_services/greenhouses.service';
import { Router } from '@angular/router';
import { Greenhouse } from 'src/app/_model/greenhouse';
import { Process } from 'src/app/_model/process';
import { ProcessService } from 'src/app/_services/process.service';

@Component({
  selector: 'app-create-process',
  templateUrl: './create-process.component.html',
  styleUrls: ['./create-process.component.css']
})
export class CreateProcessComponent {
  greenhouseId: string | null = null;
  process: any = {};
  greenhouses: Greenhouse[] = [];

  constructor(private router: Router, 
    private processService: ProcessService, private greenhouseService: GreenhousesService) {}

  ngOnInit(): void {
    this.loadGreenhouses();
  }

  loadGreenhouses() {
    this.greenhouseService.getGreenhouses().subscribe({
      next: greenhouses => {
        this.greenhouses = greenhouses
        if (this.greenhouses) {
          this.greenhouseId = greenhouses[0].id;
        }
      }
    });
  }

  addProcess() {
    if (this.greenhouseId) {
      this.process.greenhouseId = this.greenhouseId;
    }

    this.processService.addProcess(this.process).subscribe({
      next: _ => { 
        this.router.navigateByUrl('/processes');
      }
    });
  }
  
}
