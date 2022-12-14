import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ErrorComponent } from 'src/components/error/error';
import { FoodOverviewComponent } from 'src/components/food/food-overview';
import { FoodPageComponent } from 'src/components/food/food-page';
import { LoginComponent } from 'src/components/login/login';
import { MakeOrderComponent } from 'src/components/order/make-order';
import { OrderOverviewComponent } from 'src/components/order/order-overview';
import { RegisterComponent } from 'src/components/register/register';
import { ShoppingCartModalComponent } from 'src/components/shopping-cart/shopping-cart-modal';
import { AddressSettingsComponent } from 'src/components/user-settings/address-settings';
import { UserSettingsComponent } from 'src/components/user-settings/user-settings';

const routes: Routes = [
  {
    component: FoodOverviewComponent,
    path: 'food/:type',
  },
  {
    component: FoodPageComponent,
    path: 'food-detail/:id',
  },
  {
    component: LoginComponent,
    path: 'login',
  },
  {
    component: RegisterComponent,
    path: 'register',
  },
  {
    component: ShoppingCartModalComponent,
    path: 'cart',
  },
  {
    component: OrderOverviewComponent,
    path: 'my-orders',
  },
  {
    component: MakeOrderComponent,
    path: 'make-order',
  },
  {
    component: UserSettingsComponent,
    path: 'settings',
    children: [
      {
        component: AddressSettingsComponent,
        path: 'address',
      },
    ],
  },

  {
    component: ErrorComponent,
    path: '*',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
