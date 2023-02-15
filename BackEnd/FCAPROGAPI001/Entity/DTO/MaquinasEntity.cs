using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.DTO
{
    public class Maquina
    {
        public string TipoMaquina { get; set; }
        public string ClaveMaquina { get; set; }
        public string Nombre { get; set; }
        public bool Troquela { get; set; }
        public int TintasMax { get; set; }
        public decimal AnchoMax { get; set; }
        public decimal AnchoMaxT { get; set; }
        public decimal AnchoMaxAlt { get; set; }
        public decimal AnchoMin { get; set; }
        public decimal LargoMax { get; set; }
        public decimal LargoMaxT { get; set; }
        public decimal LargoMin { get; set; }
        public decimal CostoMinuto { get; set; }
        public int CapacidadSemanal { get; set; }
        public decimal SalariosTurno { get; set; }
        public decimal Desp1000 { get; set; }
        public decimal Desp10000 { get; set; }
        public decimal DespMayor10000 { get; set; }
        public decimal M2Hora { get; set; }
        public bool NoAceptaPiezasxCorte { get; set; }
        public decimal Eficiencia { get; set; }
        public int Tproceso { get; set; }
        public bool Default { get; set; }
        public bool Imprime { get; set; }
        public int CodEvaluacion { get; set; }
        public int MinStd { get; set; }
        public int TurnosxDia { get; set; }
        public bool EvaluaMtto { get; set; }
        public decimal PorcenEvaluaMtto { get; set; }
        public string Evalua { get; set; }
        public bool ValMinStd { get; set; }
        public bool EvaluaVelocidad { get; set; }
        public int MaquinaPar { get; set; }
        public int PzaValStd { get; set; }
        public bool EsCapAut { get; set; }
        public int UbucacionPlcCP { get; set; }
        public bool PermiteMoficarProd { get; set; }
        public bool ImplementadoAut { get; set; }
        public int TipoConsulta { get; set; }
        public bool IsRecord { get; set; }
        public int LimiteReserva { get; set; }
        public bool UtilizaSuaje66 { get; set; }
        public bool UtilizaSuaje50 { get; set; }
        public bool EsCortadora { get; set; }
        public bool EsEtiquetadora { get; set; }
        public int idAreaCosto { get; set; }
    }

    public class ListadoMaquinas
    {
        public string ClaveMaquina { get; set; }
        public bool Troquela { get; set; }
        public string Proceso { get; set; }
        public bool Default { get; set; }


    }

    public class Desperdicios
    {
        public int ID { get; set; }
        public int RInicial { get; set; }
        public int RFinal { get; set; }
        public decimal Desperdicio { get; set; }
    }
    public class Procesos
    {
        public int TProceso { get; set; }
        public string Descripcion { get; set; }
    }
    public class Evaluaciones
    {
        public int CodEvaluacion { get; set; }
        public string Nombre { get; set; }
    }

    public class Tripulaciones
    {
        public int IdTripulacion { get; set; }
        public string Nombre { get; set; }
    }

    public class Puestos
    {
        public string NombreMaquina { get; set; }
        public string DescripcionPuestos { get; set; }
        public int PersonasRequeridas { get; set; }

    }

    public class guardaMaquina
    {
        public Maquina[] Maquinas {get;set;}
        public Desperdicios[] Desperdicios { get; set; }
        public Tripulaciones[] Tripulaciones { get; set; }
        public bool CambioEficiencia { get; set; }
        public bool CambioCodEvaluacion { get; set; }

    }
}
