import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FoodOverviewComponent } from '../food/food-overview';
import { HttpClientModule } from '@angular/common/http';
import { ApiModule } from 'generated/api.module';

@NgModule({
  declarations: [AppComponent, FoodOverviewComponent],
  imports: [BrowserModule, AppRoutingModule, HttpClientModule, ApiModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
