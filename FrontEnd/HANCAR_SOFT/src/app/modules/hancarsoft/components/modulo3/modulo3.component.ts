import { DatePipe } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import Swal from 'sweetalert2';

import {
  camposGenerales, datosBusquedaPrograma, datosBusquedaProduccion, datosTripulaciones,
  fcaprog019mw
} from '../../../../models/DTO/fcaprog019mw';
import { Fcaprog019mwService } from '../../../../services/fcaprog019mw.service';

@Component({
  selector: 'app-modulo3',
  templateUrl: './modulo3.component.html',
  styleUrls: ['./modulo3.component.css']
})
export class Modulo3Component implements OnInit {
  @BlockUI() blockUI: NgBlockUI;
  camposGenerales: camposGenerales;

  @ViewChild('mdlValida') private mdlValida: any;
  mdlValidaRef: NgbModalRef;
  columnasGridValidacion: any;
  datosGridValidacion: Array<datosBusquedaProduccion>;
  datosTripulaciones: datosTripulaciones;

  constructor(
    private modalService: NgbModal,
    public pipe: DatePipe,
    public servicio: Fcaprog019mwService
  ) {
    this.columnasGridValidacion = [
      {
        headerName: '',
        cellRenderer: 'btnCellRenderer',
        cellRendererParams: {
          onClick: this.gridValidacion_clickSeleccionar.bind(this),
          label: '<i class="fa fa-check" title="Aceptar"></i>',
          class: 'btn btn-success btn-sm'
        },
        headerClass: 'header-center header-grid-left',
        cellClass: 'grid-cell-btn-center',
        flex: 1,
        minWidth: 40,
        maxWidth: 60,
        suppressSizeToFit: true
      },
      {
        headerName: 'Máquina',
        field: 'claveMaquina',
        flex: 1,
        minWidth: 100,
        headerClass: 'header-center header-grid',
        cellClass: 'grid-cell-center'
      },
      {
        headerName: 'Fecha',
        field: 'fecha',
        flex: 1,
        minWidth: 150,
        headerClass: 'header-center header-grid',
        cellClass: 'grid-cell-center'
      },
      {
        headerName: 'Turno',
        field: 'turno',
        flex: 1,
        minWidth: 100,
        headerClass: 'header-center header-grid',
        cellClass: 'grid-cell-center'
      },
      {
        headerName: 'Cantidad',
        field: 'cantidad',
        flex: 1,
        minWidth: 100,
        headerClass: 'header-center header-grid',
        cellClass: 'grid-cell-center'
      },
      {
        headerName: 'IdUnicoProd',
        field: 'idUnico',
        flex: 1,
        minWidth: 100,
        headerClass: 'header-center header-grid-right',
        cellClass: 'grid-cell-center'
      }
    ];
  }

  ngOnInit(): void {
    this.limpiarCampos();
  }

  limpiarCampos(): void {
    this.camposGenerales = {
      wFechaAnterior: '',
      idUnicoProduccion: '000000', programa: 0, turno: '',
      fecha: this.pipe.transform(new Date(), 'yyyy-MM-dd'),
      horaIni: '', horaFin: '',
      claveMaquina: '', op: '', claveProceso: '', piezasCorte: 0, tipoMaquina: '', cantidad: 0,
      ultimoProceso: false, pegado: false, primerColor: '', segundoColor: '', tercerColor: '', cuartoColor: '',
      areaUnitaria: 0, pesoUnitario: 0, claveArticulo: '', articulo: '', liberadoCostos: false
    };
    this.datosGridValidacion = [];
    this.datosTripulaciones = {
      selected: '', datos: []
    };
  }

