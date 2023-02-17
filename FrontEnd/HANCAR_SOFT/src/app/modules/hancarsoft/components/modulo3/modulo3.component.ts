import { DatePipe } from '@angular/common';
import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import Swal from 'sweetalert2';

import {
  camposGenerales, datosBusquedaPrograma, datosBusquedaProduccion, cbxTripulaciones,
  fcaprog019mw, datosPrograma, cbxSupervisor, cbxParafina, objGuardar
} from '../../../../models/DTO/fcaprog019mw';
import { Fcaprog019mwService } from '../../../../services/fcaprog019mw.service';
import { ActualizacionVariableService } from '../../../../services/actualizacion-variable.service';

@Component({
  selector: 'app-modulo3',
  templateUrl: './modulo3.component.html',
  styleUrls: ['./modulo3.component.css']
})
export class Modulo3Component implements OnInit {
  @Input() abiertoDesdeModuloL: boolean = false;
  @Input() visibleTituloModulo: boolean = true;
  // @Input() programa: number = 0;
  tituloModulo: string = 'Captura Estadistica Acab/Esp';
  @BlockUI() blockUI: NgBlockUI;
  camposGenerales: camposGenerales;

  @ViewChild('mdlValida') private mdlValida: any;
  mdlValidaRef: NgbModalRef;
  columnasGridValidacion: any;
  datosGridValidacion: Array<datosBusquedaProduccion>;
  cbxTripulaciones: cbxTripulaciones;
  cbxSupervisor: cbxSupervisor;
  cbxParafina: cbxParafina;
  objGuardar: objGuardar;

