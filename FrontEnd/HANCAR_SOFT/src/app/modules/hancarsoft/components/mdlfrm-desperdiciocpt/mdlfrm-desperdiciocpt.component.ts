import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { GridModel } from 'src/app/models/common/gridModel';

import {
  camposFrmDesp, gridFrmDesp
} from 'src/app/models/DTO/fcaprog019mw';
import { ActualizacionVariableService } from 'src/app/services/actualizacion-variable.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-mdlfrm-desperdiciocpt',
  templateUrl: './mdlfrm-desperdiciocpt.component.html',
  styleUrls: ['./mdlfrm-desperdiciocpt.component.css']
})
export class MdlfrmDesperdiciocptComponent implements OnInit {
  @Input() camposFrmDesp: camposFrmDesp;
  // txtDespCorrUtil: string = 'm2_txtUtilizado1';
  // txtDespCorrNoUtil: string = 'm2_txtNoUtilizado1';
  // txtDesPAUtul: string = 'm2_txtUtilizado2';
  // txtDespPANoUtil: string = 'm2_txtNoUtilizado2';
  // txtDespImprUtil: string = 'm2_txtContabilizado';
  // txtImpresoraNoCon: string = 'm2_txtNoContabilizado';

  @ViewChild('gridConceptos') gridConceptos: GridModel;

  constructor(
    public servicioActVariable: ActualizacionVariableService
  ) { }

  ngOnInit(): void {
  }

  mdlDesp_Filtro(): void {
    try {
      if (this.camposFrmDesp.filtro) {
        const index: number = this.mdlGridConceptos_selectConcepto(this.camposFrmDesp.filtro.trim());
        this.gridConceptos.ensureIndexVisible(index);
        // this.servicioActVariable.pDatosModalDesperdicio$.next(this.camposFrmDesp);
      }
    } catch (error) {
      Swal.fire('Error', error.error ? error.error : error.message, 'error');
    }
  }
  mdlGridConceptos_selectConcepto(filtro: string): number {
    const regex: RegExp = new RegExp(filtro.toLowerCase() + '*');
    const fila: Array<gridFrmDesp> = this.camposFrmDesp.gridDatos.filter(row => {
      return regex.test(row.concepto.toLowerCase());
    });
    this.gridConceptos.select({key: 'idConcepto', rows: [{idConcepto: fila[0].idConcepto}]});
    for (let index = 0; index < this.camposFrmDesp.gridDatos.length; index++) {
      const element = this.camposFrmDesp.gridDatos[index];
      if (element.idConcepto === fila[0].idConcepto) {
        return index;
      }
    }
    return 0;
  }

  changeCantidadGridConceptos(): void {
    this.mensajeFlotante('modal');
  }

  mensajeFlotante(
    mensaje: string,
    tiempo: number = 2700,
    icono: number = 0
  ): void {
    const Toast = Swal.mixin({
      toast: true,
      position: 'top-end',
      showConfirmButton: false,
      timer: tiempo,
    });

    Toast.fire({
      icon: icono === 0 ? 'success' : icono === 1 ? 'info' : 'error',
      title: mensaje,
    });
  }

}
