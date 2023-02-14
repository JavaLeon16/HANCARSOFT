import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthComponent } from './shared/components/auth/auth.component';
import { AuthGuard } from './helpers/auth.guard';

const routes: Routes = [
  {
    path: 'auth/:module/:component',
    component: AuthComponent
  },
  {
    path: 'auth/:module',
    component: AuthComponent
  },
  {
    path: 'hancarsoft',
    loadChildren: () => import('./modules/hancarsoft/hancarsoft.module').then((m) => m.HancarsoftModule),
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
