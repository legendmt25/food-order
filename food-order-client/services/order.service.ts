import { Injectable } from '@angular/core';
import { OrderService } from 'generated/services';

@Injectable({ providedIn: 'root' })
export class CustomOrderService extends OrderService {}
