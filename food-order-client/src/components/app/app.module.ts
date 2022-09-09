import { forwardRef, NgModule, Provider } from '@angular/core';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FoodOverviewComponent } from '../food/food-overview';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ApiModule } from 'generated/api.module';
import { FoodPageComponent } from '../food/food-page';
import { NavbarComponent } from '../nav/nav';
import { LoginComponent } from '../login/login';
import { RegisterComponent } from '../register/register';
import { FoodListComponent } from '../food/food-list';
import { ApiInterceptor } from 'interceptors/api.interceptor';
import { ShoppingCartOverviewComponent } from '../shopping-cart/shopping-cart-overview';

export const API_INTERCEPTOR_PROVIDER: Provider = {
  provide: HTTP_INTERCEPTORS,
  useExisting: forwardRef(() => ApiInterceptor),
  multi: true,
};

@NgModule({
  declarations: [
    AppComponent,
    FoodOverviewComponent,
    FoodPageComponent,
    NavbarComponent,
    LoginComponent,
    RegisterComponent,
    FoodListComponent,
    ShoppingCartOverviewComponent,
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
