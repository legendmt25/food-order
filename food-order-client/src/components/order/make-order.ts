import { Component, OnInit } from '@angular/core';
import { OrderService, PaymentService, UserAddressService } from 'services';
import * as braintree from 'braintree-web';
import { FormControl } from '@angular/forms';
import { UserAddress } from 'generated/models';

@Component({
  selector: 'make-order',
  templateUrl: './make-order.html',
})
export class MakeOrderComponent implements OnInit {
  inputClass =
    'outline-none border rounded p-1 px-2 shadow-sm hover:border-zinc-300 focus:border-zinc-300 transition-all duration-200';
  token: string = '';
  hostedFields: braintree.HostedFields = {} as braintree.HostedFields;
  addressFormControl = new FormControl<number>(0);
  addressId?: number = undefined;
  addresses: UserAddress[] = [];

  constructor(
    private orderService: OrderService,
    private paymentService: PaymentService,
    public userAddressService: UserAddressService
  ) {}

  ngOnInit(): void {
    this.generateToken().add(() => {
      this.createBraintreeUI();
    });
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

  createBraintreeUI() {
    braintree.client.create({ authorization: this.token }).then((client) => {
      braintree.hostedFields
        .create({
          client,
          styles: {
            form: {
              width: '50%',
            },
            input: {
              'font-size': '16px',
              height: 'auto',
              width: '50%',
            },
          },
          fields: {
            number: {
              selector: '#card-number',
              placeholder: '1111 1111 1111 1111',
            },
            cvv: {
              selector: '#cvv',
              placeholder: '111',
            },
            expirationDate: {
              selector: '#expiration-date',
              placeholder: 'MM/YY',
            },
            cardholderName: {
              selector: '#cardholder-name',
              placeholder: 'Full name',
            },
          },
        })
        .then((hostedFields) => {
          this.hostedFields = hostedFields;
        });
    });
  }

  generateToken() {
    return this.paymentService.getToken$Json().subscribe({
      next: (token) => {
        this.token = token;
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  handleMakeOrder() {
    if (!this.addressId) {
      return;
    }
    this.hostedFields.tokenize().then((payload) => {
      this.orderService
        .makeOrder({
          body: { nonce: payload.nonce },
          addressId: this.addressId,
        })
        .subscribe({
          next: () => {},
          error: (error) => {
            console.log(error);
          },
        });
    });
  }
}
