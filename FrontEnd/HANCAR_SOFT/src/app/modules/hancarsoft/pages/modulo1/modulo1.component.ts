import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';

interface camposGenerales {
  pFecha: string;
}

@Component({
  selector: 'app-modulo1',
  templateUrl: './modulo1.component.html',
  styleUrls: ['./modulo1.component.css']
})
export class Modulo1Component implements OnInit {
  camposGenerales: camposGenerales = {
    pFecha: this.pipe.transform(new Date(), 'yyyy-MM-dd')
  };
  columnasGrid1: any;
  datosGrid1 = [];

  constructor(public pipe: DatePipe) {
    this.columnasGrid1 = [
      {
        headerName: 'Campo 1',
        field: 'c1',
        flex: 1,
        minWidth: 95,
        headerClass: 'header-center header-grid-left',
        cellClass: 'grid-cell-center'
      },
      {
        headerName: 'Campo 2',
        field: 'c2',
        flex: 1,
        minWidth: 95,
        headerClass: 'header-center header-grid',
        cellClass: 'grid-cell-center'
      },
      {
        headerName: 'Campo 3',
        field: 'c3',
        flex: 1,
        minWidth: 95,
        headerClass: 'header-center header-grid-right',
        cellClass: 'grid-cell-center'
      },
    ];
  }

  ngOnInit(): void {
    this.datosGrid1.push({c1: 'c1', c2: 'c2', c3: 'c3'});
  }

}
