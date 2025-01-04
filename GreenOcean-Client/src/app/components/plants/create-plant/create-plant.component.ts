import { Component } from '@angular/core';
import { Plant } from 'src/app/_model/plant';
import { Greenhouse } from 'src/app/_model/greenhouse';
import { GreenhousesService } from 'src/app/_services/greenhouses.service';
import { PlantsService } from 'src/app/_services/plants.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-plant',
  templateUrl: './create-plant.component.html',
  styleUrls: ['./create-plant.component.css'],
})

export class CreatePlantComponent {
  plant: any = {};
  greenhouses: Greenhouse[] = [];

  constructor(private plantService: PlantsService,
    private greenhouseService: GreenhousesService, private router: Router) {}

  ngOnInit(): void {
    this.loadGreenhouses();
  }

  addPlant() {
    this.plantService.addPlant(this.plant).subscribe({
      next: _ => {
        this.router.navigateByUrl('/plants');
      }
    })
  }

  loadGreenhouses() {
    this.greenhouseService.getGreenhouses().subscribe({
      next: (greenhouses) => {
        this.greenhouses = greenhouses;
      },
    });
  }
}
