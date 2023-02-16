using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class FCAPROG019MWEntity
    {
        public string Fecha { get; set; }
    }

    public class datosBusquedaPrograma
    {
        public string ClaveMaquina {get; set; }
        public string Op {get; set; }
        public string ClaveProceso {get; set; }
        public double PiezasCorte {get; set; }
        public string TipoMaquina {get; set; }
        public int Cantidad {get; set; }
        public bool UltimoProceso {get; set; }
        public bool Pegado {get; set; }
        public string PrimerColor {get; set; }
        public string SegundoColor {get; set; }
        public string TercerColor {get; set; }
        public string CuartoColor {get; set; }
        public double AreaUnitaria {get; set; }
        public double PesoUnitario {get; set; }
        public string ClaveArticulo {get; set; }
        public string Articulo {get; set; }
        public bool LiberadoCostos {get; set; }
    }

    public class datosBusquedaProduccion
    {
        public string ClaveMaquina {get; set; }
        public string Op {get; set; }
        public string Fecha {get; set; }
        public string Turno {get; set; }
        public int Cantidad {get; set; }
        public string FechaSistema {get; set; }
        public int IdUnico {get; set; }
    }

    public class datosTripulacion
    {
        public int IdTripulacion { get; set; }
        public string Tripulacion { get; set; }
    }

    public class datosPrograma
    {
        public int Programa { get; set; }
        public string Turno { get; set; }
        public string Supervisor { get; set; }
        public string ClaveMaquina { get; set; }
        public string OP { get; set; }
        public string Fecha { get; set; }
        public string HoraInicio { get; set; }
        public string HoraTermino { get; set; }
        public int Cantidad { get; set; }
        public int PickUp { get; set; }
        public int MinutosProduccion { get; set; }
        public int DesperdicioAcabados { get; set; }
        public int LaminaDespeg { get; set; }
        public int LaminaComba { get; set; }
        public int LaminaMedidas { get; set; }
        public int LaminaImpres { get; set; }
        public int LaminaDimens { get; set; }
        public int LaminaPegad { get; set; }
        public string FechaSistema { get; set; }
        public string Usuario { get; set; }
        public int Excedente { get; set; }
        public bool ExpCostos { get; set; }
        public string FechaExp { get; set; }
        public int LaminasDesperdicio { get; set; }
        public int PiezasDesperdicio { get; set; }
        public double CanTinta1 { get; set; }
        public double CanTinta2 { get; set; }
        public double CanTinta3 { get; set; }
        public double CanTinta4 { get; set; }
        public string TipoParafina { get; set; }
        public bool SinPreparacion { get; set; }
        public bool VerificaAduana { get; set; }
        public int ProduccionPT { get; set; }
        public int MinutosFT { get; set; }
        public int ClaveInspector { get; set; }
        public double PesoLamina { get; set; }
        public double PesoCaja { get; set; }
        public int Retrabajo { get; set; }
        public string MaqPA { get; set; }
        public int DespPA { get; set; }
        public int IdTripulacion { get; set; }
        public int DespProdEnProc { get; set; }
        public string Motivo { get; set; }
        public bool PreAlimentador { get; set; }
        public string FechaInsert { get; set; }
        public int DespCorrNoUtil { get; set; }
        public int ProdProcesoCap { get; set; }
        public int DespPAUtil { get; set; }
        public int DespImpNoConPLC { get; set; }
        public int IdUnico { get; set; }
        public string ModuloInsert { get; set; }
        public int Posicion { get; set; }
        public int DesEtFront { get; set; }
        public int DesEtBack { get; set; }
        public int CajasComProPLC { get; set; }
        public bool ContabilizadoGolpesXSuaje { get; set; }
        public bool VerificaRuta { get; set; }
        public int MinutosComedor { get; set; }
        public int CajasRecEnProd { get; set; }
    }

    public class comboSupervisor
    {
        public string IdSupervisor { get; set; }
        public string Supervisor { get; set; }
    }

    public class comboParafina
    {
        public int IdParafina { get; set; }
        public string Parafina { get; set; }
    }

    public class comboMaquinas
    {
        public string ClaveMaquina { get; set; }
    }
}
