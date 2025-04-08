import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Greenhouse } from 'src/app/_model/greenhouse';
import { Plant } from 'src/app/_model/plant';
import { GreenhousesService } from 'src/app/_services/greenhouses.service';
import { PhotoService } from 'src/app/_services/photo.service';
import { PlantsService } from 'src/app/_services/plants.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-edit-plant',
  templateUrl: './edit-plant.component.html',
  styleUrls: ['./edit-plant.component.css'],
})
export class EditPlantComponent {
  basicPhotoURL: string = environment.basicPhotoURL;
  buttonDeleteValue: boolean = false;
  greenhouses: Greenhouse[] = [];
  plant: Plant | null = null;

  constructor(private router: Router, private route: ActivatedRoute,
    private greenhouseService: GreenhousesService, private plantService: PlantsService,
    private photoService: PhotoService) {}

  ngOnInit() {
    this.loadGreenhouses();
    this.getPlant();
  }

  loadGreenhouses() {
    this.greenhouseService.getGreenhouses().subscribe({
      next: (greenhouses) => {
        this.greenhouses = greenhouses;
      },
    });
  }

  getPlant() {
    const id = this.route.snapshot.params['id'];
    this.plantService.getPlant(id).subscribe({
      next: (plant) => {
        this.plant = plant;
        if (this.plant?.photoURL !== this.basicPhotoURL) {
          this.buttonDeleteValue = true;
        }
      },
    });
  }

  editPlant() {
    const id = this.route.snapshot.params['id'];
    
    if (this.plant) {
      this.plantService.editPlant(this.plant, id).subscribe({
        next: _ => {
          this.router.navigateByUrl('/plants');
        },
      });
    }
  }
  
  changePhoto(event: any) {
    const id = this.route.snapshot.params['id'];
    const file = event.target.files[0];
    const formData = new FormData();

    if (file) {
      formData.append('file', file);
    }

    this.photoService.changePhoto(formData, id).subscribe({
      next: _ => {
        this.getPlant();
      }
    });
  }

  deletePhoto() {
    const id = this.route.snapshot.params['id'];
    
    this.photoService.deletePhoto(id).subscribe({
      next: _ => {
        this.getPlant();
      }
    })
  }
}
