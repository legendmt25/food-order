import { forwardRef, NgModule, Provider } from '@angular/core';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from 'modules/app-routing.module';

import { AppComponent } from 'src/components//app/app.component';
import { FoodOverviewComponent } from 'src/components/food/food-overview';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ApiModule } from 'generated/api.module';
import { FoodPageComponent } from 'src/components/food/food-page';
import { NavbarComponent } from 'src/components/nav/nav';
import { LoginComponent } from 'src/components/login/login';
import { RegisterComponent } from 'src/components/register/register';
import { FoodListComponent } from 'src/components/food/food-list';
import { ApiInterceptor } from 'interceptors/api.interceptor';
import { ShoppingCartModalComponent } from 'src/components/shopping-cart/shopping-cart-modal';
import { OrderOverviewComponent } from 'src/components/order/order-overview';
import { OrderListComponent } from 'src/components/order/order-list';
import { SpinnerComponent } from 'src/components/spinner/spinner';
import { MakeOrderComponent } from 'src/components/order/make-order';
import { UserSettingsComponent } from 'src/components/user-settings/user-settings';
import { AddressSettingsComponent } from 'src/components/user-settings/address-settings';

export const API_INTERCEPTOR_PROVIDER: Provider = {
  provide: HTTP_INTERCEPTORS,
  useExisting: forwardRef(() => ApiInterceptor),
  multi: true,
};

@NgModule({
  declarations: [
    AppComponent,
    FoodOverviewComponent,
    FoodListComponent,
    FoodPageComponent,
    NavbarComponent,
    LoginComponent,
    RegisterComponent,
    ShoppingCartModalComponent,
    OrderOverviewComponent,
    OrderListComponent,
    SpinnerComponent,
    MakeOrderComponent,
    UserSettingsComponent,
    AddressSettingsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ApiModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [ApiInterceptor, API_INTERCEPTOR_PROVIDER],
  bootstrap: [AppComponent],
})
export class AppModule {}
