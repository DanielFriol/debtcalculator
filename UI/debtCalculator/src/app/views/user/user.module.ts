import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { UserComponent } from './user.component';
import { UserService } from './user.service';


@NgModule({
  declarations: [UserComponent],
  imports: [
    SharedModule,
    UserRoutingModule
  ],
  providers: [
    UserService
  ]
})
export class UserModule { }
