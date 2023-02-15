using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{

    /// <summary>
    /// Clase de entidades para cargar combo y llenar modelo de consumo de rollos
    /// Author: Manuel Valenzuela, 26/04/2022
    /// </summary>
    public class FCAPROG001RWEntity
    {
    }

    public class FCAPROG001RWClaseEntity
    {
        public string CL { get; set; }
    }
    public class FCAPROG001RWSubClaseEntity
    {
        public string SCL { get; set; }
    }
    public class FCAPROG001RWTripulacionEntity
    {
        public int id { get; set; }
        public string valor { get; set; }
    }
    public class FCAPROG001RWParametrosEntity
    {
        public string FechaIni { get; set; }
        public string FechaFin { get; set; }
        public string OpcionTipoRolloSeleccionado { get; set; }
        public int chkTodosTurnos { get; set; }
        public int? Turno { get; set; }
        public string CodigoRollo { get; set; }
        public string Clase { get; set; }
        public string SubClase { get; set; }
        public float? Ancho { get; set; }
        public float? PesoInicial { get; set; }
        public float? PesoFinal { get; set; }
        public float? RadioFinal { get; set; }
        public string TipoOrdenacion { get; set; }
        public int ChkOps { get; set; }
        public int? Accion { get; set; }
        public string ZonaERP { get; set; }

    }

    public class FCAPROG001RWDatosConsumoRollosEntity
    {
        public string HI { get; set; }
        public string HF { get; set; }
        public string Nrollo { get; set; }
        public string Tipo { get; set; }
        public string HoraConsumo { get; set; }
        public double ancho { get; set; }
        public double PesoInicial { get; set; }
        public DateTime Entrada { get; set; }
        public DateTime Salida { get; set; }
        public double consumo { get; set; }
        public double Resto { get; set; }
        public double RadioFinal { get; set; }
        public string Descripcion { get; set; }
        public int Programa { get; set; }
        public string OP { get; set; }
        public double consumoOP { get; set; }
        public string fecha { get; set; }
        public double Gramaje { get; set; }
        public string documento { get; set; }
        public int cveconsumo { get; set; }
        public byte STATUS { get; set; }
        public string OC { get; set; }
        public string uso { get; set; }
        public double ConsumoTotal { get; set; }

    }
}
