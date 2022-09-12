import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import * as braintree from 'braintree-web';
import { PaymentService } from 'services';

@Component({
  selector: 'payment',
  templateUrl: './payment.html',
})
export class PaymentComponent implements OnInit {
  inputClass =
    'outline-none border rounded p-1 px-2 shadow-sm hover:border-zinc-300 focus:border-zinc-300 transition-all duration-200';
  @Output('nextStep') nextStep = new EventEmitter();
  @Output('beforeStep') beforeStep = new EventEmitter();
  @Output('onTokenize') onTokenize =
    new EventEmitter<braintree.HostedFieldsTokenizePayload>();
  token: string = '';
  hostedFields: braintree.HostedFields = {} as braintree.HostedFields;

  constructor(private paymentService: PaymentService) {}

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
          styles: {
            input: {
              'font-size': '16px',
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

  tokenize() {
    this.hostedFields.tokenize().then((payload) => {
      this.onTokenize.emit(payload);
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
}
