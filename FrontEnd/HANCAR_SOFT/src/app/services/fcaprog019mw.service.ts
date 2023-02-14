import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

const URL_PROGRAMACION = environment.FCAPROGAPI001 + 'FCAPROG019MW/';

@Injectable({
  providedIn: 'root'
})
export class Fcaprog019mwService {

  constructor(public http: HttpClient) { }

  async iniciaProduccion(): Promise<object> {
    const url = URL_PROGRAMACION + 'iniciaProduccion';
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
}
