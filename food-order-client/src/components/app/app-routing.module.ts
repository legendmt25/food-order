import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FoodOverviewComponent } from '../food/food-overview';
import { FoodPageComponent } from '../food/food-page';
import { LoginComponent } from '../login/login';
import { RegisterComponent } from '../register/register';
import { ShoppingCartOverviewComponent } from '../shopping-cart/shopping-cart-overview';

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
    component: ShoppingCartOverviewComponent,
    path: 'cart'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
