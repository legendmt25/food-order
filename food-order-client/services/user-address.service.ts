import { Injectable } from '@angular/core';
import { UserAddressService } from 'generated/services';

@Injectable({ providedIn: 'root' })
export class CustomUserAddressService extends UserAddressService {}
