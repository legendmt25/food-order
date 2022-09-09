import { Injectable } from '@angular/core';
import { PaymentService } from 'generated/services';

@Injectable({ providedIn: 'root' })
export class CustomPaymentService extends PaymentService {}
