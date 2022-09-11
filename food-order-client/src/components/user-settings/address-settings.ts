import { Component, OnInit } from '@angular/core';
import { UserAddress } from 'generated/models';
import { Observable } from 'rxjs';
import { UserAddressService } from 'services';

@Component({
  selector: 'address-settings',
  templateUrl: './address-settings.html',
})
export class AddressSettingsComponent implements OnInit{
  addresses: UserAddress[] = [];
  form: UserAddress = {};
  showForm: boolean = false;
  id?: number | null = null;

  constructor(private userAddressService: UserAddressService) {}

  ngOnInit(): void {
    this.getAddreses();
  }
  getAddreses() {
    this.userAddressService.getAddressEntries$Json().subscribe({
      next: (addresses) => {
        this.addresses = addresses;
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  handleRemoveAddress(id?: number | null) {
    if (!id) {
      return;
    }
    this.userAddressService.deleteAddressEntry({ id }).subscribe({
      next: () => {
        this.getAddreses();
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  handleAddAddress() {
    this.userAddressService.saveAddressEntry$Json({ body: {} }).subscribe({
      next: () => {
        this.getAddreses();
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  handleEditAddress(id?: number | null) {
    if (!id) {
      return;
    }
    this.userAddressService.editAddressEntry$Json({ body: {}, id }).subscribe({
      next: () => {
        this.getAddreses();
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  handleToggleForm() {
    this.showForm = !this.showForm;
  }
  handleSaveClick() {
    this.id = null;
    this.handleToggleForm();
  }
  handleEditClick(addressId?: number | null) {
    const address = this.addresses.find((address) => address.id === addressId);
    this.form.address = address?.address;
    this.form.city = address?.city;
    this.form.municipality = address?.municipality;
    this.id = addressId;
    this.handleToggleForm();
  }

  handleCancelClick() {
    this.id = null;
    this.form = {};
    this.handleToggleForm();
  }

  handleSubmit() {
    let observable: Observable<UserAddress>;
    if (this.id) {
      observable = this.userAddressService.editAddressEntry$Json({
        id: this.id,
        body: this.form,
      });
    } else {
      observable = this.userAddressService.saveAddressEntry$Json({
        body: this.form,
      });
    }
    observable.subscribe({
      next: () => {
        this.getAddreses();
        this.handleCancelClick();
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
}
