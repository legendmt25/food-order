import { Component, Input } from '@angular/core';
import { Food } from 'generated/models';

@Component({
  selector: 'food-list',
  templateUrl: './food-list.html',
})
export class FoodListComponent {
    @Input('items') items: Food[] = [];
    
}
