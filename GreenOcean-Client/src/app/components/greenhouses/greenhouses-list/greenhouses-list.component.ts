import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Greenhouse } from 'src/app/_model/greenhouse';
import { GreenhousesService } from 'src/app/_services/greenhouses.service';

@Component({
  selector: 'app-greenhouses-list',
  templateUrl: './greenhouses-list.component.html',
  styleUrls: ['./greenhouses-list.component.css']
})
export class GreenhousesListComponent {
  greenhouses: Greenhouse[] = [];
  searchQuery: string = '';
  type: string = '';
  orderByNameButton: boolean = true;
  orderByStreetButton: boolean = true;
  orderByStreetNumberButton: boolean = true;
  orderByCityButton: boolean = true;
  constructor(private router: Router, private greenhouseService: GreenhousesService) {}

  ngOnInit(): void {
    this.loadGreenhouses();
  }

    loadGreenhouses() {
      this.greenhouseService.getGreenhouses().subscribe({
        next: greenhouses => {
          this.greenhouses = greenhouses
        }
      });
    }

    deleteGreenhouse(id: string) {
      this.greenhouseService.deleteGreenhouse(id).subscribe({
        next: _ => {
          this.greenhouses = this.greenhouses.filter(g => g.id !== id);
        }
      });
    }

    filteredGreenhouses() {
      this.greenhouses = this.greenhouses.filter(greenhouse =>
        greenhouse.name.toLowerCase().includes(this.searchQuery.toLowerCase())
      );
    }

    orderByName() {
      if (this.orderByNameButton === true) {
        this.greenhouses.sort((a, b) => a.name.localeCompare(b.name));
        this.orderByNameButton = !this.orderByNameButton;
      }
      else {
        this.greenhouses.sort((a, b) => b.name.localeCompare(a.name));
        this.orderByNameButton = !this.orderByNameButton;
      }
    }

    orderByStreet() {
      if (this.orderByStreetButton === true) {
        this.greenhouses.sort((a, b) => a.street.localeCompare(b.street));
        this.orderByStreetButton = !this.orderByStreetButton;
      }
      else {
        this.greenhouses.sort((a, b) => b.street.localeCompare(a.street));
        this.orderByStreetButton = !this.orderByStreetButton;
      }
    }

    orderByStreetNumber() {
      if (this.orderByStreetNumberButton === true) {
        this.greenhouses.sort((a, b) => a.number - b.number);
        this.orderByStreetNumberButton = !this.orderByStreetNumberButton;
      }
      else {
        this.greenhouses.sort((a, b) => b.number - a.number);
        this.orderByStreetNumberButton = !this.orderByStreetNumberButton;
      }
    }

    orderByCity() {
      if (this.orderByCityButton === true) {
        this.greenhouses.sort((a, b) => a.city.localeCompare(b.city));
        this.orderByCityButton = !this.orderByCityButton;
      }
      else {
        this.greenhouses.sort((a, b) => b.city.localeCompare(a.city));
        this.orderByCityButton = !this.orderByCityButton;
      }
    }

    editGreenhouse(id: string) {
      this.router.navigate(['/greenhouses/editgreenhouse', id]);
    }
}