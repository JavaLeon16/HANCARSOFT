import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { cloneDeep } from 'lodash-es';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { GridModel } from 'src/app/models/common/gridModel';
import { ActualizacionVariableService } from 'src/app/services/actualizacion-variable.service';
import { Fcaprog019mwService } from 'src/app/services/fcaprog019mw.service';
import Swal from 'sweetalert2';

import {
  camposGeneralesL, listProgramasL, cbxSupervisor, cbxMaquinas, cbxTripulaciones
} from '../../../../models/DTO/fcaprog019mw';

@Component({
  selector: 'app-pagina-l',
  templateUrl: './pagina-l.component.html',
  styleUrls: ['./pagina-l.component.css']
})
export class PaginaLComponent implements OnInit {
  camposGenerales: camposGeneralesL = {
    pFechaDel: this.primeroDelMes(new Date()),
    pFechaAl: this.pipe.transform(new Date(), 'yyyy-MM-dd'),
    pFechaProduccion: this.pipe.transform(new Date(), 'yyyy-MM-dd'),
    pSeleccionarTodos: false, pTurno: '1', pSinFechaProd: false
  };
  tituloModulo: string = 'Captura Estadistica Acab/Esp';
  columnasGridProgramas: any;
  datosGridProgramas = Array<listProgramasL>();
  @BlockUI() blockUI: NgBlockUI;
  cbxSupervisor: cbxSupervisor;
  cbxMaquinas: cbxMaquinas;
  cbxTripulaciones: cbxTripulaciones;
  grid1: GridModel;
  programa: number;

  constructor(
    public pipe: DatePipe,
    public servicio: Fcaprog019mwService,
    private servicioActVariable: ActualizacionVariableService
  ) {
    this.columnasGridProgramas = [
      {
        headerName: '',
        field: 'sel',
        flex: 1,
        minWidth: 30,
        headerClass: 'header-center header-grid-left',
        cellRenderer: 'hybCellRenderer',
        cellClass: 'grid-cell-btn-center',
        cellRendererParams: {
          type: 'chk',
          // disabled: true
          // change: this.changeChkScorPrincipal.bind(this)
        }
      },
      {
        headerName: 'Programa',
        field: 'programa',
        flex: 4,
        minWidth: 100,
        headerClass: 'header-center header-grid-right',
        cellClass: 'grid-cell-center'
      },
    ];
    this.cbxSupervisor = { selected: '', datos: [] };
    this.cbxMaquinas = { selected: '', datos: [] };
    this.cbxTripulaciones = { selected: 0, datos: [] };
  }

  async ngOnInit(): Promise<void> {
    this.datosGridProgramas = [];
    // for (let index = 1; index <= 100; index++) {
    //   this.datosGridProgramas.push({sel: false, programa: index, maquina: '', turno: '', op: '', idUnico: 0, fecha: ''});
    // }
    await this.cargaComboSupervisor();
    await this.cargaComboMaquinas();
    await this.cargaComboTripulacion();
  }

  async dateDiff(fechaI: string, fechaF: string): Promise<number> {

    var hi = fechaI, hf = fechaF;
    if (fechaI > fechaF) { hi = fechaF; hf = fechaI; }

    var fi = new Date(this.setFormat(hi, 'yyyy-MM-dd', 'MM/dd/yyyy'));
    var ff = new Date(this.setFormat(hf, 'yyyy-MM-dd', 'MM/dd/yyyy'));

    const difDias = Math.floor((ff.getDate() - fi.getDate()) / (1000 * 60 * 60 * 24))

    return difDias;
  }

  setFormat(
      date: string,
      fromFormat: 'yyyy-MM-dd' | 'dd/MM/yyyy' | 'yyyyMMdd',
      toFormat: 'yyyy-MM-dd' | 'dd/MM/yyyy' | 'yyyyMMdd' | 'MM/dd/yyyy'
  ): string {
    let day = '';
    let month = '';
    let year = '';

    if (fromFormat === 'dd/MM/yyyy') {
        const dateParts = date.split('/');

        if (dateParts.length > 2) {
            day = dateParts[0];
            month = dateParts[1];
            year = dateParts[2];
        } else {
            return '';
        }
    } else if (fromFormat === 'yyyy-MM-dd') {
        const dateParts = date.split('-');

        if (dateParts.length > 2) {
            day = dateParts[2];
            month = dateParts[1];
            year = dateParts[0];
        } else {
            return '';
        }
    } else if (fromFormat === 'yyyyMMdd') {
        if (date.length > 7) {
            day = date.substring(0, 4);
            month = date.substring(4, 6);
            year = date.substring(6, 8);
        } else {
            return '';
        }
    } else {
        return '';
    }

    if (toFormat === 'dd/MM/yyyy') {
      return day + '/' + month + '/' + year;
    } else if (toFormat === 'yyyy-MM-dd') {
      return year + '-' + month + '-' + day;
    } else if (toFormat === 'yyyyMMdd') {
      return year + month + day;
    } else if (toFormat === 'MM/dd/yyyy') {
      return month + '/' + day + '/' + year;
    } else {
      return '';
    }
  }

