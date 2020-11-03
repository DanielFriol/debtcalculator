import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DebtComponent } from './debt.component';



const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Dívidas'
    },
    children: [
      {
        path: '',
        data: {
          title: null
        },
        component: DebtComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DebtRoutingModule { }
