import { Injectable } from '@angular/core';
import { FoodCategory } from 'generated/models';
import { FoodService } from 'generated/services';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class CustomFoodService extends FoodService {
  filterByCategory(category: FoodCategory) {
    return this.getFoodEntries$Json().pipe(
      map((entries) => entries.filter((entry) => entry.category === category))
    );
  }

  getPizzas() {
    return this.filterByCategory(FoodCategory.Pizza);
  }

  getSandwiches() {
    return this.filterByCategory(FoodCategory.Sandwich);
  }

  getDrinks() {
    return this.filterByCategory(FoodCategory.Drink);
  }
}
