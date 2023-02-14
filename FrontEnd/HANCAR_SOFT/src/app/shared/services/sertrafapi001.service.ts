import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';

const URL_SERTRAFAPI001 = environment.SERTRAFAPI001 + 'Direccion/';

@Injectable({
  providedIn: 'root',
})
export class sertrafapi001Service {
  constructor(public http: HttpClient) {}

  ListarPais(): any {
    const url = URL_SERTRAFAPI001 + 'GetPais';
    return this.http.get(url);
  }
  ListarEstado(clavePais: string): any {
    const url = URL_SERTRAFAPI001 + 'GetEstado';
    const params = new HttpParams().append('ClavePais', clavePais);
    return this.http.get(url, { params });
  }
  ListarCiudad(claveEstado: string): any {
    const url = URL_SERTRAFAPI001 + 'GetCiudad';
    const params = new HttpParams().append('ClaveEstado', claveEstado);
    return this.http.get(url, { params });
  }
}
