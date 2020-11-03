import { NgModule } from '@angular/core';
import { NavComponent } from './nav.component';
import { SharedModule } from '../shared/shared.module';
import { AppRoutingModule } from '../app-routing.module';

@NgModule({
    imports: [
        SharedModule,
        AppRoutingModule
    ],
    declarations: [
        NavComponent
    ],
    exports: [
        NavComponent
    ]
})
export class NavModule { }
