import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
  })
  export class CatFacturacionService {
  
    constructor(private http: HttpClient) { }

    URL_API_FACTURACION = environment.URL_API_ADMFCOAPI001 + 'CatalogosPreFactura/';

    buscarUsosCFDI(): any {
        return this.http.get(this.URL_API_FACTURACION + 'GET_USOS_CFDI');
      }
    buscarMonedas(): any {
        return this.http.get(this.URL_API_FACTURACION + 'GET_MONEDAS');
      }
    buscarIva(): any {
        return this.http.get(this.URL_API_FACTURACION + 'GET_IVAS');
    }
    buscarFormasPago(): any {
      return this.http.get(this.URL_API_FACTURACION + 'GET_FORMAS_PAGO');
    }
    buscarMetodoPago(): any{
      return this.http.get(this.URL_API_FACTURACION + 'GET_METODOS_PAGO');
    }
  }
