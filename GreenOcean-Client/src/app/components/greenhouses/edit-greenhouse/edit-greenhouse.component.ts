import { Component } from '@angular/core';
import { GreenhousesService } from 'src/app/_services/greenhouses.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Greenhouse } from 'src/app/_model/greenhouse';
@Component({
  selector: 'app-edit-greenhouse',
  templateUrl: './edit-greenhouse.component.html',
  styleUrls: ['./edit-greenhouse.component.css']
})
export class EditGreenhouseComponent {

  greenhouse: Greenhouse | null = null;

  constructor(private greenhouseService: GreenhousesService, private router: Router, 
    private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.loadGreenhouse();
  }

  editGreenhouse() {
    if (this.greenhouse) {
      const id = this.route.snapshot.params['id'];

      this.greenhouseService.editGreenhouse(this.greenhouse, id).subscribe({
        next: _ => {
          this.router.navigateByUrl('/greenhouses');
        }
      });
    } 
  }

  loadGreenhouse() {
    const id = this.route.snapshot.params['id'];

    this.greenhouseService.getGreenhouse(id).subscribe({
      next: greenhouse => 
      {
        this.greenhouse = greenhouse
      }
    });
  }
}
