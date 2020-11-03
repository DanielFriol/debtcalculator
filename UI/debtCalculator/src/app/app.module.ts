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
import { UserComponent } from './views/user/user.component';
import { UserModule } from './views/user/user.module';
import { DebtComponent } from './views/debt/debt.component';
import { DebtModule } from './views/debt/debt.module';
import { SignupModule } from './views/signup/signup.module';

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
    SignupModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
