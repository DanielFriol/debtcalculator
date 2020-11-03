import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DebtRoutingModule } from './debt-routing.module';
import { DebtComponent } from './debt.component';
import { DebtService } from './debt.service';
import { SharedModule } from 'src/app/shared/shared.module';


@NgModule({
  declarations: [DebtComponent],
  imports: [
    SharedModule,
    DebtRoutingModule
  ],
  providers: [DebtService]
})
export class DebtModule { }
