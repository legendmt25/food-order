import { Component, OnInit } from '@angular/core';
import { Order } from 'generated/models';
import { OrderService } from 'services';

@Component({
  selector: 'order-overview',
  templateUrl: './order-overview.html',
})
export class OrderOverviewComponent implements OnInit {
  orders: Order[] = [];
  isLoading: boolean = false;

  constructor(private orderService: OrderService) {}

  ngOnInit(): void {
    this.getOrders();
  }

  getOrders() {
    this.isLoading = true;
    this.orderService
      .getOrders$Json()
      .subscribe({
        next: (response) => {
          this.orders = response;
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
