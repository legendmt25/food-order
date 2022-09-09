import { Injectable } from '@angular/core';
import { ShoppingCartService } from 'generated/services';

@Injectable({
  providedIn: 'root',
})
export class CustomShoppingCartService extends ShoppingCartService {}
