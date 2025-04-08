import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Greenhouse } from 'src/app/_model/greenhouse';
import { Plant } from 'src/app/_model/plant';
import { GreenhousesService } from 'src/app/_services/greenhouses.service';
import { PlantsService } from 'src/app/_services/plants.service';

@Component({
  selector: 'app-plants-list',
  templateUrl: './plants-list.component.html',
  styleUrls: ['./plants-list.component.css']
})
export class PlantsListComponent {
  greenhouses: Greenhouse[] = [];
  plants: Plant[] = [];
  greenhouseId: string | null = null;

  constructor(private router: Router, 
    private greenhouseService: GreenhousesService, private plantService: PlantsService) {}

  ngOnInit(): void {
    this.loadGreenhouses();
  }

  loadGreenhouses() {
    this.greenhouseService.getGreenhouses().subscribe({
      next: greenhouses => {
        this.greenhouses = greenhouses
        if (this.greenhouses.length) {
          this.greenhouseId = this.greenhouses[0].id;
          this.getPlants();
        }
      }
    });
  }

  getPlants() {
    if (this.greenhouseId) {
    this.plantService.getPlants(this.greenhouseId).subscribe({
      next: plants => {
        this.plants = plants
      }
    });
  }
}

  editPlant(id: string) {
    this.router.navigateByUrl('plants/editplant/' + id);
  }

  deletePlant(id: string) {
    this.plantService.deletePlant(id).subscribe({
      next: _ => {
        this.plants = this.plants.filter(p => p.id !== id);
      }
    });
  }
}