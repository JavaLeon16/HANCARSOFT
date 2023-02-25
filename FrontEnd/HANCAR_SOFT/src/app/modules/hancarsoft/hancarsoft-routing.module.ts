import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { PaginaLComponent } from './pages/pagina-l/pagina-l.component';
import { Modulo1Component } from './pages/modulo1/modulo1.component';
import { Modulo2Component } from './components/modulo2/modulo2.component';
import { Modulo3Component } from './components/modulo3/modulo3.component';

const routes: Routes = [
  { path: 'modulo1', component: Modulo1Component },
  { path: 'modulo2', component: Modulo2Component },
  { path: 'modulo3', component: Modulo3Component },
  { path: 'paginal', component: PaginaLComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HancarsoftRoutingModule { }
