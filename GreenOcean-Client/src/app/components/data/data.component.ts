import { Component, Output, EventEmitter } from '@angular/core';
import { Greenhouse } from 'src/app/_model/greenhouse';
import { IoTSystem } from 'src/app/_model/iotSystem';
import { DataService } from 'src/app/_services/data.service';
import { GreenhousesService } from 'src/app/_services/greenhouses.service';
import { IoTSystemService } from 'src/app/_services/iot-system.service';
import { DatePipe } from '@angular/common';
import { Data } from 'src/app/_model/data';

@Component({
  selector: 'app-data',
  templateUrl: './data.component.html',
  styleUrls: ['./data.component.css'],
})
export class DataComponent {
  @Output() currentDateEvent: EventEmitter<Date> = new EventEmitter<Date>();

  greenhouses: Greenhouse[] = [];
  greenhouseId: string | null = null;
  iotSystems: IoTSystem[] = [];
  iotSystemId: string | null = null;
  data: Data[] = [];
  storedEvent: any;

  orderByRegistrationTimeButton: boolean = true;
  orderByTemperatureButton: boolean = true;
  orderByHumidityButton: boolean = true;
  orderByLightLevelButton: boolean = true;
  orderBySoilMoistureButton: boolean = true;

  constructor(
    private greenhouseService: GreenhousesService,
    private iotSystemService: IoTSystemService,
    private dataService: DataService
  ) {}

  ngOnInit(): void {
    this.getGreenhouses();
  }

  getGreenhouses() {
    this.greenhouseService.getGreenhouses().subscribe({
      next: (greenhouses) => {
        this.greenhouses = greenhouses;
        if (this.greenhouses.length) {
          this.greenhouseId = this.greenhouses[0].id;
          this.getIoTSystems();
        }
      },
    });
  }

  getIoTSystems() {
    if (this.greenhouseId) {
      this.iotSystemService.getSystems(this.greenhouseId).subscribe({
        next: (iotSystems) => {
          this.iotSystems = iotSystems;
          if (this.iotSystems.length) {
            this.iotSystemId = this.iotSystems[0].id;
          }

          if (this.iotSystemId) {
            const event = this.getEvent();
            this.getData(event);
          }
        }
      });
    }
  }

  getData(event: any) {
    this.storedEvent = event;
    const timestamp = event.target.value;
    
    if (this.iotSystemId) {
      this.dataService.getData(this.iotSystemId, timestamp).subscribe({
        next: (data) => {
          this.data = data;
        },
      });
    }
  }

  deleteData(timestamp: string) {
    this.dataService.deleteData(timestamp).subscribe({
      next: _ => {
        if (this.storedEvent) {
          this.getData(this.storedEvent);
        }
      },
    });
  }

  orderByRegistrationTime() {
    if (this.orderByRegistrationTimeButton === true) {
      this.data.sort((a, b) => a.timestamp.localeCompare(b.timestamp));
      this.orderByRegistrationTimeButton = !this.orderByRegistrationTimeButton;
    } else {
      this.data.sort((a, b) => b.timestamp.localeCompare(a.timestamp));
      this.orderByRegistrationTimeButton = !this.orderByRegistrationTimeButton;
    }
  }

  orderByTemperature() {
    if (this.orderByTemperatureButton === true) {
      this.data.sort((a, b) => a.temperature - b.temperature);
      this.orderByTemperatureButton = !this.orderByTemperatureButton;
    } else {
      this.data.sort((a, b) => b.temperature - a.temperature);
      this.orderByTemperatureButton = !this.orderByTemperatureButton;
    }
  }

  orderByHumidity() {
    if (this.orderByHumidityButton === true) {
      this.data.sort((a, b) => a.humidity - b.humidity);
      this.orderByHumidityButton = !this.orderByHumidityButton;
    } else {
      this.data.sort((a, b) => b.humidity - a.humidity);
      this.orderByHumidityButton = !this.orderByHumidityButton;
    }
  }

  orderByLightLevel() {
    if (this.orderByLightLevelButton === true) {
      this.data.sort((a, b) => a.lightLevel - b.lightLevel);
      this.orderByLightLevelButton = !this.orderByLightLevelButton;
    } else {
      this.data.sort((a, b) => b.lightLevel - a.lightLevel);
      this.orderByLightLevelButton = !this.orderByLightLevelButton;
    }
  }

  orderBySoilMoisture() {
    if (this.orderBySoilMoistureButton === true) {
      this.data.sort((a, b) => a.soilMoisture.localeCompare(b.soilMoisture));
      this.orderBySoilMoistureButton = !this.orderBySoilMoistureButton;
    } else {
      this.data.sort((a, b) => b.soilMoisture.localeCompare(a.soilMoisture));
      this.orderBySoilMoistureButton = !this.orderBySoilMoistureButton;
    }
  }

  getEvent() {
    const currentDate = new Date();
    const formattedDate = this.formatDate(currentDate);
    const event = {
      target: {
        value: formattedDate,
      },
    };

    return event;
  }

  formatDate(date: Date): string {
    const year = date.getFullYear();
    const month = this.padNumber(date.getMonth() + 1);
    const day = this.padNumber(date.getDate());
    return `${year}-${month}-${day}`;
  }

  padNumber(num: number): string {
    return num < 10 ? '0' + num : num.toString();
  }
}