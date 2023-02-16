import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ActualizacionVariableService {

  public pPrograma$ = new BehaviorSubject<number>(0);
  constructor() { }
}
