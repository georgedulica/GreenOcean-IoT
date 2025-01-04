import { Component } from '@angular/core';
import { GreenhousesService } from 'src/app/_services/greenhouses.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-create-greenhouse',
  templateUrl: './create-greenhouse.component.html',
  styleUrls: ['./create-greenhouse.component.css']
})
export class CreateGreenhouseComponent {
  invalidGreenhouse: boolean = false;
  model: any = {};

  constructor(private greenhouseService: GreenhousesService, private router: Router) {}

  addGreenhouse() {
    this.greenhouseService.createGreenhouse(this.model).subscribe({
      next: _ => {
        const username = this.greenhouseService.getUsername();
        this.router.navigateByUrl('/greenhouses');
      },
      error: _ => {
        this.invalidGreenhouse = true;
      }
    });
  }
}