  constructor(
    private modalService: NgbModal,
    public pipe: DatePipe,
    public servicio: Fcaprog019mwService,
    private servicioActVariable: ActualizacionVariableService
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

  async ngOnInit(): Promise<void> {
    this.limpiarCampos();
    await this.cargaCbxSupervisor();
    await this.cargaCbxTipoParafina();
    if (this.abiertoDesdeModuloL) {
      this.servicioActVariable.pPrograma$.subscribe(async data => {
        this.camposGenerales.programa = data;
        await this.txtPrograma_change();
        // console.log(`El valor de la variable cambio a: ${data}`);
      });
    }
  }

  limpiarCampos(limpiaPrograma: boolean = true, program: number = 0): void {
    this.camposGenerales = {
      wFechaAnterior: '',
      idUnicoProduccion: '', programa: limpiaPrograma ? 0 : program, turno: '',
      fecha: this.pipe.transform(new Date(), 'yyyy-MM-dd'),
      horaIni: '00:00', horaFin: '00:00', disabledParafina: true, disabledBtnAcepta: true,
      claveMaquina: '', op: '', claveProceso: '', piezasCorte: 0, tipoMaquina: '', cantidad: 0,
      ultimoProceso: false, pegado: false, primerColor: '', segundoColor: '', tercerColor: '', cuartoColor: '',
      areaUnitaria: 0, pesoUnitario: 0, claveArticulo: '', articulo: '', liberadoCostos: false,
      desperdicioImpresora: 0, desperdicioCorrugadora: 0, desperdicioLinea: 0, pesoLamina: 0, pesoCaja: 0, retrabajo: 0
    };
    this.datosGridValidacion = [];
    if (limpiaPrograma) { this.cbxTripulaciones = { selected: 0, datos: [] }; }
    if (limpiaPrograma) { this.cbxSupervisor = { selected: '', datos: [] }; }
    if (limpiaPrograma) { this.cbxParafina = { selected: 0, datos: [] }; }
    this.objGuardar = {
      fecha: '', horaIni: '', horaFin: '', turno: 0, supervisor: '', minutos: 0, despCorrguradora: 0,
      despImpresora: 0, despAcabados: 0, fechaNow: '', parafina: '', pesoLamina: 0, pesoCaja: 0, retrabajo: 0,
      actCantidad: 0, idTripulacion: 0, programa: 0, claveMaquina: '', wFechaAnterior: '', idUnico: 0
    }
  }

  async txtPrograma_change(): Promise<void> {
    if (this.abiertoDesdeModuloL) {
      await this.btnBuscarPrograma();
    }
  }

  async btnBuscarPrograma(): Promise<void> {
    this.limpiarCampos(false, this.camposGenerales.programa);
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
            this.camposGenerales.disabledBtnAcepta = false;
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
        this.cbxTripulaciones.datos = res.data;
        this.cbxTripulaciones.selected = Number(res.data[0].idTripulacion);
      }
      this.camposGenerales.claveMaquina = row.claveMaquina;
      this.camposGenerales.turno = row.turno;
      this.camposGenerales.idUnicoProduccion = row.idUnico.toString().trim();
      if (row.fecha) {
        this.camposGenerales.fecha = row.fecha;
        this.camposGenerales.wFechaAnterior = row.fecha;
      }
      await this.txtTurno_Change();
      await this.m3_txtMaquina_Change();
      this.blockUI.stop();
      this.mdlValidaRef.close();
    } catch (error) {
      this.blockUI.stop();
      Swal.fire('Error', error.error, 'error');
    }
  }

  async m3_txtMaquina_Change(): Promise<void> {
    await this.servicio.leeHoraLocal();
    const maq = this.camposGenerales.claveMaquina.trim();
    if (maq === 'PARAF' || maq === 'PARAF2') {
      this.camposGenerales.disabledParafina = false;
    }
    else {
      this.camposGenerales.disabledParafina = true;
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

      const res: any = await this.servicio.leePrograma(
        this.camposGenerales.fecha, this.camposGenerales.programa, this.camposGenerales.claveMaquina, this.camposGenerales.turno
      );

      if (res.correcto && res.data.length > 0) {
        await this.leeDatos(res.data);
        this.camposGenerales.disabledBtnAcepta = false;
      }

      this.blockUI.stop();
    } catch (error) {
      this.blockUI.stop();
      Swal.fire('Error', error.error, 'error');
    }
  }

  async leeDatos(data: Array<datosPrograma>): Promise<void> {
    for (const row of data) {
      this.camposGenerales.fecha = row.fecha ? row.fecha.trim() : this.camposGenerales.fecha.trim();
      this.camposGenerales.horaIni = row.horaInicio ? row.horaInicio.trim() : this.camposGenerales.horaIni.trim();
      this.camposGenerales.horaFin = row.horaTermino ? row.horaTermino.trim() : this.camposGenerales.horaFin.trim();
      this.camposGenerales.desperdicioCorrugadora = row.laminaDespeg ? row.laminaDespeg : this.camposGenerales.desperdicioCorrugadora;
      this.camposGenerales.desperdicioImpresora = row.laminaImpres ? row.laminaImpres : this.camposGenerales.desperdicioImpresora;
      this.camposGenerales.desperdicioLinea = row.desperdicioAcabados ? row.desperdicioAcabados : this.camposGenerales.desperdicioLinea;
      this.camposGenerales.cantidad = row.cantidad ? row.cantidad : this.camposGenerales.cantidad;
      // PickUp
      if (!this.camposGenerales.disabledParafina) {
        if (this.cbxParafina.datos && this.cbxParafina.datos.length > 0) {
          for (const fila of this.cbxParafina.datos) {
            if (fila.parafina === row.tipoParafina) {
              this.cbxParafina.selected = fila.idParafina;
              break;
            }
          }
        }
      }
      this.camposGenerales.pesoLamina = row.pesoLamina ? row.pesoLamina : this.camposGenerales.pesoLamina;
      this.camposGenerales.pesoCaja = row.pesoCaja ? row.pesoCaja : this.camposGenerales.pesoCaja;
      this.camposGenerales.retrabajo = row.retrabajo ? row.retrabajo : this.camposGenerales.retrabajo;
      this.cbxSupervisor.selected = row.supervisor ? row.supervisor.trim() : this.cbxSupervisor.selected.trim();
      var tmpSup = this.cbxSupervisor.selected;
      for (const row of this.cbxSupervisor.datos) {
        if (row.idSupervisor.trim() === tmpSup) {
          this.cbxSupervisor.selected = tmpSup;
          break;
        }
        else {
          this.cbxSupervisor.selected = '';
        }
      }
      this.cbxTripulaciones.selected = row.idTripulacion ? row.idTripulacion : this.cbxTripulaciones.selected;
      var tmpTrip = this.cbxTripulaciones.selected;
      for (const row of this.cbxTripulaciones.datos) {
        if (row.idTripulacion === tmpTrip) {
          this.cbxTripulaciones.selected = tmpTrip;
          break;
        }
        else {
          this.cbxTripulaciones.selected = 0;
        }
      }
    }
  }

  async cargaCbxSupervisor(): Promise<void> {
    const res: any = await this.servicio.buscaSupervisor();
    this.cbxSupervisor = { selected: '', datos: [] };
    if (res.correcto && res.data.length > 0) {
      this.cbxSupervisor.datos = res.data;
    }
  }

  async cargaCbxTipoParafina(): Promise<void> {
    const res: any = await this.servicio.buscaParafina();
    this.cbxParafina = { selected: 0, datos: [] };
    if (res.correcto && res.data.length > 0) {
      this.cbxParafina.datos = res.data;
      this.cbxParafina.selected = res.data[0].idParafina;
    }
  }

  async iniciaProduccion(): Promise<void> {
    const res: any = await this.servicio.leeHoraLocal();
    if (res.correcto && res.data.length > 0) {
      const data: Array<fcaprog019mw> = res.data;
      for (const iterator of data) {
        this.camposGenerales.fecha = iterator.fecha.substring(0, 10).trim();
        this.camposGenerales.horaIni = iterator.fecha.substring(11, 16).trim();
        this.camposGenerales.horaFin = iterator.fecha.substring(11, 16).trim();
      }
    }
    this.camposGenerales.desperdicioImpresora = 0;
    this.camposGenerales.desperdicioCorrugadora = 0;
    this.camposGenerales.desperdicioLinea = 0;
  }

  async validaDatos(): Promise<boolean> {
    var pValidaDatos: boolean = true;
    if (!this.camposGenerales.programa) {
      this.mensajeFlotante('Seleccione un Programa', 1, 3000);
      pValidaDatos = false;
    }
    if (!this.cbxSupervisor.selected) {
      this.mensajeFlotante('Seleccione un Supervisor', 1, 3000);
      pValidaDatos = false;
    }
    if (!this.camposGenerales.turno) {
      this.mensajeFlotante('Seleccione un Turno', 1, 3000);
      pValidaDatos = false;
    }
    if (!this.cbxSupervisor.selected) {
      this.mensajeFlotante('Seleccione una Tripulación', 1, 3000);
      pValidaDatos = false;
    }
    if (!this.camposGenerales.idUnicoProduccion) {
      this.mensajeFlotante('No se identificó el Id del Registro', 1, 3000);
      pValidaDatos = false;
    }

    if (pValidaDatos) {
      if (this.camposGenerales.wFechaAnterior !== this.camposGenerales.fecha) {
        const res: any = await this.servicio.validarGuardado(
          this.camposGenerales.programa, this.camposGenerales.claveMaquina, this.camposGenerales.turno, this.camposGenerales.fecha
        );
        if (res.correcto && res.data.length > 0) {
          pValidaDatos = false;
          Swal.fire({
            title: 'Información',
            html: 'Ya existe Registro de Producción el Turno ' + this.camposGenerales.turno + '<br>' +
              'Del día ' + this.camposGenerales.fecha,
            icon: 'info'
          });
        }
      }
    }
    return pValidaDatos;
  }

  async btnAcepta(): Promise<void> {
    if (await this.validaDatos()) {
      const res: any = await this.servicio.leeHoraLocal();
      var fechaNow = '', lMinutos = 0, lParafina = 0, lActCantidad = 0;
      if (res.correcto && res.data.length > 0) {
        fechaNow = res.data[0].fecha;
      }
      lMinutos = await this.dateDiff(this.camposGenerales.horaIni, this.camposGenerales.horaFin);

      if (lMinutos < 0) {
        lMinutos = 1440 - lMinutos;
      }

      if (!this.camposGenerales.desperdicioCorrugadora) { this.camposGenerales.desperdicioCorrugadora = 0; }
      if (!this.camposGenerales.desperdicioImpresora) { this.camposGenerales.desperdicioImpresora = 0; }
      if (!this.camposGenerales.desperdicioLinea) { this.camposGenerales.desperdicioLinea = 0; }
      if (!this.camposGenerales.pesoLamina) { this.camposGenerales.pesoLamina = 0; }
      if (!this.camposGenerales.pesoCaja) { this.camposGenerales.pesoCaja = 0; }
      if (!this.camposGenerales.retrabajo) { this.camposGenerales.retrabajo = 0; }
      lParafina = !this.camposGenerales.disabledParafina ? this.cbxParafina.selected : 0;
      lActCantidad = !this.camposGenerales.ultimoProceso ? this.camposGenerales.cantidad : 0;

      this.objGuardar = {
        fecha: this.camposGenerales.fecha, horaIni: this.camposGenerales.horaIni, horaFin: this.camposGenerales.horaFin,
        turno: Number(this.camposGenerales.turno), supervisor: this.cbxSupervisor.selected, minutos: lMinutos,
        despCorrguradora: this.camposGenerales.desperdicioCorrugadora, despImpresora: this.camposGenerales.desperdicioImpresora,
        despAcabados: this.camposGenerales.desperdicioLinea, fechaNow: fechaNow, parafina: lParafina.toString(),
        pesoLamina: this.camposGenerales.pesoLamina, pesoCaja: this.camposGenerales.pesoCaja, retrabajo: this.camposGenerales.retrabajo,
        actCantidad: lActCantidad, idTripulacion: this.cbxTripulaciones.selected,
        programa: this.camposGenerales.programa, claveMaquina: this.camposGenerales.claveMaquina,
        wFechaAnterior: this.camposGenerales.wFechaAnterior, idUnico: Number(this.camposGenerales.idUnicoProduccion)
      }

      try {
        const result: any = await this.servicio.guardar(this.objGuardar);
        if (result.correcto) {
          this.mensajeFlotante('Datos Actualizados...', 3800);
        }
      } catch (error) {
        Swal.fire({
          title: 'Error',
          html: 'Se presento un problema, favor de reportarlo a mesa de servicio.<br>' + error.error,
          icon: 'error'
        });
      }
    }
  }

  async dateDiff(horaI: string, horaF: string): Promise<number> {
    var hi = '', hf = '';
    var mHi = 0, mHf = 0, difMin = 0;
    if (horaI > horaF) { hi = horaF; hf = horaI; }
    else { hi = horaI; hf = horaF; }

    mHi = (Number(hi.substring(0, 2)) * 60) + Number(hi.substring(3, 5));
    mHf = (Number(hf.substring(0, 2)) * 60) + Number(hf.substring(3, 5));
    difMin = mHf - mHi;

    return difMin;
  }

  async m3_txtHora_change(): Promise<void> {
    var lMin = 0;
    lMin = await this.dateDiff(this.camposGenerales.horaIni, this.camposGenerales.horaFin);
    if (lMin < 0) {
      lMin = 1440 - lMin;
    }
    if (lMin > 450) {
      Swal.fire('Información', 'Tiempo calculado ' + lMin.toString() + " minutos... Por favor, verifique", 'info');
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
