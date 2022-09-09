import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Food, FoodCategory } from 'generated/models';
import { FoodService } from 'services';

@Component({
  selector: 'food-overview',
  templateUrl: './food-overview.html',
})
export class FoodOverviewComponent {
  foods: Food[] = [];
  filteredFoods: Food[] = [];
  isLoading: boolean = false;

  constructor(private foodService: FoodService, route: ActivatedRoute) {
    route.params.subscribe({
      next: (params) => {
        this.isLoading = true;
        const type = params['type'];
        this.getFoodEntries(type);
      },
    });
  }

  handleSearchChange(event: Event) {
    const target = event.target as HTMLInputElement;
    this.filteredFoods = this.foods.filter(
      (food) =>
        target.value === '' || food.name?.toLowerCase().includes(target.value)
    );
  }

  getFoodEntries(type: string) {
    return this.foodService
      .filterByCategory(type.toUpperCase() as FoodCategory)
      .subscribe({
        next: (response) => {
          this.foods = response;
          this.filteredFoods = this.foods;
        },
        error: (error) => {
          console.log(error);
        },
      })
      .add(() => {
        this.isLoading = false;
      });
  }
}
