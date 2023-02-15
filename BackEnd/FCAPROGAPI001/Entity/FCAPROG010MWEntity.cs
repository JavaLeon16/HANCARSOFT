using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class FCAPROG010MWEntity
    {
        public int IdParametro {get;set;}
        public string Parametro { get; set; }
        public string Descripcion { get; set; }
        public string Valor { get; set; }

        public List<Variables> lstdatos { get; set; }
    }

    public class Variables
    {
        public int idParametro { get; set; }
        public string valor { get; set; }
    }
}
