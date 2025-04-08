import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { GreenhousesService } from 'src/app/_services/greenhouses.service';
import { Greenhouse } from 'src/app/_model/greenhouse';
import { IoTSystemService } from 'src/app/_services/iot-system.service';
import { IoTSystem } from 'src/app/_model/iotSystem';

@Component({
  selector: 'app-systems-list',
  templateUrl: './systems-list.component.html',
  styleUrls: ['./systems-list.component.css'],
})
export class SystemsListComponent {
  greenhouses: Greenhouse[] = [];
  greenhouseId: string | null = null;
  iotSystems: IoTSystem[] = [];
  orderByNameButton: boolean = true;
  orderByTimeStampButton: boolean = true;
  orderByStatusButton: boolean = true;
  orderByMaxTemperatureButton: boolean = true;
  orderByMinTemperatureButton: boolean = true;
  orderByMaxHumidityButton: boolean = true;
  orderByMinHumidityButton: boolean = true;
  orderByMaxLightLevelButton: boolean = true;
  orderByMinLightLevelButton: boolean = true;

  constructor(private router: Router,private iotSystemService: IoTSystemService,
    private greenhouseService: GreenhousesService) {}

  ngOnInit(): void {
    this.loadGreenhouses();
  }

  loadGreenhouses() {
    this.greenhouseService.getGreenhouses().subscribe({
      next: (greenhouses) => {
        this.greenhouses = greenhouses;
        if (this.greenhouses.length) {
          this.greenhouseId = this.greenhouses[0].id;
          this.getSystems();
        }
      },
    });
  }

  getSystems() {
    if (this.greenhouseId) {
    this.iotSystemService.getSystems(this.greenhouseId).subscribe({
      next: iotSystems => {
        this.iotSystems = iotSystems;
      }
     });
    }
  }

  orderByName() {
    if (this.orderByNameButton === true) {
      this.iotSystems.sort((a, b) => a.name.localeCompare(b.name));
      this.orderByNameButton = !this.orderByNameButton;
    }
    else {
      this.iotSystems.sort((a, b) => b.name.localeCompare(a.name));
      this.orderByNameButton = !this.orderByNameButton;
    }
  }

  orderByTimestamp() {
    if (this.orderByTimeStampButton == true) {
      this.iotSystems.sort((a, b) => a.timestamp.localeCompare(b.timestamp));
      this.orderByTimeStampButton = !this.orderByTimeStampButton;
    }
    else {
      this.iotSystems.sort((a, b) => b.timestamp.localeCompare(a.timestamp));
      this.orderByTimeStampButton = !this.orderByTimeStampButton;
    }
  }

  orderByStatus() {
    if (this.orderByStatusButton === true) {
      this.iotSystems.sort((a, b) => a.status.localeCompare(b.status));
      this.orderByStatusButton = !this.orderByStatusButton;
    }
    else {
      this.iotSystems.sort((a, b) => b.status.localeCompare(a.status));
      this.orderByStatusButton = !this.orderByStatusButton;
    }
  }

  orderByMaxTemperature() {
    if (this.orderByMaxTemperatureButton === true) {
      this.iotSystems.sort((a, b) => a.maxTemperature - b.maxTemperature);
      this.orderByMaxTemperatureButton = !this.orderByMaxTemperatureButton;
    }
    else {
      this.iotSystems.sort((a, b) => b.maxTemperature - a.maxTemperature);    
      this.orderByMaxTemperatureButton = !this.orderByMaxTemperatureButton;
    }
  }

  orderByMinTemperature() {
    if (this.orderByMinTemperatureButton === true) {
      this.iotSystems.sort((a, b) => a.minTemperature - b.minTemperature);
      this.orderByMinTemperatureButton = !this.orderByMinTemperatureButton;
    }
    else {
      this.iotSystems.sort((a, b) => b.minTemperature - a.minTemperature);    
      this.orderByMinTemperatureButton = !this.orderByMinTemperatureButton;
    }
  }

  orderByMaxHumidity() {
    if (this.orderByMaxHumidityButton === true) {
      this.iotSystems.sort((a, b) => a.maxHumidity - b.maxHumidity);
      this.orderByMaxHumidityButton = !this.orderByMaxHumidityButton;
    }
    else {
      this.iotSystems.sort((a, b) => b.maxHumidity - a.maxHumidity);    
      this.orderByMaxHumidityButton = !this.orderByMaxHumidityButton;
    }
  }

  orderByMinHumidity() {
    if (this.orderByMinHumidityButton === true) {
      this.iotSystems.sort((a, b) => a.minHumidity - b.minHumidity);
      this.orderByMinHumidityButton = !this.orderByMinHumidityButton;
    }
    else {
      this.iotSystems.sort((a, b) => b.minHumidity - a.minHumidity);    
      this.orderByMinHumidityButton = !this.orderByMinHumidityButton;
    }
  }

  orderByMaxLightLevel() {
    if (this.orderByMaxLightLevelButton === true) {
      this.iotSystems.sort((a, b) => a.maxLightLevel - b.maxLightLevel);
      this.orderByMaxLightLevelButton = !this.orderByMaxLightLevelButton;
    }
    else {
      this.iotSystems.sort((a, b) => b.maxLightLevel - a.maxLightLevel);    
      this.orderByMaxLightLevelButton = !this.orderByMaxLightLevelButton;
    }
  }

  orderByMinLightLevel() {
    if (this.orderByMinLightLevelButton === true) {
      this.iotSystems.sort((a, b) => a.minLightLevel - b.minLightLevel);
      this.orderByMinLightLevelButton = !this.orderByMinLightLevelButton;
    }
    else {
      this.iotSystems.sort((a, b) => b.minLightLevel - a.minLightLevel);    
      this.orderByMinLightLevelButton = !this.orderByMinLightLevelButton;
    }
  }

  editSystem(id: string) {
    this.router.navigateByUrl('systems/editsystem/' + id);
  }

  deleteSystem(id: string) {
    this.iotSystemService.deleteSystem(id).subscribe({
      next: _ => {
        this.iotSystems = this.iotSystems.filter(iotSystem => iotSystem.id !== id);
      }
    });
  }
}
