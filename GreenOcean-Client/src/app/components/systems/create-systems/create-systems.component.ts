import { Component } from '@angular/core';
import { IoTSystem } from 'src/app/_model/iotSystem';
import { Router } from '@angular/router';
import { GreenhousesService } from 'src/app/_services/greenhouses.service';
import { IoTSystemService } from 'src/app/_services/iot-system.service';
import { Greenhouse } from 'src/app/_model/greenhouse';

@Component({
  selector: 'app-create-systems',
  templateUrl: './create-systems.component.html',
  styleUrls: ['./create-systems.component.css']
})

export class CreateSystemsComponent {
  iotSystem: any = {};
  greenhouses: Greenhouse[] = [];

  constructor(private router: Router, 
    private iotSystemService: IoTSystemService, private greenhouseService: GreenhousesService) {}

  ngOnInit(): void {
    this.loadGreenhouses();
  }

  loadGreenhouses() {
    this.greenhouseService.getGreenhouses().subscribe({
      next: greenhouses => {
        this.greenhouses = greenhouses
      }
    });
  }

  addSystem() {
    this.iotSystemService.addSystem(this.iotSystem).subscribe({
      next: _ => {
        this.router.navigateByUrl('/systems');
      },
      error: _ => {
        
      }
    });
  }
}