import { Component, OnInit } from '@angular/core';
import { Food } from 'generated/models';
import { FoodService } from 'generated/services';

@Component({
  selector: 'food-overview',
  templateUrl: './food-overview.html',
  styleUrls: ['./food-overview.css'],
})
export class FoodOverviewComponent implements OnInit {
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
