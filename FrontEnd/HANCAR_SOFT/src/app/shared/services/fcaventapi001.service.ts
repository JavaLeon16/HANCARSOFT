import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';

const URL_FCAVENTAPI001 = environment.FCAVENTAPI001;

@Injectable({
  providedIn: 'root',
})
export class fcaventapi001Service {
  constructor(public http: HttpClient) {}

  ListarUMFacturacion(): any {
    const url = URL_FCAVENTAPI001 + 'UnidadMedidaFacturacion/getUMFacturacion';
    return this.http.get(url);
  }
  ListarRepresentantes(zona): any {
    const url = URL_FCAVENTAPI001 + 'Representantes/getListaRepresentantes';
    const params = new HttpParams().append('zona', zona);
    return this.http.get(url, { params });
  }
  ListarTipoRemisionado(): any {
    const url = URL_FCAVENTAPI001 + 'TipoRemisionado/getTipoRemisionado';
    return this.http.get(url);
  }
  ListarAlmacen(): any {
    const url = URL_FCAVENTAPI001 + 'Almacen/getAlmacen';
    return this.http.get(url);
  }
  ListarAlmacenStock(): any {
    const url = URL_FCAVENTAPI001 + 'Almacen/getAlmacenStock';
    return this.http.get(url);
  }
  ListarDiasInventario(): any {
    const url = URL_FCAVENTAPI001 + 'DiasInventario/getDiasInventario';
    return this.http.get(url);
  }
  ListarVariacion(): any {
    const url = URL_FCAVENTAPI001 + 'Variacion/getVariacion';
    return this.http.get(url);
  }
  ListarTipoVariacion(): any {
    const url = URL_FCAVENTAPI001 + 'TipoVariacion/getTipoVariacion';
    return this.http.get(url);
  }
  ListarClasificacion(): any {
    const url = URL_FCAVENTAPI001 + 'Clasificacion/getClasificacion';
    return this.http.get(url);
  }
  ListarTipoDato(): any {
    const url = URL_FCAVENTAPI001 + 'TipoDato/getTipoDato';
    return this.http.get(url);
  }
  ListarUnidadMedida(): any {
    const url = URL_FCAVENTAPI001 + 'UnidadMedida/getUnidadMedida';
    return this.http.get(url);
  }
  ListarTipoProducto(): any {
    const url = URL_FCAVENTAPI001 + 'TipoProducto/getTipoProducto';
    return this.http.get(url);
  }
  ListaSubcodigo(): any {
    const url = URL_FCAVENTAPI001 + 'Subcodigo/getListarSubcodigo';
    return this.http.get(url);
  }
  ListaResistencias(): any {
    const url = URL_FCAVENTAPI001 + 'Resistencias/ListarResistencia';
    return this.http.get(url);
  }
  ListaTipoCaja(): any {
    const url = URL_FCAVENTAPI001 + 'Articulos/getTipoCaja';
    return this.http.get(url);
  }
  ListaTipoGrabado(): any {
    const url = URL_FCAVENTAPI001 + 'Articulos/getTipoGrabado';
    return this.http.get(url);
  }
  ListaPresentacion(): any {
    const url = URL_FCAVENTAPI001 + 'Articulos/getPresentacion';
    return this.http.get(url);
  }
  ListaProcesos(): any {
    const url = URL_FCAVENTAPI001 + 'Articulos/getProcesos';
    return this.http.get(url);
  }
  ListaFlautas(): any {
    const url = URL_FCAVENTAPI001 + 'Articulos/getFlautas';
    return this.http.get(url);
  }
  ListaTipoUnion(): any {
    const url = URL_FCAVENTAPI001 + 'Articulos/getTipoUnion';
    return this.http.get(url);
  }
  ListaColor(): any {
    const url = URL_FCAVENTAPI001 + 'Articulos/getColor';
    return this.http.get(url);
  }
  ListaTipoPaletizado(): any {
    const url = URL_FCAVENTAPI001 + 'Articulos/getTipoPaletizado';
    return this.http.get(url);
  }
  ListaLlenadoPallet(): any {
    const url = URL_FCAVENTAPI001 + 'Articulos/getLlenadoPallet';
    return this.http.get(url);
  }
  ListaTipoPallet(): any {
    const url = URL_FCAVENTAPI001 + 'Articulos/getTipoPallet';
    return this.http.get(url);
  }
  ListaTinta(): any {
    const url = URL_FCAVENTAPI001 + 'Articulos/getTintas';
    return this.http.get(url);
  }
  ListaConfigProceso(): any {
    const url = URL_FCAVENTAPI001 + 'Articulos/getConfigProceso';
    return this.http.get(url);
  }
  ListaProveedor(par: any, fil: any): any {
    const url = URL_FCAVENTAPI001 + 'Articulos/getProveedor';
    const params = new HttpParams()
      .append('startRow', par.startRow)
      .append('endRow', par.endRow)
      .append('filtro', fil.filtro);
    return this.http.get(url, { params });
  }
  ListaAcomodo(): any {
    const url = URL_FCAVENTAPI001 + 'Articulos/getAcomodo';
    return this.http.get(url);
  }
  ListarMaestraNo(): any {
    const url = URL_FCAVENTAPI001 + 'Articulos/getMaestraNo';
    return this.http.get(url);
  }
  ListaEstatusSeguimiento(): any {
    const url = URL_FCAVENTAPI001 + 'SeguimientoSolicitud/getEstatusSeguimiento';
    return this.http.get(url);
  }
  ListaResponsable(par: any, fil: any): any {
    const url = URL_FCAVENTAPI001 + 'SeguimientoSolicitud/getResponsable';
    const params = new HttpParams()
    .append('startRow', par.startRow)
    .append('endRow', par.endRow)
    .append('Usuario', fil.Usuario)
    .append('filtro', fil.filtro)
    .append('Zona', fil.Zona);
    return this.http.get(url, { params });
  }

}
