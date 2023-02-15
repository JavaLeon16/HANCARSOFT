using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{

    // Manuel Valenzuela 05oct2022
    public class FCAPROG002RWEntity
    {
    }

    public class TipoIndustriaEntity
    {
        public string Clave { get; set; }
        public string Descripcion { get; set; }
    }

    public class FCAPROG002RWDataEntity
    {
        public string industria { get; set; }
        public string op { get; set; }
        public double ancho { get; set; }
        public double largo { get; set; }
        public string clavearticulo { get; set; }
        public DateTime fecha { get; set; }
        public string articulo { get; set; } // Descripcion
        public string resistencia { get; set; }
        public string flauta { get; set; }

    }

    public class FCAPROG002RWParametrosSPEntity
    {

        public int Accion { get; set; }
        public string TipoDeConsulta { get; set; }
        public double? AnchoMin { get; set; }
        public double? AnchoMax { get; set; }
        public double? LargoMin { get; set; }
        public double? LargoMax { get; set; }
        public string TipoIndustria { get; set; }
        public DateTime? FechaIni { get; set; }
        public DateTime? FechaFin { get; set; }
        public string OrderBy { get; set; }
        public string Zona { get; set; }
    }
}
