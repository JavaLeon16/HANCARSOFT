import { DatePipe } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';

interface camposGenerales {
  pFechaDel: string;
  pFechaAl: string;
  pFechaProduccion: string;
}

@Component({
  selector: 'app-pagina-l',
  templateUrl: './pagina-l.component.html',
  styleUrls: ['./pagina-l.component.css']
})
export class PaginaLComponent implements OnInit {
  camposGenerales: camposGenerales = {
    pFechaDel: this.primeroDelMes(new Date()),
    pFechaAl: this.pipe.transform(new Date(), 'yyyy-MM-dd'),
    pFechaProduccion: this.pipe.transform(new Date(), 'yyyy-MM-dd')
  };
  @Input() tituloModulo: string = '';
  columnasGridProgramas: any;
  datosGridProgramas = [];

  constructor(public pipe: DatePipe) {
    this.columnasGridProgramas = [
      {
        headerName: '',
        field: 'sel',
        flex: 1,
        minWidth: 30,
        headerClass: 'header-center header-grid-left',
        cellRenderer: 'hybCellRenderer',
        cellClass: 'grid-cell-btn-center',
        cellRendererParams: {
          type: 'chk',
          // disabled: true
          // change: this.changeChkScorPrincipal.bind(this)
        }
      },
      {
        headerName: 'Programa',
        field: 'programa',
        flex: 4,
        minWidth: 100,
        headerClass: 'header-center header-grid-right',
        cellClass: 'grid-cell-center'
      },
    ];
  }

  ngOnInit(): void {
    this.datosGridProgramas.push({sel: false, programa: 'Programa 1'});
    this.datosGridProgramas.push({sel: false, programa: 'Programa 2'});
    this.datosGridProgramas.push({sel: false, programa: 'Programa 3'});
    this.datosGridProgramas.push({sel: false, programa: 'Programa 4'});
  }

  primeroDelMes(fecha: Date): string {
    var año = fecha.getUTCFullYear().toString();
    var mes = (fecha.getMonth() + 1).toString();
    return año + '-' + (mes.length === 1 ? '0' + mes : mes) + '-01';
  }

}
