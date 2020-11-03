import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainComponent } from './main.component';
import { NavModule } from '../../nav/nav.module';

@NgModule({
  declarations: [MainComponent],
  imports: [
    CommonModule,
    NavModule
  ]
})
export class MainModule { }
