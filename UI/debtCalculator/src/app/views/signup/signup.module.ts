import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SignupRoutingModule } from './signup-routing.module';
import { SignupComponent } from './signup.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { LoginService } from '../login/login.service';


@NgModule({
  declarations: [SignupComponent],
  imports: [
    SharedModule,
    SignupRoutingModule
  ],
  providers: [LoginService]
})
export class SignupModule { }
