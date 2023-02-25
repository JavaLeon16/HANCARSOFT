import { DatePipe } from '@angular/common';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import Swal from 'sweetalert2';

import {
  camposFrmDesp,
  camposGeneralesMod2,
  camposGuardado,
  cbxClaveProceso,
  cbxGrabado,
  cbxMaquinas,
  cbxSuaje,
  cbxSupervisor,
  cbxTripulaciones,
  datosBusquedaProduccion,
  datosPrograma,
  desperdiciosCapturados,
  fcaprog019mw,
  gridFrmDesp,
  validaDatosSupervisor,
} from 'src/app/models/DTO/fcaprog019mw';
import { Fcaprog019mwService } from 'src/app/services/fcaprog019mw.service';
import { cloneDeep } from 'lodash-es';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { GridModel } from '../../../../models/common/gridModel';

@Component({
  selector: 'app-modulo2',
  templateUrl: './modulo2.component.html',
  styleUrls: ['./modulo2.component.css'],
})
export class Modulo2Component implements OnInit {
  @Input() abiertoDesdeModuloL: boolean = false;
  @Input() visibleTituloModulo: boolean = true;
  tituloModulo: string = 'Captura Estadísticas Producción Impresoras';
  controlFocusName: string;
  @BlockUI() blockUI: NgBlockUI;
  pZonaERP: string;

  // @ViewChild('mdlCon') mdlCon: GridModel;
  @ViewChild('frmDesperdicioCpt') private frmDesperdicioCpt: any;
  frmDesperdicioCptRef: NgbModalRef;

  camposGenerales: camposGeneralesMod2;
  cbxClaveProceso: cbxClaveProceso;
  cbxSupervisor: cbxSupervisor;
  cbxSuaje: cbxSuaje;
  cbxGrabado: cbxGrabado;
  cbxMaquinaProcAnt: cbxMaquinas;

