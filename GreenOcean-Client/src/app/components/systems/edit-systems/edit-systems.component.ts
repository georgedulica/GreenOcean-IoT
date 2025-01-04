import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IoTSystemService } from 'src/app/_services/iot-system.service';
import { GreenhousesService } from 'src/app/_services/greenhouses.service';
import { IoTSystem } from 'src/app/_model/iotSystem';
import { Greenhouse } from 'src/app/_model/greenhouse';

@Component({
  selector: 'app-edit-systems',
  templateUrl: './edit-systems.component.html',
  styleUrls: ['./edit-systems.component.css']
})
export class EditSystemsComponent {
  iotSystem: IoTSystem | null = null;
  greenhouses: Greenhouse[] = [];

  constructor(private router: Router, private route: ActivatedRoute,
    private iotSystemService: IoTSystemService, private greenhouseService: GreenhousesService) {}

  ngOnInit(): void {
    this.loadSystem();
    this.loadGreenhouses();
  }

  loadGreenhouses() {
    this.greenhouseService.getGreenhouses().subscribe({
      next: greenhouses => {
        this.greenhouses = greenhouses
      }
    });
  }

  loadSystem() {
    const id = this.route.snapshot.params['id'];
    this.iotSystemService.getSystem(id).subscribe({
      next: iotSystem => {
        this.iotSystem = iotSystem;
      }
    })
  }

  editSystem() {
    const id = this.route.snapshot.params['id'];
    if (this.iotSystem) {
      this.iotSystemService.editSystem(this.iotSystem, id).subscribe({
        next: _ => {
          this.router.navigateByUrl('systems');
        },
      });
    }
  }
}
