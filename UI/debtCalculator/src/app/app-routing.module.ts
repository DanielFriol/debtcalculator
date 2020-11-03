import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './shared/services/auth-guard.service';
import { LoginComponent } from './views/login/login.component';
import { MainComponent } from './views/main/main.component';


const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full',
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: '',
    component: MainComponent,
    children: [
      {
        path: 'home',
        canActivate: [AuthGuard],
        loadChildren: () => import('./views/home/home.module').then(m => m.HomeModule)
      },
      {
        path: 'users',
        canActivate: [AuthGuard],
        loadChildren: () => import('./views/user/user.module').then(m => m.UserModule)
      },
      {
        path: 'debts',
        canActivate: [AuthGuard],
        loadChildren: () => import('./views/debt/debt.module').then(m => m.DebtModule)
      },
      {
        path: 'change-password',
        canActivate: [AuthGuard],
        loadChildren: () => import('./views/change-password/change-password.module').then(m => m.ChangePasswordModule)
      },
    ],
  },
  { path: '**', redirectTo: 'home' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
