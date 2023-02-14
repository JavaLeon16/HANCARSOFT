import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';

const URL_FCAPROGAPI001 = environment.FCAPROGAPI001;
const URL_FCAPROGAPI002 = environment.FCAPROGAPI002;

@Injectable({
  providedIn: 'root',
})
export class fcaprogapi001Service {
  constructor(public http: HttpClient) {}

  ListarMaquina(): any {
    const url = URL_FCAPROGAPI002 + 'DisponibilidadMaquina/GetMaquina';
    return this.http.get(url);
  }
  ListarProcesoCosto(TipoMaquina: string): any {
    const url = URL_FCAPROGAPI001 + 'Proceso/GetProcesoCosto';
    const params = new HttpParams()
      .append('TipoMaquina', TipoMaquina);
    return this.http.get(url, { params });
  }
  ListarTipoMaquina(): any {
    const url = URL_FCAPROGAPI001 + 'Proceso/GetTipoMaquina';
    return this.http.get(url);
  }
  ListarFlauta(): any {
    const url = URL_FCAPROGAPI001 + 'Combinaciones/GetFlautas';
    return this.http.get(url);
  }
  MdlPapel(par: any, fil: any): any {
    const url = URL_FCAPROGAPI001 + 'Combinaciones/getPapel';
    const params = new HttpParams()
      .append('startRow', par.startRow)
      .append('endRow', par.endRow)
      .append('filtro', fil.filtro)
      .append('TipoPapel', fil.TipoPapel);
    return this.http.get(url, { params });
  }
  GetListaArea(EsAreaCaptura: any): any {
    const url = URL_FCAPROGAPI001 + 'Desperdicios/getListaArea';
    const params = new HttpParams()
      .append('EsAreaCaptura', EsAreaCaptura);
    return this.http.get(url, { params });
  }
  ListaDesperdicio(): any {
    const url = URL_FCAPROGAPI001 + 'Desperdicios/getListaDesperdicio';
    return this.http.get(url);
  }
  ListarVariacion(): any {
    const url = URL_FCAPROGAPI002 + 'ParametrosProgramacion/GetVariacion';
    return this.http.get(url);
  }
  ListarClavePreparacion(): any {
    const url = URL_FCAPROGAPI002 + 'SecuenciaCorrugadora/GetClavePreparacion';
    return this.http.get(url);
  }
  ListaAlmacen(): any {
    const url = URL_FCAPROGAPI002 + 'SecuenciaCorrugadora/GetAlmacen';
    return this.http.get(url);
  }

}
