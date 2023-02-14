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
export class listaTripulacion {
  idTripulacion: string;
  tripulacion: string;
}
export class datosTripulaciones {
  selected: string;
  datos: Array<listaTripulacion>;
}
