using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class clsHorario
    {
        public string clavemaquina { get; set; }

        public DateTime inicioprimero { get; set; }
        public DateTime finprimero { get; set; }
        public bool activoprimero { get; set; }

        public DateTime iniciosegundo { get; set; }
        public DateTime finsegundo { get; set; }
        public bool activosegundo { get; set; }

        public DateTime iniciotercero { get; set; }
        public DateTime fintercero { get; set; }
        public bool activotercero { get; set; }

        public DateTime inicioprimerosabado { get; set; }
        public DateTime finprimerosabado { get; set; }
        public bool activoprimerosabado { get; set; }
        public bool activoprimerodomingo { get; set; }

        public DateTime iniciosegundosabado { get; set; }
        public DateTime finsegundosabado { get; set; }
        public bool activosegundosabado { get; set; }
        public bool activosegundodomingo { get; set; }

        public DateTime iniciotercerosabado { get; set; }
        public DateTime fintercerosabado { get; set; }
        public bool activotercerosabado { get; set; }
        public bool activotercerdomingo { get; set; }

        public string usuario { get; set; }        
        //public string fecha { get; set; }




    }
}
