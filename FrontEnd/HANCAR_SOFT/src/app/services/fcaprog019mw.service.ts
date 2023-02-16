import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { objGuardar } from '../models/DTO/fcaprog019mw';

const URL_PROGRAMACION = environment.FCAPROGAPI001 + 'FCAPROG019MW/';

@Injectable({
  providedIn: 'root'
})
export class Fcaprog019mwService {

  constructor(public http: HttpClient) { }

  async leeHoraLocal(): Promise<object> {
    const url = URL_PROGRAMACION + 'leeHoraLocal';
    // const params = new HttpParams()
    return await this.http.get(url/*, {params}*/).toPromise();
  }
  async buscaPrograma(programa: number): Promise<object> {
    const url = URL_PROGRAMACION + 'buscaPrograma';
    const params = new HttpParams()
      .append("programa", programa.toString().trim());
    return await this.http.get(url, {params}).toPromise();
  }
  async buscaProduccion(programa: number): Promise<object> {
    const url = URL_PROGRAMACION + 'buscaProduccion';
    const params = new HttpParams()
      .append("programa", programa.toString().trim());
    return await this.http.get(url, {params}).toPromise();
  }
  async buscaTripulacionMaquina(claveMaquina: string): Promise<object> {
    const url = URL_PROGRAMACION + 'buscaTripulacionMaquina';
    const params = new HttpParams()
      .append("claveMaquina", claveMaquina.trim());
    return await this.http.get(url, {params}).toPromise();
  }
  async leePrograma(fechaAnterior: string, programa: number, claveMaquina: string, turno: string): Promise<object> {
    const url = URL_PROGRAMACION + 'leePrograma';
    const params = new HttpParams()
      .append("fechaAnterior", fechaAnterior.trim())
      .append("programa", programa.toString().trim())
      .append("claveMaquina", claveMaquina.trim())
      .append("turno", turno.trim());
    return await this.http.get(url, {params}).toPromise();
  }
  async buscaSupervisor(): Promise<object> {
    const url = URL_PROGRAMACION + 'buscaSupervisor';
    // const params = new HttpParams()
    return await this.http.get(url/*, {params}*/).toPromise();
  }
  async buscaParafina(): Promise<object> {
    const url = URL_PROGRAMACION + 'buscaParafina';
    // const params = new HttpParams()
    return await this.http.get(url/*, {params}*/).toPromise();
  }
  async validarGuardado(programa: number, claveMaquina: string, turno: string, fecha: string): Promise<object> {
    const url = URL_PROGRAMACION + 'validarGuardado';
    const params = new HttpParams()
      .append("programa", programa.toString().trim())
      .append("claveMaquina", claveMaquina.trim())
      .append("turno", turno.trim())
      .append("fecha", fecha.trim());
    return await this.http.get(url, {params}).toPromise();
  }
  async guardar(obj: objGuardar): Promise<object> {
    const url = URL_PROGRAMACION + 'guardar';
    const params = new HttpParams()
      .append('fecha', obj.fecha)
      .append('horaIni', obj.horaIni)
      .append('horaFin', obj.horaFin)
      .append('turno', obj.turno.toString())
      .append('supervisor', obj.supervisor)
      .append('minutos', obj.minutos.toString())
      .append('despCorrguradora', obj.despCorrguradora.toString())
      .append('despImpresora', obj.despImpresora.toString())
      .append('despAcabados', obj.despAcabados.toString())
      .append('fechaNow', obj.fechaNow)
      .append('parafina', obj.parafina)
      .append('pesoLamina', obj.pesoLamina.toString())
      .append('pesoCaja', obj.pesoCaja.toString())
      .append('retrabajo', obj.retrabajo.toString())
      .append('actCantidad', obj.actCantidad.toString())
      .append('idTripulacion', obj.idTripulacion.toString())
      .append('programa', obj.programa.toString())
      .append('claveMaquina', obj.claveMaquina)
      .append('wFechaAnterior', obj.wFechaAnterior)
      .append('idUnico', obj.idUnico.toString())
    return await this.http.get(url, {params}).toPromise();
  }

  // METODOS DE PAGINA L
  async cargaComboMaquinas(): Promise<object> {
    const url = URL_PROGRAMACION + 'cargaComboMaquinas';
    // const params = new HttpParams()
    return await this.http.get(url/*, {params}*/).toPromise();
  }
  async buscaProgramas(fecha: string, fechaF: string, turno: string, claveMaquina: string, sinFechaProd: boolean): Promise<object> {
    const url = URL_PROGRAMACION + 'buscaProgramas';
    const params = new HttpParams()
      .append('fecha', fecha)
      .append('fechaF', fechaF)
      .append('turno', turno)
      .append('claveMaquina', claveMaquina)
      .append('sinFechaProd', sinFechaProd ? '1' : '0');
    return await this.http.get(url, {params}).toPromise();
  }
}
