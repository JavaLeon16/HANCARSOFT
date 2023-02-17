export class fcaprog019mw {
  fecha: string;
}
export class camposGenerales {
  // pIdUnicoProduccion: string;
  // pFecha: string;
  // pHoraIni: string;
  // pHoraFin: string;
  // pPrograma: number;
  // pUltimoProceso: boolean;
  wFechaAnterior: string;
  idUnicoProduccion: string;
  programa: number;
  turno: string;
  fecha: string;
  horaIni: string;
  horaFin: string;
  desperdicioImpresora: number;
  desperdicioCorrugadora: number;
  desperdicioLinea: number;
  pesoLamina: number;
  pesoCaja: number;
  retrabajo: number;
  disabledParafina: boolean;
  disabledBtnAcepta: boolean;

  claveMaquina: string;
  op: string;
  claveProceso: string;
  piezasCorte: number;
  tipoMaquina: string;
  cantidad: number;
  ultimoProceso: boolean;
  pegado: boolean;
  primerColor: string;
  segundoColor: string;
  tercerColor: string;
  cuartoColor: string;
  areaUnitaria: number;
  pesoUnitario: number;
  claveArticulo: string;
  articulo: string;
  liberadoCostos: boolean;
}
export class datosBusquedaPrograma {
  claveMaquina: string;
  op: string;
  claveProceso: string;
  piezasCorte: number;
  tipoMaquina: string;
  cantidad: number;
  ultimoProceso: boolean;
  pegado: boolean;
  primerColor: string;
  segundoColor: string;
  tercerColor: string;
  cuartoColor: string;
  areaUnitaria: number;
  pesoUnitario: number;
  claveArticulo: string;
  articulo: string;
  liberadoCostos: boolean;
}
export class datosBusquedaProduccion {
  claveMaquina: string;
  op: string;
  fecha: string;
  turno: string;
  cantidad: number;
  fechaSistema: string;
  idUnico: number;
}
export class comboTripulacion {
  idTripulacion: number;
  tripulacion: string;
}
export class cbxTripulaciones {
  selected: number;
  datos: Array<comboTripulacion>;
}
export class datosPrograma {
  programa: number;
  turno: string;
  supervisor: string;
  claveMaquina: string;
  op: string;
  fecha: string;
  horaInicio: string;
  horaTermino: string;
  cantidad: number;
  pickUp: number;
  minutosProduccion: number;
  desperdicioAcabados: number;
  laminaDespeg: number;
  laminaComba: number;
  laminaMedidas: number;
  laminaImpres: number;
  laminaDimens: number;
  laminaPegad: number;
  fechaSistema: string;
  usuario: string;
  excedente: number;
  expCostos: boolean;
  fechaExp: string;
  laminasDesperdicio: number;
  piezasDesperdicio: number;
  canTinta1: number;
  canTinta2: number;
  canTinta3: number;
  canTinta4: number;
  tipoParafina: string;
  sinPreparacion: boolean;
  verificaAduana: boolean;
  produccionPT: number;
  minutosFT: number;
  claveInspector: number;
  pesoLamina: number;
  pesoCaja: number;
  retrabajo: number;
  maqPA: string;
  despPA: number;
  idTripulacion: number;
  despProdEnProc: number;
  motivo: string;
  preAlimentador: boolean;
  fechaInsert: string;
  despCorrNoUtil: number;
  prodProcesoCap: number;
  despPAUtil: number;
  despImpNoConPLC: number;
  idUnico: number;
  moduloInsert: string;
  posicion: number;
  desEtFront: number;
  desEtBack: number;
  cajasComProPLC: number;
  contabilizadoGolpesXSuaje: boolean;
  verificaRuta: boolean;
  minutosComedor: number;
  cajasRecEnProd: number;
}
export class comboSupervisor {
  idSupervisor: string;
  supervisor: string;
}
export class cbxSupervisor {
  selected: string;
  datos: Array<comboSupervisor>;
}
export class comboParafina {
  idParafina: number;
  parafina: string;
}
export class cbxParafina {
  selected: number;
  datos: Array<comboParafina>;
}
export class comboMaquinas {
  claveMaquina: string;
}
export class cbxMaquinas {
  selected: string;
  datos: Array<comboMaquinas>;
}

export class objGuardar {
  fecha: string;
  horaIni: string;
  horaFin: string;
  turno: number;
  supervisor: string;
  minutos: number;
  despCorrguradora: number;
  despImpresora: number;
  despAcabados: number;
  fechaNow: string;
  parafina: string;
  pesoLamina: number;
  pesoCaja: number;
  retrabajo: number;
  actCantidad: number;
  idTripulacion: number;
  programa: number;
  claveMaquina: string;
  wFechaAnterior: string;
  idUnico: number;
}

export class camposGeneralesL {
  pFechaDel: string;
  pFechaAl: string;
  pFechaProduccion: string;
  pSeleccionarTodos: boolean;
  pTurno: string;
  pSinFechaProd: boolean;
}
export class listProgramasL {
  sel: boolean;
  programa: number;
  maquina: string;
  turno: string;
  op: string;
  idUnico: number;
  fecha: string;
}

export class programas {
  fecha: string;
  supervisor: string;
  idTripulacion: number;
  idUnico: number;
}
export class programasSeleccionadosL {
  programasSeleccionados: Array<programas>;
}
