import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { ChangePasswordComponent } from './change-password.component';
import { ChangePasswordRoutingModule } from './change-password-routing.module';
import { ChangePasswordService } from './change-password.service';

@NgModule({
  declarations: [ChangePasswordComponent],
  imports: [
    SharedModule,
    ChangePasswordRoutingModule
  ],
  providers: [
    ChangePasswordService
  ]
})
export class ChangePasswordModule { }
