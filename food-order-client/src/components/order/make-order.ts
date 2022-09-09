import { Component, OnInit } from '@angular/core';
import { OrderService, PaymentService } from 'services';
import * as braintree from 'braintree-web';

@Component({
  selector: 'make-order',
  templateUrl: './make-order.html',
})
export class MakeOrderComponent implements OnInit {
  inputClass =
    'outline-none border rounded p-1 px-2 shadow-sm hover:border-zinc-300 focus:border-zinc-300 transition-all duration-200';
  token: string = '';
  hostedFields: braintree.HostedFields = {} as braintree.HostedFields;
  constructor(
    private orderService: OrderService,
    private paymentService: PaymentService
  ) {}

  ngOnInit(): void {
    this.generateToken().add(() => {
      this.createBraintreeUI();
    });
  }

  createBraintreeUI() {
    braintree.client.create({ authorization: this.token }).then((client) => {
      braintree.hostedFields
        .create({
          client,
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
    this.hostedFields.tokenize().then((payload) => {
      console.log(payload);
      this.orderService
        .makeOrder({ body: { nonce: payload.nonce } })
        .subscribe({
          next: () => {},
          error: (error) => {
            console.log(error);
          },
        });
    });
  }
}