  async btnBuscarPrograma(): Promise<void> {
    if (this.camposGenerales.programa) {
      this.blockUI.start('Obteniendo programa');
      try {
        const objBuscaPrograma: any = await this.servicio.buscaPrograma(this.camposGenerales.programa);
        this.blockUI.stop();
        if (objBuscaPrograma.correcto && objBuscaPrograma.data.length > 0) {
          const obj: Array<datosBusquedaPrograma> = objBuscaPrograma.data;
          for (const iterator of obj) {
            this.camposGenerales.ultimoProceso = iterator.ultimoProceso;
            this.camposGenerales.op = iterator.op;
            this.camposGenerales.claveArticulo = iterator.claveArticulo;
            this.camposGenerales.articulo = iterator.articulo;
            this.camposGenerales.primerColor = iterator.primerColor;
            this.camposGenerales.segundoColor = iterator.segundoColor;
            this.camposGenerales.tercerColor = iterator.tercerColor;
            this.camposGenerales.cuartoColor = iterator.cuartoColor;
            this.camposGenerales.cantidad = iterator.cantidad;
            this.camposGenerales.tipoMaquina = iterator.tipoMaquina;
            this.camposGenerales.liberadoCostos = iterator.liberadoCostos;
          }
          if (this.camposGenerales.tipoMaquina === 'IM' || this.camposGenerales.liberadoCostos) {
            Swal.fire(
              'Captura Inválida',
              (this.camposGenerales.tipoMaquina === 'IM' && this.camposGenerales.liberadoCostos
                ? 'Programa Generado para Máquina Impresora y OP liberada para Costos'
                : (this.camposGenerales.tipoMaquina === 'IM'
                  ? 'Programa Generado para Máquina Impresora'
                  : this.camposGenerales.liberadoCostos
                    ? 'OP liberada para Costos'
                    : ''
                )
              ),
              'info'
            );
          }
          else {
            const objBuscaProduccion: any = await this.servicio.buscaProduccion(this.camposGenerales.programa);
            if (objBuscaProduccion.correcto && objBuscaProduccion.data.length > 0) {
              this.datosGridValidacion = objBuscaProduccion.data;
              this.mdlValidaRef = this.modalService.open(this.mdlValida, {size: 'xl', backdrop: 'static'});
            }
            else {
              this.mensajeFlotante('Actualizado');
            }
          }
        }
        else {
          this.mensajeFlotante('Número de Programa INEXISTENTE', 1, 3000);
        }
      } catch (error) {
        this.blockUI.stop();
        Swal.fire('Error', error.error, 'error');
      }
    }
    else {
      this.mensajeFlotante('Favor de capturar el programa', 1, 3000);
    }
  }

  async gridValidacion_clickSeleccionar(obj: any): Promise<void> {
    const row: datosBusquedaProduccion = obj.data;
    this.blockUI.start('');
    try {
      const res: any = await this.servicio.buscaTripulacionMaquina(row.claveMaquina);
      if (res.correcto && res.data.length > 0) {
        this.datosTripulaciones.datos = res.data;
        this.datosTripulaciones.selected = res.data[0].idTripulacion;
      }
      this.camposGenerales.claveMaquina = row.claveMaquina;
      this.camposGenerales.turno = row.turno;
      this.camposGenerales.idUnicoProduccion = row.idUnico.toString().trim();
      if (row.fecha) {
        this.camposGenerales.fecha = row.fecha;
        this.camposGenerales.wFechaAnterior = row.fecha;
      }
      this.txtTurno_Change();
      this.blockUI.stop();
      this.mdlValidaRef.close();
    } catch (error) {
      this.blockUI.stop();
      Swal.fire('Error', error.error, 'error');
    }
  }

  async txtTurno_Change(): Promise<void> {
    if (this.camposGenerales.turno) {
      await this.leeProduccion();
    }
  }

  async leeProduccion(): Promise<void> {
    this.blockUI.start('');
    try {
      await this.iniciaProduccion();

      if (this.camposGenerales.wFechaAnterior) {
        this.camposGenerales.fecha = this.camposGenerales.wFechaAnterior;
      }



      this.blockUI.stop();
    } catch (error) {
      this.blockUI.stop();
      Swal.fire('Error', error.error, 'error');
    }
  }

  async iniciaProduccion(): Promise<void> {
    const res: any = await this.servicio.iniciaProduccion();
    if (res.correcto && res.data.length > 0) {
      const data: Array<fcaprog019mw> = res.data;
      for (const iterator of data) {
        this.camposGenerales.fecha = iterator.fecha.substring(0, 10);
        this.camposGenerales.horaIni = iterator.fecha.substring(11, 16);
        this.camposGenerales.horaFin = iterator.fecha.substring(11, 16);
      }
    }
  }

  mensajeFlotante(mensaje: string, icono: number = 0, tiempo: number = 2700): void {
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
