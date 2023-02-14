import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';

const URL_AMDCCOBAPI001 = environment.AMDCCOBAPI001 + 'TipoCredito/';

@Injectable({
  providedIn: 'root',
})
export class amdccobapi001Service {
  constructor(public http: HttpClient) {}

  ListarTipoCredito(): any {
    const url = URL_AMDCCOBAPI001 + 'GetTipoCredito';
    return this.http.get(url);
  }
}
