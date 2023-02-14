import {
  Component,
  AfterViewInit,
  ChangeDetectorRef,
  ViewChild,
} from '@angular/core';
import { ICellRendererAngularComp } from 'ag-grid-angular';
import tippy, { hideAll } from 'tippy.js';

@Component({
  selector: 'app-popup-cell-renderer',
  templateUrl: './popup-cell-renderer.component.html',
  styleUrls: ['./popup-cell-renderer.component.css'],
})
export class PopupCellRendererComponent
  implements ICellRendererAngularComp, AfterViewInit
{
  params;
  label: string;
  type: string;
  class: string;
  class2: string;
  dato: string;
  value: any;
  isOpen = false;
  disabled = false;
  Tipo: 'B1' | 'B2' ;
  Activar2B = false;
  Activar1B = false;
  private tippyInstance;

  @ViewChild('content') container;
  @ViewChild('trigger') button;

  constructor(private changeDetector: ChangeDetectorRef) {}

  ngAfterViewInit(): void {
    this.tippyInstance = tippy(this.button.nativeElement);
    this.tippyInstance.disable();
  }
  agInit(params): void {
    this.params = params;
    this.label = null;
    this.type = 'button';
    this.class = '';
    this.dato = '';
    this.value = params.Opciones;
    this.disabled = params.disabled;
    this.Tipo = params.tipo;
    this.cellrenderDinamico(this.params);
  }
  refresh(params?: any): boolean {
    return true;
  }
  onClick($event: any): void {
    if (this.params.onClick instanceof Function) {
      const params = {
        event: $event,
        data: this.params.node.data,
      };
      this.params.onClick(params);
    }
  }
  onClickOpciones($event: any, onclick: any): void {
    this.togglePopup();
    if (onclick instanceof Function) {
      const params = {
        event: $event,
        data: this.params.node.data,
      };
      onclick(params);
    }
  }
  cellrenderDinamico(params): void {
    let Obj;
    switch (this.Tipo) {
      case 'B2':
        this.Activar2B = true;
        this.Activar1B = false;
        break;
      case 'B1':
        this.Activar2B = false;
        this.Activar1B = true;
        break;
      default:
        this.Activar2B = true;
        this.Activar1B = false;
        break;
    }
    if (params.data !== undefined) {
      Obj = params.data;
      const key = params.colDef.field;

      this.label = params.label || null;
      this.type = params.type || 'button';
      this.class = params.class || null;

      if (typeof params.colDef.field !== 'undefined') {
        if (params.colDef.field !== null) {
          this.dato = Obj[key];
        }
      }
    }
  }
  configureTippyInstance(): void {
    this.tippyInstance.enable();
    this.tippyInstance.show();

    this.tippyInstance.setProps({
      trigger: 'manual',
      placement: 'right',
      arrow: false,
      interactive: true,
      appendTo: document.body,
      hideOnClick: false,
      onShow: (instance) => {
        hideAll({ exclude: instance });
      },
      onClickOutside: (instance, event) => {
        this.isOpen = false;
        instance.unmount();
      },
    });
  }

  togglePopup(): void {
    this.isOpen = !this.isOpen;
    this.changeDetector.detectChanges();
    if (this.isOpen) {
      this.configureTippyInstance();
      this.tippyInstance.setContent(this.container.nativeElement);
    } else {
      this.tippyInstance.unmount();
    }
  }
}
