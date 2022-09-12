import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import * as braintree from 'braintree-web';
import { UserAddress } from 'generated/models';
import { OrderService, UserAddressService } from 'services';

@Component({
  selector: 'make-order',
  templateUrl: './make-order.html',
})
export class MakeOrderComponent implements OnInit {
  inputClass =
    'outline-none border rounded p-1 px-2 shadow-sm hover:border-zinc-300 focus:border-zinc-300 transition-all duration-200';
  addressFormControl = new FormControl<number>(0);
  addressId?: number = undefined;
  addresses: UserAddress[] = [];
  currentStep: number = 1;

  constructor(
    private orderService: OrderService,
    public userAddressService: UserAddressService
  ) {}

  ngOnInit(): void {
    this.getAddressEntries();
    this.addressFormControl.valueChanges.subscribe({
      next: (addressId) => {
        this.addressId = addressId as number;
      },
    });
  }

  getAddressEntries() {
    this.userAddressService.getAddressEntries$Json().subscribe({
      next: (addresses) => {
        this.addresses = addresses;
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  handleMakeOrder(payload: braintree.HostedFieldsTokenizePayload) {
    if (!this.addressId) {
      return;
    }
    this.orderService
      .makeOrder({
        body: { nonce: payload?.nonce },
        addressId: this.addressId,
      })
      .subscribe({
        next: () => {},
        error: (error) => {
          console.log(error);
        },
      });
  }

  nextStep() {
    if (!this.addressId) {
      return;
    }
    this.currentStep++;
  }
  beforeStep() {
    this.currentStep--;
  }
}