  @ViewChild('mdlValida') private mdlValida: any;
  mdlValidaRef: NgbModalRef;
  columnasGridValidacion: any;
  datosGridValidacion: Array<datosBusquedaProduccion>;
  cbxTripulaciones: cbxTripulaciones;

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
          class: 'btn btn-success btn-sm',
        },
        headerClass: 'header-center header-grid-left',
        cellClass: 'grid-cell-btn-center',
        flex: 1,
        minWidth: 40,
        maxWidth: 60,
        suppressSizeToFit: true,
      },
      {
        headerName: 'Máquina',
        field: 'claveMaquina',
        flex: 1,
        minWidth: 100,
        headerClass: 'header-center header-grid',
        cellClass: 'grid-cell-center',
      },
      {
        headerName: 'Fecha',
        field: 'fecha',
        flex: 1,
        minWidth: 150,
        headerClass: 'header-center header-grid',
        cellClass: 'grid-cell-center',
      },
      {
        headerName: 'Turno',
        field: 'turno',
        flex: 1,
        minWidth: 100,
        headerClass: 'header-center header-grid',
        cellClass: 'grid-cell-center',
      },
      {
        headerName: 'Cantidad',
        field: 'cantidad',
        flex: 1,
        minWidth: 100,
        headerClass: 'header-center header-grid',
        cellClass: 'grid-cell-center',
      },
      {
        headerName: 'IdUnicoProd',
        field: 'idUnico',
        flex: 1,
        minWidth: 100,
        headerClass: 'header-center header-grid-right',
        cellClass: 'grid-cell-center',
      },
    ];
  }

  ngOnInit(): void {
    this.pZonaERP = localStorage.getItem('Zona');
    this.limpiarCampos();
    this.inicializarCbxClavePreparacion('CECS2');
  }

  limpiarCampos(): void {
    this.camposGenerales = {
      wFechaAnterior: '',
      idUnicoProduccion: '',
      programa: 0,
      proceso1: false,
      turno: '1',
      fecha: this.pipe.transform(new Date(), 'yyyy-MM-dd'),
      horaIni: '00:00',
      horaFin: '00:00',
      desperdicioImpresora: 0,
      desperdicioCorrugadora: 0,
      desperdicioLinea: 0,
      pesoLamina: 0,
      pesoCaja: 0,
      retrabajo: 0,
      claveMaquina: '',
      op: '',
      claveProceso: '',
      piezasCorte: 0,
      tipoMaquina: '',
      cantidad: 0,
      programado: 0,
      ultimoProceso: false,
      pegado: false,
      primerColor: '',
      cantidadPrimerColor: 0,
      segundoColor: '',
      cantidadSegundoColor: 0,
      tercerColor: '',
      cantidadTercerColor: 0,
      cuartoColor: '',
      cantidadCuartoColor: 0,
      areaUnitaria: 0,
      pesoUnitario: 0,
      claveArticulo: '',
      articulo: '',
      suajeOld: '',
      suaje: '',
      liberadoCostos: false,
      notasOperacion: '',
      eficiencia: 0,
      eficienciaAct: 0,
      wEficiencia: 0,
      sinPreparacion: false,
      canTinta1: 0,
      canTinta2: 0,
      canTinta3: 0,
      canTinta4: 0,
      supervisor: '',
      proceso: '',
      minPrep: 0,
      velocidad: 0,
      velocidadAnt: 0,
      minStdProd: 0,
      minStdAnt: 0,
      maqPA: '',
      despPA: 0,
      utilizadoPA1: 0,
      noUtilizadoPA1: 0,
      utilizadoPA2: 0,
      contabilizadoPA: 0,
      noContabilizadoPA: 0,
      cantidadCajasRec: 0,
      maquinaProcAct: 'IMPRESORA',
      maquinaProcAnt: 'CORR1',

      disabledCbxClavePreparacion: false,
      disabledParafina: false,
      disabledBtnAcepta: false,
      disabledPanelPA: true,
      disabledPanelProcesoActual: true,
      disabledMaquinaPA: true,
      disabledBtnConceptos2: true,
    };
    this.cbxClaveProceso = {
      selected: '',
      selectedTmp: '',
      datos: [],
    };
    this.cbxTripulaciones = {
      selected: 0,
      datos: [],
    };
    this.cbxSupervisor = { selected: '', datos: [] };
    this.cbxSuaje = { selected: '', datos: [] };
    this.cbxGrabado = { selected: '', datos: [] };
    this.cbxMaquinaProcAnt = { selected: '', datos: [] };
    this.limpiarMdlConceptosDesp();
  }

  limpiarMdlConceptosDesp(): void {
    this.camposFrmDesp = {
      despId: 0,filtro: '', programa: 0, op: '', claveMaquinaCap: '', gridDatos: [],
      totalCapturado: 0, aplicaCajaRec: false, modalHabilitado: false,
      maquinaDesperdicio: '', capturaDesperdicio: false, esUtilizado: false,
      esContabilizadoPLC: false, esProcesoAnterior: false, despTurno: 0,
      gridColumns: [
        {
          headerName: '',
          field: 'idConcepto',
          flex: 1,
          minWidth: 50,
          headerClass: 'header-center header-grid-left',
          cellClass: 'grid-cell-center',
        },
        {
          headerName: 'Descripcion',
          field: 'concepto',
          flex: 4,
          minWidth: 250,
          headerClass: 'header-center header-grid',
          cellClass: 'grid-cell-center',
        },
        {
          headerName: 'Cantidad',
          field: 'cantidad',
          flex: 1,
          minWidth: 80,
          headerClass: 'header-center header-grid-right',
          cellClass: 'grid-cell-center',
          cellRenderer: 'hybCellRenderer',
          cellRendererParams: {
            type: 'num',
            change: this.changeCantidadGridConceptos.bind(this)
          },
        }
      ]
    };
  }

  changeCantidadGridConceptos(obj: any): void {
    const row: gridFrmDesp = obj.data;
    var total = 0;
    for (const iterator of this.camposFrmDesp.gridDatos) {
      total = total + iterator.cantidad;
    }
    this.camposFrmDesp.totalCapturado = total;
  }

  async frmDesperdicioCpt_btnAceptar(): Promise<void> {

    this.blockUI.start('');
    this.servicio.guardarDespMod2(this.camposFrmDesp).subscribe(async (res: any) => {
      this.blockUI.stop();
      if (res.correcto) {
        if (this.camposFrmDesp.despId === 1) { this.camposGenerales.utilizadoPA1 = this.camposFrmDesp.totalCapturado; }
        else if (this.camposFrmDesp.despId === 2) { this.camposGenerales.noUtilizadoPA1 = this.camposFrmDesp.totalCapturado; }
        else if (this.camposFrmDesp.despId === 3) { this.camposGenerales.utilizadoPA2 = this.camposFrmDesp.totalCapturado; }
        else if (this.camposFrmDesp.despId === 4) { this.camposGenerales.despPA = this.camposFrmDesp.totalCapturado; }
        else if (this.camposFrmDesp.despId === 5) { this.camposGenerales.contabilizadoPA = this.camposFrmDesp.totalCapturado; }
        else if (this.camposFrmDesp.despId === 6) { this.camposGenerales.noContabilizadoPA = this.camposFrmDesp.totalCapturado; }

        if (this.camposFrmDesp.aplicaCajaRec) { this.camposGenerales.cantidadCajasRec = this.camposFrmDesp.totalCapturado; }

        if (this.frmDesperdicioCptRef) { this.frmDesperdicioCptRef.close(); }

        this.mensajeFlotante('Registros actualizados correctamente', 3400);
      }
      else {
        Swal.fire('Información', res.data, 'info');
      }
    }, (err: any) => {
      this.blockUI.stop();
      Swal.fire('Error', 'Error: ' + err.error, 'error');
    });
  }

  // changeHojasGridPrincipal(e: any): void {
  //   const row: listaDatos = e.data;

  // }

  async gridValidacion_clickSeleccionar(obj: any): Promise<void> {
    const row: datosBusquedaProduccion = obj.data;
    this.blockUI.start('');
    try {
      if (row) {
        const res: any = await this.servicio.buscaTripulacionMaquina(
          row.claveMaquina
        );
        if (res.correcto && res.data.length > 0) {
          this.cbxTripulaciones.datos = res.data;
          this.cbxTripulaciones.selected = Number(res.data[0].idTripulacion);
        }
        this.camposGenerales.claveMaquina = row.claveMaquina;
        this.camposGenerales.turno = row.turno;
        this.camposGenerales.idUnicoProduccion = row.idUnico
          ? row.idUnico.toString().trim()
          : '';

        if (row.fecha) {
          this.camposGenerales.wFechaAnterior = row.fecha;
          this.camposGenerales.fecha = row.fecha;
        }

        const rsPa: any = await this.servicio.leePrograma(
          '',
          this.camposGenerales.programa,
          this.camposGenerales.claveMaquina,
          this.camposGenerales.turno
        );
        if (rsPa.correcto && rsPa.data && rsPa.data.length > 0) {
          const fila: Array<datosPrograma> = rsPa.data;
          this.camposGenerales.disabledPanelPA = false;
          this.camposGenerales.disabledMaquinaPA = false;
          this.camposGenerales.disabledBtnConceptos2 = false;

          if (this.pZonaERP === '01') {
            if (
              row.claveMaquina === 'CECSO' ||
              row.claveMaquina === 'CECS2' ||
              row.claveMaquina === 'WARD1'
            ) {
              this.camposGenerales.disabledPanelProcesoActual = true;
              this.camposGenerales.disabledMaquinaPA = true;
              this.camposGenerales.disabledBtnConceptos2 = true;
            } else {
              if (this.camposGenerales.claveProceso !== 'PEG') {
                this.camposGenerales.disabledPanelProcesoActual = true;
                this.camposGenerales.disabledMaquinaPA = true;
                this.camposGenerales.disabledBtnConceptos2 = true;
              }
            }
          }

          this.camposGenerales.maqPA = fila[0].maqPA
            ? fila[0].maqPA.trim()
            : '';
          this.cbxMaquinaProcAnt.selected = this.camposGenerales.maqPA;
          this.camposGenerales.despPA = fila[0].despPA;

          // OBTIENE LOS DESPERDICIOS CAPTURADOS
          // Antes de intentar llenar el grid revisa el nombre de la maquina
          if (row.claveMaquina) {
            const rs: any = await this.servicio.obtenerDesperdicios(
              row.op,
              this.camposGenerales.programa,
              row.turno
            );
            if (rs.correcto && rs.data) {
              const desperdicios: Array<desperdiciosCapturados> = rs.data;
              for (const iterator of desperdicios) {
                // CUANDO SEA CLAVEMAQUINADESP = CORR1 Y ESNOUTILIZADO = 0
                // CUANDO SEA CLAVEMAQUINADESP = CORR1 ES NO UTILIZADO = 1
                // CUANDO SEA DIFERENTE DE CORR1 Y ESNOUTILIZADO = 0
                if (
                  (iterator.claveMaquinaDesp
                    ? iterator.claveMaquinaDesp.trim()
                    : iterator.claveMaquinaDesp) === 'CORR1'
                ) {
                  // CORRUGADORA
                  if (iterator.esUtilizado) {
                    // CORRUGADORA NO UTILIZADO
                    this.camposGenerales.noUtilizadoPA1 =
                      iterator.totalDesperdicio;
                  } else {
                    // CORRUGADORA UTILIZADO
                    this.camposGenerales.utilizadoPA1 =
                      iterator.totalDesperdicio;
                  }
                } else {
                  // PROCESO ANTERIOR
                  if (iterator.esProcesoAnterior) {
                    if (iterator.esUtilizado) {
                      this.camposGenerales.utilizadoPA2 =
                        iterator.totalDesperdicio;
                    } else {
                      this.camposGenerales.despPA = iterator.totalDesperdicio;
                    }
                  } else {
                    // IMPRESORA
                    this.camposGenerales.contabilizadoPA =
                      iterator.totalDesperdicio;
                  }
                }
              }
            }
          } else {
            this.camposGenerales.utilizadoPA1 = 0;
            this.camposGenerales.noUtilizadoPA1 = 0;
            this.camposGenerales.utilizadoPA2 = 0;
            this.camposGenerales.despPA = 0;
            this.camposGenerales.contabilizadoPA = 0;
          }
        }

        // PENDIENTE
        await this.m2_txtTurno_Change();
        // await this.m3_txtMaquina_Change();
        await this.cargaCombo();

        await this.obtieneDespCapturado();
        await this.obtenerCajasRecuperdas();
      } else {
        this.mensajeFlotante(
          'Seleccione un registro para continuar...',
          2700,
          1
        );
      }
      this.blockUI.stop();
      this.mdlValidaRef.close();
    } catch (error) {
      this.blockUI.stop();
      this.mdlValidaRef.close();
      Swal.fire('Error', error.error ? error.error : error.message, 'error');
    }
  }

  async m2_txtTurno_Change(): Promise<void> {
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
        this.camposGenerales.fecha,
        this.camposGenerales.programa,
        this.camposGenerales.claveMaquina,
        this.camposGenerales.turno
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
      // this.camposGenerales.fecha = row.fecha ? row.fecha.trim() : this.camposGenerales.fecha.trim();
      this.camposGenerales.horaIni = row.horaInicio
        ? row.horaInicio.trim()
        : this.camposGenerales.horaIni.trim();
      this.camposGenerales.horaFin = row.horaTermino
        ? row.horaTermino.trim()
        : this.camposGenerales.horaFin.trim();
      // this.camposGenerales.desperdicioCorrugadora = row.laminaDespeg ? row.laminaDespeg : this.camposGenerales.desperdicioCorrugadora;
      // this.camposGenerales.desperdicioImpresora = row.laminaImpres ? row.laminaImpres : this.camposGenerales.desperdicioImpresora;
      // this.camposGenerales.desperdicioLinea = row.desperdicioAcabados ? row.desperdicioAcabados : this.camposGenerales.desperdicioLinea;
      this.camposGenerales.utilizadoPA1 = row.laminaDespeg
        ? row.laminaDespeg
        : this.camposGenerales.utilizadoPA1;
      this.camposGenerales.contabilizadoPA = row.laminaImpres
        ? row.laminaImpres
        : this.camposGenerales.contabilizadoPA;
      // this.camposGenerales.desperdicioLinea = row.desperdicioAcabados ? row.desperdicioAcabados : this.camposGenerales.desperdicioLinea;
      this.camposGenerales.cantidad = row.cantidad
        ? row.cantidad
        : this.camposGenerales.cantidad;
      this.camposGenerales.cantidadPrimerColor = row.canTinta1
        ? row.canTinta1
        : this.camposGenerales.cantidadPrimerColor;
      this.camposGenerales.cantidadSegundoColor = row.canTinta2
        ? row.canTinta2
        : this.camposGenerales.cantidadSegundoColor;
      this.camposGenerales.cantidadTercerColor = row.canTinta3
        ? row.canTinta3
        : this.camposGenerales.cantidadTercerColor;
      this.camposGenerales.cantidadCuartoColor = row.canTinta4
        ? row.canTinta4
        : this.camposGenerales.cantidadCuartoColor;

      this.camposGenerales.pesoLamina = row.pesoLamina
        ? row.pesoLamina
        : this.camposGenerales.pesoLamina;
      this.camposGenerales.pesoCaja = row.pesoCaja
        ? row.pesoCaja
        : this.camposGenerales.pesoCaja;
      this.camposGenerales.retrabajo = row.retrabajo
        ? row.retrabajo
        : this.camposGenerales.retrabajo;
      this.cbxSupervisor.selected = row.supervisor
        ? row.supervisor.trim()
        : this.cbxSupervisor.selected.trim();
      var tmpSup = this.cbxSupervisor.selected;
      for (const row of this.cbxSupervisor.datos) {
        if (row.idSupervisor.trim() === tmpSup) {
          this.cbxSupervisor.selected = tmpSup;
          break;
        } else {
          this.cbxSupervisor.selected = '';
        }
      }
      this.cbxTripulaciones.selected = row.idTripulacion
        ? row.idTripulacion
        : this.cbxTripulaciones.selected;
      var tmpTrip = this.cbxTripulaciones.selected;
      for (const row of this.cbxTripulaciones.datos) {
        if (row.idTripulacion === tmpTrip) {
          this.cbxTripulaciones.selected = tmpTrip;
          break;
        } else {
          this.cbxTripulaciones.selected = 0;
        }
      }
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
    this.camposGenerales.disabledBtnAcepta = true;
    // this.camposGenerales.desperdicioImpresora = 0;
    // this.camposGenerales.desperdicioCorrugadora = 0;
    // this.camposGenerales.desperdicioLinea = 0;
  }

  async inicializarCbxClavePreparacion(claveMaquina: string): Promise<void> {
    try {
      const res: any = await this.servicio.getClavesPreparacion(claveMaquina);
      this.cbxClaveProceso.datos = [];
      this.cbxClaveProceso.selected = '';
      this.cbxClaveProceso.selectedTmp = '';
      if (res.data && res.data.length > 0) {
        this.cbxClaveProceso.datos = res.data;
      }
    } catch (error) {
      Swal.fire('Error', error.error ? error.error : error.message, 'error');
    }
  }

  async btnBuscarPrograma(): Promise<void> {
    if (!this.camposGenerales.programa) {
      this.mensajeFlotante('Teclee un Número Válido de Programa', 3000, 1);
      return;
    }

    if (await this.buscaPrograma()) {
      if (await this.buscaProduccion()) {
        this.mdlValidaRef = this.modalService.open(this.mdlValida, {
          size: 'xl',
          backdrop: 'static',
        });
        this.camposGenerales.disabledPanelProcesoActual = false;
      } else {
        this.limpiarCampos();
        this.camposGenerales.disabledPanelProcesoActual = true;
      }
    } else {
      this.camposGenerales.disabledBtnAcepta = true;
    }
  }

  async obtieneDespCapturado(): Promise<void> {
    try {
      const rs: any = await this.servicio.obtenerDesperdicios(
        this.camposGenerales.op,
        this.camposGenerales.programa,
        this.camposGenerales.turno,
        '1'
      );
      if (rs.correcto && rs.data.length > 0) {
        this.camposGenerales.utilizadoPA1 = 0;
        this.camposGenerales.noUtilizadoPA1 = 0;
        this.camposGenerales.utilizadoPA2 = 0;
        this.camposGenerales.despPA = 0;
        this.camposGenerales.contabilizadoPA = 0;
        this.camposGenerales.noContabilizadoPA = 0;
        const desperdicios: Array<desperdiciosCapturados> = rs.data;
        for (const rowDesp of desperdicios) {
          // CUANDO SEA CLAVEMAQUINADESP = CORR1 Y EsUtilizado = 0
          // CUANDO SEA CLAVEMAQUINADESP = CORR1 ES NO UTILIZADO = 1
          // CUANDO SEA DIFERENTE DE CORR1 Y EsUtilizado = 0
          if (
            (rowDesp.claveMaquinaDesp
              ? rowDesp.claveMaquinaDesp.trim()
              : rowDesp.claveMaquinaDesp) === 'CORR1'
          ) {
            // CORRUGADORA
            if (rowDesp.esUtilizado) {
              // CORRUGADORA NO UTILIZADO
              this.camposGenerales.utilizadoPA1 = rowDesp.totalDesperdicio;
            } else {
              // CORRUGADORA UTILIZADO
              this.camposGenerales.noUtilizadoPA1 = rowDesp.totalDesperdicio;
            }
          } else {
            // PROCESO ANTERIOR
            if (rowDesp.esProcesoAnterior) {
              if (rowDesp.esUtilizado) {
                this.camposGenerales.utilizadoPA2 = rowDesp.totalDesperdicio;
              } else {
                this.camposGenerales.despPA = rowDesp.totalDesperdicio;
              }
            } else {
              // IMPRESORA
              if (!rowDesp.esContabilizadoPlc) {
                this.camposGenerales.noContabilizadoPA =
                  rowDesp.totalDesperdicio;
              } else {
                this.camposGenerales.contabilizadoPA = rowDesp.totalDesperdicio;
              }
            }
          }
        }
      }
    } catch (error) {
      Swal.fire('Error', error.error ? error.error : error.message, 'error');
    }
  }

  async obtenerCajasRecuperdas(): Promise<void> {
    try {
      this.camposGenerales.cantidadCajasRec = 0;
      const res: any = await this.servicio.cargaCantidadRecMod2(
        this.camposGenerales.op,
        this.camposGenerales.claveMaquina,
        this.camposGenerales.programa
      );
      if (res.correcto && res.data.length > 0) {
        for (const iterator of res.data) {
          this.camposGenerales.cantidadCajasRec = Number(iterator.cantidad);
        }
      }
    } catch (error) {
      Swal.fire('Error', error.error ? error.error : error.message, 'error');
    }
  }

  async cargaCombo(): Promise<void> {
    this.blockUI.start('Cargando información...');
    try {
      await this.cargaCbxSupervisor();
      const resSup: any = await this.servicio.cargaSupsMod2(
        this.camposGenerales.programa,
        this.camposGenerales.turno
      );
      if (resSup.correcto && resSup.data) {
        for (const iterator of resSup.data) {
          this.cbxSupervisor.selected = iterator.supervisor;
        }
      }
      await this.cargaCbxSuaje();
      await this.cargaCbxGrabados();
      this.blockUI.stop();
    } catch (error) {
      this.blockUI.stop();
      Swal.fire('Error', error.error ? error.error : error.message, 'error');
    }
  }

  async cargaCbxSupervisor(): Promise<void> {
    const res: any = await this.servicio.buscaSupervisor();
    this.cbxSupervisor = { selected: '', datos: [] };
    if (res.correcto && res.data.length > 0) {
      this.cbxSupervisor.datos = res.data;
    }
  }

  async cargaCbxSuaje(): Promise<void> {
    const res: any = await this.servicio.cargaSuajeMod2(
      this.camposGenerales.suaje
    );
    this.cbxSuaje = { selected: '', datos: [] };
    if (res.correcto && res.data.length > 0) {
      this.cbxSuaje.datos = res.data;
    } else {
      this.cbxSuaje.datos.push({
        codigoGrabadoSuaje: '0',
        descripcion: 'No disponible',
        compuesto: '0 - No Disponible',
      });
    }
  }

  async cargaCbxGrabados(): Promise<void> {
    const res: any = await this.servicio.cargaGrabadosMod2(
      this.camposGenerales.claveArticulo
    );
    this.cbxGrabado = { selected: '', datos: [] };
    if (res.correcto && res.data.length > 0) {
      this.cbxGrabado.datos = res.data;
    } else {
      this.cbxGrabado.datos.push({
        codigoGrabadoSuaje: '0',
        descripcion: 'No disponible',
        compuesto: '0 - No Disponible',
      });
    }
  }

  async cargaCbxMaqProcAnt(): Promise<void> {
    const res: any = await this.servicio.cargaMaqProcAntMod2();
    this.cbxMaquinaProcAnt = { selected: '', datos: [] };
    if (res.correcto && res.data.length > 0) {
      this.cbxMaquinaProcAnt.datos = res.data;
    }
  }

  async buscaPrograma(): Promise<boolean> {
    var correcto = false;

    try {
      const res: any = await this.servicio.buscaProgramaMod2(
        this.camposGenerales.programa
      );
      const datos: Array<camposGeneralesMod2> = res.data;
      if (datos && datos.length > 0) {
        for (const row of datos) {
          this.camposGenerales.disabledPanelProcesoActual = false;
          this.camposGenerales.disabledMaquinaPA = false;
          this.camposGenerales.disabledBtnConceptos2 = false;
          this.camposGenerales.claveMaquina = row.claveMaquina
            ? row.claveMaquina.trim()
            : '';
          this.camposGenerales.op = row.op ? row.op.trim() : '';
          this.camposGenerales.claveProceso = row.claveProceso
            ? row.claveProceso.trim()
            : '';
          this.camposGenerales.piezasCorte = row.piezasCorte;
          this.camposGenerales.proceso1 = row.proceso1;
          this.camposGenerales.tipoMaquina = row.tipoMaquina
            ? row.tipoMaquina.trim()
            : '';
          this.camposGenerales.cantidad = row.cantidad;
          this.camposGenerales.programado = row.cantidad;
          this.camposGenerales.ultimoProceso = row.ultimoProceso;
          this.camposGenerales.pegado = row.pegado;
          this.camposGenerales.primerColor = row.primerColor
            ? row.primerColor.trim()
            : '';
          this.camposGenerales.segundoColor = row.segundoColor
            ? row.segundoColor.trim()
            : '';
          this.camposGenerales.tercerColor = row.tercerColor
            ? row.tercerColor.trim()
            : '';
          this.camposGenerales.cuartoColor = row.cuartoColor
            ? row.cuartoColor.trim()
            : '';
          this.camposGenerales.areaUnitaria = row.areaUnitaria;
          this.camposGenerales.pesoUnitario = row.pesoUnitario;
          this.camposGenerales.claveArticulo = row.claveArticulo
            ? row.claveArticulo.trim()
            : '';
          this.camposGenerales.articulo = row.articulo
            ? row.articulo.trim()
            : '';
          this.camposGenerales.suajeOld = row.suajeOld
            ? row.suajeOld.trim()
            : '';
          this.camposGenerales.suaje = row.suaje ? row.suaje.trim() : '';
          this.camposGenerales.liberadoCostos = row.liberadoCostos;
          this.camposGenerales.notasOperacion = row.notasOperacion
            ? row.notasOperacion.trim()
            : '';
          this.camposGenerales.eficiencia = row.eficiencia;
          this.camposGenerales.eficienciaAct = row.eficiencia;
          this.camposGenerales.wEficiencia = row.eficienciaAct;
          this.camposGenerales.sinPreparacion = row.sinPreparacion;
          this.camposGenerales.canTinta1 = row.canTinta1;
          this.camposGenerales.canTinta2 = row.canTinta2;
          this.camposGenerales.canTinta3 = row.canTinta3;
          this.camposGenerales.canTinta4 = row.canTinta4;
          this.camposGenerales.supervisor = row.supervisor
            ? row.supervisor.trim()
            : '';
          this.camposGenerales.proceso = row.proceso ? row.proceso.trim() : '';
          this.camposGenerales.minPrep = row.minPrep;
          this.camposGenerales.velocidad = row.velocidad;
          this.camposGenerales.velocidadAnt = row.velocidad;
          this.camposGenerales.minStdProd = row.minStdProd;
          this.camposGenerales.minStdAnt = row.minStdProd;
          this.camposGenerales.maqPA = row.maqPA ? row.maqPA.trim() : '';
          this.cbxMaquinaProcAnt.selected = this.camposGenerales.maqPA;
          this.camposGenerales.despPA = row.despPA;
          this.camposGenerales.disabledPanelProcesoActual = false;
          this.camposGenerales.disabledMaquinaPA = false;
          this.camposGenerales.disabledBtnConceptos2 = false;
          this.cbxClaveProceso.selected = this.camposGenerales.claveProceso;
          this.cbxClaveProceso.selectedTmp = this.camposGenerales.claveProceso;

          if (this.pZonaERP === '01') {
            if (
              this.camposGenerales.claveMaquina === 'CECSO' ||
              this.camposGenerales.claveMaquina === 'CECS2' ||
              this.camposGenerales.claveMaquina === 'WARD1'
            ) {
              this.camposGenerales.disabledPanelProcesoActual = true;
              this.camposGenerales.disabledMaquinaPA = true;
              this.camposGenerales.disabledBtnConceptos2 = true;
            } else {
              if (this.camposGenerales.claveProceso !== 'PEG') {
                this.camposGenerales.disabledPanelProcesoActual = true;
                this.camposGenerales.disabledMaquinaPA = true;
                this.camposGenerales.disabledBtnConceptos2 = true;
              }
            }
          }

          if (row.tipoMaquina !== 'IM') {
            Swal.fire(
              'Información',
              'Programa generado para Acabados/Especialidades... Captura de Producción Inválida',
              'info'
            );
          } else {
            if (row.liberadoCostos) {
              Swal.fire(
                'Información',
                'OP Liberada para costos... Captura de Producción Inválida',
                'info'
              );
            } else {
              correcto = true;
            }
          }
        }
      }
    } catch (error) {
      Swal.fire('Error', error.error ? error.error : error.message, 'error');
    }

    return correcto;
  }

  async buscaProduccion(): Promise<boolean> {
    var correcto = false;

    try {
      const res: any = await this.servicio.buscaProduccion(
        this.camposGenerales.programa
      );
      if (res.correcto && res.data && res.data.length > 0) {
        correcto = true;
        this.datosGridValidacion = res.data;
      } else {
        this.mensajeFlotante(
          'No existe Producción registrada para este Programa...',
          3500,
          1
        );
      }
    } catch (error) {
      Swal.fire('Error', error.error ? error.error : error.message, 'error');
    }

    return correcto;
  }

  m2_txtUtilizado1_focus(): void {
    this.controlFocusName = this.txtDespCorrUtil;
  }
  m2_txtNoUtilizado1_focus(): void {
    this.controlFocusName = this.txtDespCorrNoUtil;
  }
  m2_txtUtilizado2_focus(): void {
    this.controlFocusName = this.txtDesPAUtul;
  }
  m2_txtNoUtilizado2_focus(): void {
    this.controlFocusName = this.txtDespPANoUtil;
  }
  m2_txtContabilizado_focus(): void {
    this.controlFocusName = this.txtDespImprUtil;
  }
  m2_txtNoContabilizado_focus(): void {
    this.controlFocusName = this.txtImpresoraNoCon;
  }
  m2_txtCantidadCajasRec_focus(): void {
    this.controlFocusName = this.txtImpresoraNoCon;
  }

  // mdlDesp_Filtro(): void {
  //   try {
  //     const index: number = this.mdlGridConceptos_selectConcepto(this.camposFrmDesp.filtro.trim());
  //     this.mdlCon.ensureIndexVisible(index);
  //   } catch (error) {
  //     Swal.fire('Error', error.error ? error.error : error.message, 'error');
  //   }
  // }
  // mdlGridConceptos_selectConcepto(filtro: string): number {
  //   this.mdlCon.select({key: 'concepto', rows: [{concepto: filtro}]});
  //   filtro = filtro + '%';
  //   for (let index = 0; index < this.camposFrmDesp.gridDatos.length; index++) {
  //     const element = this.camposFrmDesp.gridDatos[index];
  //     if (element.concepto.match(filtro)) {
  //       return index;
  //     }
  //   }
  //   return 0;
  // }

  txtDespCorrUtil: string = 'm2_txtUtilizado1';
  txtDespCorrNoUtil: string = 'm2_txtNoUtilizado1';
  txtDesPAUtul: string = 'm2_txtUtilizado2';
  txtDespPANoUtil: string = 'm2_txtNoUtilizado2';
  txtDespImprUtil: string = 'm2_txtContabilizado';
  txtImpresoraNoCon: string = 'm2_txtNoContabilizado';
  txtCantidadCajasRec: string = 'm2_txtCantidadCajasRec';
  camposFrmDesp: camposFrmDesp;

  async btnModalConceptosPA1(): Promise<void> {
    this.camposFrmDesp.modalHabilitado = false;
    if (this.controlFocusName === this.txtDespCorrUtil || this.controlFocusName === this.txtDespCorrNoUtil) {
      this.limpiarMdlConceptosDesp();
      this.camposFrmDesp.aplicaCajaRec = false;
      this.camposFrmDesp.filtro = '';
      this.camposFrmDesp.programa = this.camposGenerales.programa;
      this.camposFrmDesp.maquinaDesperdicio = 'CORR1';
      this.camposFrmDesp.claveMaquinaCap = this.camposGenerales.claveMaquina;
      this.camposFrmDesp.op = this.camposGenerales.op;
      this.camposFrmDesp.capturaDesperdicio = true;
      this.camposFrmDesp.esUtilizado = this.controlFocusName === this.txtDespCorrUtil ? true : false;
      this.camposFrmDesp.esContabilizadoPLC = this.controlFocusName === this.txtDespCorrUtil ? true : false;
      this.camposFrmDesp.esProcesoAnterior = true;
      this.camposFrmDesp.despId = this.controlFocusName === this.txtDespCorrUtil ? 1 /*UTILIZADO*/ : 2 /*NO UTILIZADO*/;
      this.camposFrmDesp.despTurno = this.camposGenerales.turno ? Number(this.camposGenerales.turno) : 0;
      await this.cargaModalFrmDesperdicioCpt();
      this.camposFrmDesp.modalHabilitado = true;
      this.frmDesperdicioCptRef = this.modalService.open(this.frmDesperdicioCpt, {
        size: 'xl',
        backdrop: 'static',
      });
    }
  }

  async cargaModalFrmDesperdicioCpt(): Promise<void> {
    this.blockUI.start('');
    try {
      this.camposFrmDesp.gridDatos = [];
      this.camposFrmDesp.totalCapturado = 0;
      const res: any = await this.servicio.cargaConceptosDesp(
        this.camposFrmDesp.maquinaDesperdicio, this.camposFrmDesp.op, this.camposFrmDesp.programa, this.camposFrmDesp.claveMaquinaCap,
        this.camposGenerales.turno, this.camposFrmDesp.aplicaCajaRec, this.camposFrmDesp.esUtilizado, this.camposFrmDesp.esContabilizadoPLC,
        this.camposFrmDesp.esProcesoAnterior
      );
      if (res.correcto && res.data.length > 0) {
        const conceptos: Array<gridFrmDesp> = res.data;
        this.camposFrmDesp.gridDatos = conceptos;
        for (const row of conceptos) {
          this.camposFrmDesp.totalCapturado = this.camposFrmDesp.totalCapturado + row.cantidad;
        }
      }
      this.blockUI.stop();
    } catch (error) {
      this.blockUI.stop();
      Swal.fire('Error', error.error ? error.error : error.message, 'error');
    }
  }

  async btnModalConceptosPA2(): Promise<void> {
    this.camposFrmDesp.modalHabilitado = false;
    if (this.controlFocusName === this.txtDesPAUtul || this.controlFocusName === this.txtDespPANoUtil) {
      if (!this.cbxMaquinaProcAnt.selected) {
        this.mensajeFlotante('Favor de seleccionar una maquina valida', 3400, 1);
        return;
      }
      this.limpiarMdlConceptosDesp();
      this.camposFrmDesp.aplicaCajaRec = false;
      this.camposFrmDesp.filtro = '';
      this.camposFrmDesp.programa = this.camposGenerales.programa;
      this.camposFrmDesp.maquinaDesperdicio = this.cbxMaquinaProcAnt.selected;
      this.camposFrmDesp.claveMaquinaCap = this.camposGenerales.claveMaquina;
      this.camposFrmDesp.op = this.camposGenerales.op;
      this.camposFrmDesp.capturaDesperdicio = true;
      this.camposFrmDesp.esUtilizado = this.controlFocusName === this.txtDespPANoUtil ? false : true;
      this.camposFrmDesp.esContabilizadoPLC = this.controlFocusName === this.txtDespPANoUtil ? false : true;
      this.camposFrmDesp.esProcesoAnterior = true;
      this.camposFrmDesp.despId = this.controlFocusName === this.txtDespPANoUtil ? 4 /*NO UTILIZADO*/ : 3 /*UTILIZADO*/;
      this.camposFrmDesp.despTurno = this.camposGenerales.turno ? Number(this.camposGenerales.turno) : 0;
      await this.cargaModalFrmDesperdicioCpt();
      this.camposFrmDesp.modalHabilitado = true;
      this.frmDesperdicioCptRef = this.modalService.open(this.frmDesperdicioCpt, {
        size: 'xl',
        backdrop: 'static',
      });
    }
  }

  async btnModalConceptosPA3(): Promise<void> {
    this.camposFrmDesp.modalHabilitado = false;
    if (this.controlFocusName === this.txtDespImprUtil || this.controlFocusName === this.txtImpresoraNoCon) {
      this.limpiarMdlConceptosDesp();
      this.camposFrmDesp.aplicaCajaRec = false;
      this.camposFrmDesp.filtro = '';
      this.camposFrmDesp.programa = this.camposGenerales.programa;
      this.camposFrmDesp.maquinaDesperdicio = this.camposGenerales.claveMaquina;
      this.camposFrmDesp.claveMaquinaCap = this.camposGenerales.claveMaquina;
      this.camposFrmDesp.op = this.camposGenerales.op;
      this.camposFrmDesp.capturaDesperdicio = true;
      this.camposFrmDesp.esUtilizado = true;
      this.camposFrmDesp.esContabilizadoPLC = this.controlFocusName === this.txtDespImprUtil ? true : false;
      this.camposFrmDesp.esProcesoAnterior = false;
      this.camposFrmDesp.despId = this.controlFocusName === this.txtDespImprUtil ? 5 /*CONTABILIZADO*/ : 6 /*NO CONTABILIZADO*/;
      this.camposFrmDesp.despTurno = this.camposGenerales.turno ? Number(this.camposGenerales.turno) : 0;
      await this.cargaModalFrmDesperdicioCpt();
      this.camposFrmDesp.modalHabilitado = true;
      this.frmDesperdicioCptRef = this.modalService.open(this.frmDesperdicioCpt, {
        size: 'xl',
        backdrop: 'static',
      });
    }
  }

  async btnModalConceptosCajasRec(): Promise<void> {
    this.camposFrmDesp.modalHabilitado = false;
    if (this.controlFocusName === this.txtCantidadCajasRec) {
      this.limpiarMdlConceptosDesp();
      this.camposFrmDesp.aplicaCajaRec = true;
      this.camposFrmDesp.filtro = '';
      this.camposFrmDesp.programa = this.camposGenerales.programa;
      this.camposFrmDesp.maquinaDesperdicio = this.camposGenerales.claveMaquina;
      this.camposFrmDesp.op = this.camposGenerales.op;
      this.camposFrmDesp.claveMaquinaCap = this.camposGenerales.claveMaquina;
      this.camposFrmDesp.despId = 5;
      this.camposFrmDesp.despTurno = this.camposGenerales.turno ? Number(this.camposGenerales.turno) : 0;
      await this.cargaModalFrmDesperdicioCpt();
      this.camposFrmDesp.modalHabilitado = true;
      this.frmDesperdicioCptRef = this.modalService.open(this.frmDesperdicioCpt, {
        size: 'xl',
        backdrop: 'static',
      });
    }
  }

  async btnGuardar(): Promise<void> {
    if (await this.validaDatos()) {
      await this.actualiza();
    }
  }

  async validaDatos(): Promise<boolean> {
    var correcto = false;

    if (!this.camposGenerales.programa) {
      this.mensajeFlotante('Seleccione un Programa...', 2700, 1);
      return correcto;
    }
    if (!this.cbxSupervisor.selected) {
      this.mensajeFlotante('Seleccione un Supervisor...', 2700, 1);
      return correcto;
    }

    if (this.camposGenerales.disabledCbxClavePreparacion) {
      if (!this.cbxClaveProceso.selected) {
        this.mensajeFlotante('Seleccione una Clave de Preparación...', 2700, 1);
        return correcto;
      }
    }

    if (this.camposGenerales.wFechaAnterior !== this.camposGenerales.fecha) {
      try {
        const res: any = await this.servicio.validarGuardado(
          this.camposGenerales.programa, this.camposGenerales.claveMaquina, this.camposGenerales.turno, this.camposGenerales.fecha
        );
        if (res.correcto && res.data.length > 0) {
          Swal.fire({
            title: 'Información',
            html: 'Ya existe Registro de Producción el Turno ' + this.camposGenerales.turno + '<br>' +
              'Del día ' + this.camposGenerales.fecha + '... Verifique',
            icon: 'info'
          });
          return correcto;
        }
      } catch (error) {
        Swal.fire('Error', error.error ? error.error : error.message, 'error');
        return correcto;
      }
    }

    try {
      const res: any = await this.servicio.validaDatosSupervisorMod2(
        this.camposGenerales.claveMaquina, this.camposGenerales.turno, this.camposGenerales.fecha,
        this.cbxSupervisor.selected, this.camposGenerales.programa
      );
      if (res.correcto && res.data.length > 0) {
        var supervisorPrograma = '';
        const dt: Array<validaDatosSupervisor> = res.data;
        for (const row of dt) {
          supervisorPrograma = supervisorPrograma + 'Supervisor: ' + (row.nombreSupervisor ? row.nombreSupervisor.trim() : '') +
            ' - Programa: ' + (row.programa ? row.programa.toString() : '0') + '<br>';
        }

        if (supervisorPrograma) {
          Swal.fire({
            title: 'Atención',
            html: 'Usted está indicando como supervisor a : ' +
              (
                this.cbxSupervisor.datos.filter(row => row.idSupervisor === this.cbxSupervisor.selected)[0].supervisor
                ? this.cbxSupervisor.datos.filter(row => row.idSupervisor === this.cbxSupervisor.selected)[0].supervisor.trim()
                : this.cbxSupervisor.selected
              ) + ', Pero se han encontrado registros de Producción para esta fecha, turno y máquina con los siguientes datos: ' +
              supervisorPrograma + '<br><br>¿Desea continuar y agregar el registro con un Supervisor distinto al de los registros anteriores?',
            icon: 'question',
            showCancelButton: true,
            confirmButtonColor: "#28A745",
            cancelButtonColor: "#DC3545",
            cancelButtonText: '<i class="fa fa-times"></i> Cancelar',
            confirmButtonText: '<i class="fa fa-check"></i> Aceptar'
          }).then((result) => {
            if (result.isConfirmed) {
              correcto = true;
            }
            else {
              correcto = false;
              return correcto;
            }
          });
        }
      }

      if (!this.camposGenerales.pesoCaja || !this.camposGenerales.pesoLamina) {
        Swal.fire('Información', 'Captura de peso incorrecto, Favor de introducir un peso de caja o lamina mayor a cero.', 'info');
        correcto = false;
        return correcto;
      }
      if (this.camposGenerales.pesoCaja > this.camposGenerales.pesoLamina) {
        Swal.fire('Información', 'Captura de peso incorrecto, El peso de la caja no puede ser superior al peso de lamina.', 'info');
        correcto = false;
        return correcto;
      }
      if (this.camposGenerales.pesoLamina < this.camposGenerales.pesoCaja) {
        Swal.fire('Información', 'Captura de peso incorrecto, El peso de la Caja no puede ser superior al peso de la Lamina.', 'info');
        correcto = false;
        return correcto;
      }

      if (this.camposGenerales.pesoLamina >= 8) {
        Swal.fire({
          title: '¿Desea continuar?',
          text: 'Se esta capturando más de 8 KG en peso de Lamina.',
          icon: 'question',
          showCancelButton: true,
          confirmButtonColor: "#28A745",
          cancelButtonColor: "#DC3545",
          cancelButtonText: '<i class="fa fa-times"></i> Cancelar',
          confirmButtonText: '<i class="fa fa-check"></i> Aceptar'
        }).then((result) => {
          if (result.isConfirmed) {
            correcto = true;
          }
          else {
            correcto = false;
            return correcto;
          }
        });
      }

      if (this.camposGenerales.pesoCaja >= 8) {
        Swal.fire({
          title: '¿Desea continuar?',
          text: 'Se esta capturando más de 8 KG en peso de Caja.',
          icon: 'question',
          showCancelButton: true,
          confirmButtonColor: "#28A745",
          cancelButtonColor: "#DC3545",
          cancelButtonText: '<i class="fa fa-times"></i> Cancelar',
          confirmButtonText: '<i class="fa fa-check"></i> Aceptar'
        }).then((result) => {
          if (result.isConfirmed) {
            correcto = true;
          }
          else {
            correcto = false;
            return correcto;
          }
        });
      }

    } catch (error) {
      Swal.fire('Error', error.error ? error.error : error.message, 'error');
      return correcto;
    }

    return correcto;
  }

  camposGuardar: camposGuardado;
  async actualiza(): Promise<void> {
    var lMin = 0;
    lMin = await this.dateDiff(this.camposGenerales.horaIni, this.camposGenerales.horaFin);
    this.camposGuardar = {
      ignoraTiempo: this.camposGenerales.sinPreparacion,
      idUnico: Number(this.camposGenerales.idUnicoProduccion),
      wFechaAnterior: this.camposGenerales.wFechaAnterior,
      chkProceso: this.camposGenerales.ultimoProceso, // ULTIMO PROCESO
      lSupervisor: this.cbxSupervisor.selected, // CLAVE SUPERVISOR
      lSuaje: this.cbxSuaje.selected,
      lGrabado: this.cbxGrabado.selected,
      lMinutos: lMin < 0 ? 1440 + lMin : lMin,
      lProceso: this.cbxClaveProceso.selected, // CLAVE PREPARACION
      lVelocidad: this.camposGenerales.velocidad,
      lEficiencia: this.camposGenerales.eficiencia,
      lMinStd: this.camposGenerales.minPrep,
      programa: this.camposGenerales.programa,
      minStdProd: this.camposGenerales.minStdProd,
      chkMP: this.camposGenerales.disabledCbxClavePreparacion,
      txtDespCorrUtil: this.camposGenerales.utilizadoPA1, // UTILIZADO PA1
      txtDespImprUtil: this.camposGenerales.noContabilizadoPA, // CONTABILIZADO PA
      txtPesoLamina: this.camposGenerales.pesoLamina,
      txtPesoCaja: this.camposGenerales.pesoCaja,
      txtRetrabajo: this.camposGenerales.retrabajo,
      cmbMaquinaPA: this.cbxMaquinaProcAnt.selected, // MAQUINA PA2
      txtDesPAUtul: this.camposGenerales.utilizadoPA2, // UTILIZADO PA2
      cant1: this.camposGenerales.cantidadPrimerColor,
      cant2: this.camposGenerales.cantidadSegundoColor,
      cant3: this.camposGenerales.cantidadTercerColor,
      cant4: this.camposGenerales.cantidadCuartoColor,
      txtCantidad: this.camposGenerales.cantidad,
      txtCantidadCajasRec: this.camposGenerales.cantidadCajasRec,
      fecProduccion: this.camposGenerales.fecha,
      horaIni: this.camposGenerales.horaIni,
      horaFin: this.camposGenerales.horaFin,
      turno: Number(this.camposGenerales.turno),
      idTripulacion: this.cbxTripulaciones.selected,
      cmbMaquina: this.camposGenerales.claveMaquina
    };

    this.blockUI.start('Guardando información');
    this.servicio.gardarDatosMod2(this.camposGuardar).subscribe(async (res: any) => {
      this.blockUI.stop();
      if (res.correcto) {
        this.mensajeFlotante('Guardado correcto');
      }
    }, (err: any) => {
      this.blockUI.stop();
      Swal.fire('Error', 'Error: ' + err.error, 'error');
    });
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
