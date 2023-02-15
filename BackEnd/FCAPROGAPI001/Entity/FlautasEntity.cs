using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class FlautasEntity
    {
        public string flauta { get; set; }
        public decimal factor1 { get; set; }
        public decimal factor2 { get; set; }
        public decimal factor3 { get; set; }
        public decimal pegamento { get; set; }
        public string corrugado { get; set; }
        public int laminasmt { get; set; }
        public decimal factormts { get; set; }
        public decimal piezaspulgadas { get; set; }

    }
    public class CorrugadosEntity
    {
        public string corrugado { get; set; }
    }
}
