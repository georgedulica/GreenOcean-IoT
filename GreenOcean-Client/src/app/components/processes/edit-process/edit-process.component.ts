import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { Greenhouse } from 'src/app/_model/greenhouse';
import { Process } from 'src/app/_model/process';
import { GreenhousesService } from 'src/app/_services/greenhouses.service';
import { ProcessService } from 'src/app/_services/process.service';

@Component({
  selector: 'app-edit-process',
  templateUrl: './edit-process.component.html',
  styleUrls: ['./edit-process.component.css'],
})
export class EditProcessComponent {
  greenhouses: Greenhouse[] = [];
  process: Process | null = null;
  greenhouseId: string | null = null;

  constructor(
    private greenhouseService: GreenhousesService,
    private processService: ProcessService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.getGreenhouses();
    this.getProcess();
  }

  getGreenhouses() {
    this.greenhouseService.getGreenhouses().subscribe({
      next: (greenhouses) => {
        this.greenhouses = greenhouses;
      },
    });
  }

  getProcess() {
    const id = this.route.snapshot.params['id'];
    this.processService.getProcess(id).subscribe({
      next: process => {
        this.process = process;
      },
    });
  }

  editProcess() {
    const id = this.route.snapshot.params['id'];
    if (this.process) {
      this.processService.editProcess(id, this.process).subscribe({
        next: _ => {
          this.router.navigateByUrl('/processes');
        },
      });
    }
  }
}