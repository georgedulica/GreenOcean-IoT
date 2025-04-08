import { Component } from '@angular/core';
import { GreenhousesService } from 'src/app/_services/greenhouses.service';
import { ProcessService } from 'src/app/_services/process.service';
import { Router } from '@angular/router';
import { Greenhouse } from 'src/app/_model/greenhouse';
import { Process } from 'src/app/_model/process';

@Component({
  selector: 'app-processes-list',
  templateUrl: './processes-list.component.html',
  styleUrls: ['./processes-list.component.css'],
})
export class ProcessesListComponent {
  greenhouseId: string | null = null;
  greenhouses: Greenhouse[] = [];
  processes: Process[] = [];
  storedEvent: any;

  orderByNameButton: boolean = true;
  orderByDescriptionButton: boolean = true;
  orderByDateButton: boolean = true;

  constructor(
    private router: Router,
    private processService: ProcessService,
    private greenhouseService: GreenhousesService
  ) {}

  ngOnInit(): void {
    this.loadGreenhouses();
  }

  loadGreenhouses() {
    this.greenhouseService.getGreenhouses().subscribe({
      next: (greenhouses) => {
        this.greenhouses = greenhouses;
        if (this.greenhouses.length) {
          this.greenhouseId = this.greenhouses[0].id;
          
          const event = this.getEvent();
          this.getProcesses(event);
        }
      },
    });
  }

  getProcesses(event: any) {
    this.storedEvent = event;
    const timestamp = event.target.value;
    if (this.greenhouseId) {
      this.processService.getProcesses(this.greenhouseId, timestamp).subscribe({
        next: (processes) => {
          this.processes = processes;
        },
      });
    }
  }

  editProcess(id: string) {
    this.router.navigateByUrl('/processes/editprocess/' + id);
  }

  deleteProcess(id: string) {
    this.processService.deleteProcess(id).subscribe({
      next: _ => {
        if (this.storedEvent) {
          this.getProcesses(this.storedEvent);
        }
      },
    });
  }

  orderByName() {
    if (this.orderByNameButton) {
      this.processes.sort((a, b) => a.processName.localeCompare(b.processName));
      this.orderByNameButton = !this.orderByNameButton;
    }
    else {
      this.processes.sort((a, b) => b.processName.localeCompare(a.processName));
      this.orderByNameButton = !this.orderByNameButton;
    }
  }

  orderByDescription() {
    if (this.orderByDescriptionButton) {
      this.processes.sort((a, b) => a.description.localeCompare(b.description));
      this.orderByDescriptionButton = !this.orderByDescriptionButton;
    }
    else {
      this.processes.sort((a, b) => b.description.localeCompare(a.description));
      this.orderByDescriptionButton = !this.orderByDescriptionButton;
    }
  }

  orderByDate() {

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