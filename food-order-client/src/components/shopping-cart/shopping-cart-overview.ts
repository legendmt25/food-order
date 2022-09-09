import { Component, OnInit } from '@angular/core';
import { ShoppingCart } from 'generated/models';
import { ShoppingCartService } from 'services';

@Component({
  selector: 'shopping-cart-overview',
  templateUrl: './shopping-cart-overview.html',
})
export class ShoppingCartOverviewComponent implements OnInit {
  cart: ShoppingCart = {};
  constructor(public shoppingCartService: ShoppingCartService) {}

  ngOnInit(): void {
    this.getShoppingCartEntry();
  }

  handleRemoveItem(id?: number | null) {
    if (!id) {
      return;
    }
    this.shoppingCartService.removeItem({ id }).subscribe({
      next: () => {
        if (!this.cart.foodCartEntry) {
          return;
        }
        this.cart.foodCartEntry.items = this.cart.foodCartEntry?.items?.filter(
          (item) => item.id !== id
        );
      },
    });
  }

  getShoppingCartEntry() {
    this.shoppingCartService.getShoppingCart$Json().subscribe({
      next: (cart) => {
        this.cart = cart;
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
}
