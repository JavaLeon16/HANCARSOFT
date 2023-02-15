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
    path: 'progauto',
    loadChildren: () => import('./modules/progauto/progauto.module').then((m) => m.ProgautoModule),
    // canActivate: [AuthGuard]
  },
  {
    path: 'secimp',
    loadChildren: () => import('./modules/secimp/secimp.module').then((m) => m.SecimpModule),
    canActivate: [AuthGuard]
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
