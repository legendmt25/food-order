import { Component, OnInit } from '@angular/core';
import { Food } from 'generated/models';
import { FoodService } from 'generated/services';

@Component({
  selector: 'app-overview',
  templateUrl: './app-component-overview.html',
})
export class ComponentOverviewComponent implements OnInit {
  foods: Food[] = [];
  constructor(private foodService: FoodService) {}

  ngOnInit(): void {
    this.getFoodEntries();
  }

  getFoodEntries() {
    this.foodService.apiFoodGet$Json().subscribe({
      next: (response) => {
        this.foods = response;
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
}
