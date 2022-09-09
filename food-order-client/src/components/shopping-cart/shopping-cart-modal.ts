import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ShoppingCart } from 'generated/models';
import { ShoppingCartService } from 'services';

@Component({
  selector: 'shopping-cart-modal',
  templateUrl: './shopping-cart-modal.html',
})
export class ShoppingCartModalComponent implements OnInit {
  @Input('show') showModal: boolean = false;
  @Output('onToggle') toggleEvent = new EventEmitter();
  isLoading: boolean = false;
  cart: ShoppingCart = {};
  constructor(public shoppingCartService: ShoppingCartService) {}

  ngOnInit(): void {
    this.getShoppingCartEntry();
  }
  
  handleToggle() {
    this.toggleEvent.emit();
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
    this.isLoading = true;
    this.shoppingCartService
      .getShoppingCart$Json()
      .subscribe({
        next: (cart) => {
          this.cart = cart;
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
