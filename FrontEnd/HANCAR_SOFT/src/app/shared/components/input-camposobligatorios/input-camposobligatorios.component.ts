import { Component, OnInit, Input, Output, EventEmitter} from '@angular/core';

@Component({
  // tslint:disable-next-line: component-selector
  selector: 'input-camposobligatorios',
  templateUrl: './input-camposobligatorios.component.html',
  styleUrls: ['./input-camposobligatorios.component.css']
})
export class InputCamposobligatoriosComponent implements OnInit {
  @Input() Titulo = '';
  @Input() required = false;
  @Input() mayus = false;
  data: any;
  @Input() type = '';
  @Input() maxlength = '';
  @Input() min = '';
  @Input() style = '';
  //@Input() event: any = {};
  @Input() Disabled = false;
  @Input() mosLabel = true;
  @Output() TextChange: EventEmitter<any> = new EventEmitter<any>();
  @Output() DataChange: EventEmitter<any> = new EventEmitter<any>();
  @Output() keypress: EventEmitter<any> = new EventEmitter<any>();
  @Output() keyup: EventEmitter<any> = new EventEmitter<any>();
  @Input() get Data(): any {
    return this.data;
  }
  set Data(value: any) {
    this.data = value;
    this.DataChange.emit(this.data);
  }
  @Output() requiredChange: EventEmitter<boolean> = new EventEmitter<boolean>();
  constructor() { }

  ngOnInit(): void {
  }

  blur(e): void {
    if (e.target.value.length > 0) {
      this.requiredChange.emit(false);
    }
  }
  Event(e): any {
    let result: any;

      switch (e.type) {
        case 'keyup':
          this.keyup.emit(e);
          break;
        case 'keypress':
          this.keypress.emit(e);
          break;

        default:
          break;
      }
    return result;
  }
}
