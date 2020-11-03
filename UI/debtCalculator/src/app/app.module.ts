import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { MainModule } from './views/main/main.module';
import { HomeModule } from './views/home/home.module';
import { LoginModule } from './views/login/login.module';
import { ToastrModule } from 'ngx-toastr';
import { DebtModule } from './views/debt/debt.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    MainModule,
    ToastrModule.forRoot({
      preventDuplicates: true
    }),
    LoginModule,
    HomeModule,
    TabsModule.forRoot(),
    DebtModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
