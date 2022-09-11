import { Component, Input } from '@angular/core';
import { Order } from 'generated/models';

@Component({
  selector: 'order-list',
  templateUrl: './order-list.html',
})
export class OrderListComponent {
  @Input('items') items: Order[] = [];
}
