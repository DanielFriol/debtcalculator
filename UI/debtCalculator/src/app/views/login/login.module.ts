import { NgModule } from '@angular/core';
import { LoginComponent } from './login.component';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { LoginService } from './login.service';
import { SharedModule } from 'src/app/shared/shared.module';
import { SignupComponent } from './signup/signup.component';

@NgModule({
  declarations: [LoginComponent],
  imports: [
    SharedModule,
    AppRoutingModule
  ],
  providers: [
    LoginService
  ]
})
export class LoginModule { }
