using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    // Creo: Manuel Valenzuela, 27/ene/2022
    // Objetivo: cargar datos en CMO034MF, módulo migrado a angular ahora se llama FCAPROG010MW.

    // clase campos de tabla de bd
    public class FCAPROGDAT023Entity
    {
        // propiedades
        public string claveMaquina { get; set; }
        public int turnos { get; set; }
        public int m2CargaPendiente { get; set; }
        public int m2PromTurno { get; set; }
        //public DateTime Fecha { get; set; }
        

    }

    // clase parametros de SP
    public class FCAPROGDAT023ParametrosEntity
    {
        // constructor
        public FCAPROGDAT023ParametrosEntity
        (
            int pAccion = 0, 
            string pMaquina1 = null,
            string pMaquina2 = null,
            int pTurnos = 0,
            int pValor = 0,
            int pProm = 0,
            string pUsuarioERP = null,
            int pStartRow = 0,
            int pEndRow = 0
        )
        {
            Accion = pAccion;
            Maquina1 = pMaquina1;
            Maquina2 = pMaquina2;
            Turnos = pTurnos;
            Valor = pValor;
            Prom = pProm;
            UsuarioERP = pUsuarioERP;
            startRow = pStartRow;
            endRow = pEndRow;
            
        }

        // propiedades
        public int Accion { get; set; }
        public string Maquina1 { get; set; }
        public string Maquina2 { get; set; }
        public int Turnos { get; set; }
        public int Valor { get; set; }
        public int Prom { get; set; }
        public string UsuarioERP { get; set; }
        public int startRow { get; set; }
        public int endRow { get; set; }
    }
}
