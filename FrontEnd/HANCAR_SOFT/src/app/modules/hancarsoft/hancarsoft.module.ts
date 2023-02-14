import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { BlockUIModule } from 'ng-block-ui';

import { HancarsoftRoutingModule } from './hancarsoft-routing.module';
import { Modulo1Component } from './pages/modulo1/modulo1.component';
import { PaginaLComponent } from './pages/pagina-l/pagina-l.component';
import { Modulo3Component } from './components/modulo3/modulo3.component';

@NgModule({
  declarations: [Modulo1Component, PaginaLComponent, Modulo3Component],
  imports: [
    CommonModule,
    HancarsoftRoutingModule,
    SharedModule,
    BlockUIModule.forRoot()
  ]
})
export class HancarsoftModule { }
