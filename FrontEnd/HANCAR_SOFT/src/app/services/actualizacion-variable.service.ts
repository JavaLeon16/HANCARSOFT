import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { camposFrmDesp } from '../models/DTO/fcaprog019mw';

@Injectable({
  providedIn: 'root'
})
export class ActualizacionVariableService {

  public pPrograma$ = new BehaviorSubject<number>(0);
  public pDatosModalDesperdicio$ = new BehaviorSubject<camposFrmDesp>(undefined);
  constructor() { }
}
