import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ShoppingCartService } from 'services';

@Component({
  selector: 'shopping-cart-modal',
  templateUrl: './shopping-cart-modal.html',
})
export class ShoppingCartModalComponent {
  @Input('show') showModal: boolean = false;
  @Output('onToggle') toggleEvent = new EventEmitter();

  constructor(public shoppingCartService: ShoppingCartService) {}

  handleToggle() {
    this.toggleEvent.emit();
  }

  handleRemoveItem(id?: number | null) {
    if (!id) {
      return;
    }
    this.shoppingCartService.removeFoodItem(id).subscribe({
      error: (error) => {
        console.log(error);
      },
    });
  }
}