  async btnBuscar(): Promise<void> {
    // PENDIENTE
    // const difD = await this.dateDiff(this.camposGenerales.pFechaDel, this.camposGenerales.pFechaAl);
    // if (difD >= 7 || difD < 0) {
    //   Swal.fire('Información', 'Solo puede seleccionar 7 días de antiguedad a la fecha actual...', 'info');
    //   return;
    // }
    this.camposGenerales.pSeleccionarTodos = false;
    this.datosGridProgramas = [];
    await this.llenaListProgramas();
  }

  async llenaListProgramas(): Promise<void> {
    this.blockUI.start('Cargando Programa Seleccionado');
    this.datosGridProgramas = [];
    try {
      const res: any = await this.servicio.buscaProgramas(
        this.camposGenerales.pFechaDel, this.camposGenerales.pFechaAl, this.camposGenerales.pTurno,
        this.cbxMaquinas.selected, this.camposGenerales.pSinFechaProd
      );

      if (res.correcto && res.data.length > 0) {
        this.datosGridProgramas = res.data;
      }

      this.datosGridProgramas = cloneDeep(this.datosGridProgramas);

      this.blockUI.stop();

      if (res.data.length === 0) {
        this.mensajeFlotante('No se encontraron Datos para esa selección...', 6000, 1);
      }
    } catch (error) {
      this.blockUI.stop();
      Swal.fire('Error', error.error, 'error');
    }
  }

  chkAllPrograms_change(): void {
    for (const iterator of this.datosGridProgramas) {
      iterator.sel = this.camposGenerales.pSeleccionarTodos;
    }
    this.datosGridProgramas = cloneDeep(this.datosGridProgramas);
  }

  primeroDelMes(fecha: Date): string {
    var año = fecha.getUTCFullYear().toString();
    var mes = (fecha.getMonth() + 1).toString();
    return año + '-' + (mes.length === 1 ? '0' + mes : mes) + '-01';
  }

  async cargaComboSupervisor(): Promise<void> {
    this.blockUI.start('');
    this.cbxSupervisor = { selected: '', datos: [] };
    try {
      const res: any = await this.servicio.buscaSupervisor();
      if (res.correcto && res.data.length > 0) {
        this.cbxSupervisor.datos = res.data;
        this.cbxSupervisor.selected = res.data[0].idSupervisor;
      }

      this.blockUI.stop();
    } catch (error) {
      this.blockUI.stop();
      Swal.fire('Error', error.error, 'error');
    }
  }

  async cargaComboMaquinas(): Promise<void> {
    this.blockUI.start('');
    this.cbxMaquinas = { selected: '', datos: [] };
    try {
      const res: any = await this.servicio.cargaComboMaquinas();
      if (res.correcto && res.data.length > 0) {
        this.cbxMaquinas.datos = res.data;
        this.cbxMaquinas.selected = res.data[0].claveMaquina;
      }

      this.blockUI.stop();
    } catch (error) {
      this.blockUI.stop();
      Swal.fire('Error', error.error, 'error');
    }
  }

  async cargaComboTripulacion(): Promise<void> {
    this.blockUI.start('');
    this.cbxTripulaciones = { selected: 0, datos: [] };
    try {
      const res: any = await this.servicio.buscaTripulacionMaquina(this.cbxMaquinas.selected);
      if (res.correcto && res.data.length > 0) {
        this.cbxTripulaciones.datos = res.data;
        this.cbxTripulaciones.selected = Number(res.data[0].idTripulacion);
      }

      this.blockUI.stop();
    } catch (error) {
      this.blockUI.stop();
      Swal.fire('Error', error.error, 'error');
    }
  }

  async grid1_OnReady(ref: GridModel): Promise<void> {
    this.grid1 = ref;
  }

  async grid1_change(): Promise<void> {
    // OBTENER FILA SELECCIONADA
    const obj: any = this.grid1.getSelectedData();
    const row: listProgramasL = obj.length > 0 ? obj[0] : undefined;

    if (row) {
      this.servicioActVariable.pPrograma$.next(row.programa);
    }
  }

  mensajeFlotante(mensaje: string, tiempo: number = 2700, icono: number = 0): void {
    const Toast = Swal.mixin({
      toast: true,
      position: 'top-end',
      showConfirmButton: false,
      timer: tiempo
    });

    Toast.fire({
      icon: icono === 0 ? 'success' : icono === 1 ? 'info' : 'error',
      title: mensaje
    });
  }

}
