import { Injectable } from '@angular/core';
import { FoodAddItemDto, ShoppingCart } from 'generated/models';
import { ShoppingCartService } from 'generated/services';
import { map, ReplaySubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CustomShoppingCartService extends ShoppingCartService {
  private cartSubject = new ReplaySubject<ShoppingCart>(1);

  get cart() {
    return this.cartSubject.asObservable();
  }

  init() {
    this.getShoppingCart();
  }

  getShoppingCart() {
    return super.getShoppingCart$Json().subscribe((value) => {
      this.cartSubject.next(value);
    });
  }

  addFoodItem(body: FoodAddItemDto) {
    return super.addItem({ body }).pipe(
      map(() => {
        this.getShoppingCart();
      })
    );
  }

  removeFoodItem(id: number) {
    return super.removeItem({ id }).pipe(
      map(() => {
        this.getShoppingCart();
      })
    );
  }
}
