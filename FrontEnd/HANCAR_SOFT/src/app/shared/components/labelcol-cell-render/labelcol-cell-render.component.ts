import { Component, OnInit } from '@angular/core';
import { ICellRendererAngularComp } from 'ag-grid-angular';

@Component({
  // tslint:disable-next-line: component-selector
  selector: 'labelcol-cell-render',
  templateUrl: './labelcol-cell-render.component.html',
  styleUrls: ['./labelcol-cell-render.component.css']
})
export class LabelcolCellRenderComponent implements ICellRendererAngularComp {

  params;
  class: string;
  dato: string;
  data: any;
  display: any;

  agInit(params): void {
    this.params = params;
    this.dato = this.params.value;
    this.data = params.data;
    this.display = params.display;
    this.displayHandler();
    // this.cellrenderDinamico(this.params);
  }
  refresh(params?: any): boolean {
    return true;
  }
  displayHandler(): void {
    if (typeof this.display === 'undefined') {
      this.class = null;
    } else {
        this.class = this.display(this.data, this.params.column.colId) || null;
    }
  }

}
