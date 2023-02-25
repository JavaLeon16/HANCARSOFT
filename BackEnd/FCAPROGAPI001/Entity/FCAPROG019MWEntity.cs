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

        public bool Proceso1 { get; set; }
        public string SuajeOld { get; set; }
        public string NotasOperacion { get; set; }
        public double EficienciaAct { get; set; }
        public double Eficiencia { get; set; }
        public bool SinPreparacion { get; set; }
        public double CanTinta1 { get; set; }
        public double CanTinta2 { get; set; }
        public double CanTinta3 { get; set; }
        public double CanTinta4 { get; set; }
        public string Supervisor { get; set; }
        public string Proceso { get; set; }
        public int MinPrep { get; set; }
        public double Velocidad { get; set; }
        public int MinStdProd { get; set; }
        public string MaqPA { get; set; }
        public int DespPA { get; set; }
        public string Suaje { get; set; }
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

    public class comboSuaje
    {
        public string CodigoGrabadoSuaje { get; set; }
        public string Descripcion { get; set; }
        public string Compuesto { get; set; }
    }

    public class comboGrabado
    {
        public string CodigoGrabadoSuaje { get; set; }
        public string Descripcion { get; set; }
        public string Compuesto { get; set; }
    }

    public class comboParafina
    {
        public int IdParafina { get; set; }
        public string Parafina { get; set; }
    }

    public class comboMaquinas
    {
        public string ClaveMaquina { get; set; }
        public string Nombre { get; set; }
    }

    public class programas
    {
        public string Fecha { get; set; }
        public string Supervisor { get; set; }
        public int IdTripulacion { get; set; }
        public int IdUnico { get; set; }
    }

    public class programasSeleccionadosL
    {
        public List<programas> programasSeleccionados { get; set; }
        public programasSeleccionadosL()
        {
            programasSeleccionados = new List<programas>();
        }
    }

    // MODULO 2
    public class clavesProcesos
    {
        public string ClaveProceso { get; set; }
        public string Descripcion { get; set; }
        public int TiempoStd { get; set; }
        public string Compuesto { get; set; }
    }
    public class desperdiciosCapturados
    {
        public string Programa { get; set; }
        public string ClaveMaquinaDesp { get; set; }
        public string Turno { get; set; }
        public bool EsUtilizado { get; set; }
        public int TotalDesperdicio { get; set; }
        public bool EsProcesoAnterior { get; set; }
        public bool EsContabilizadoPlc { get; set; }
    }
    public class conceptosDespMod2
    {
        public int IdConcepto { get; set; }
        public string Concepto { get; set; }
        public int Cantidad { get; set; }
        public bool EsUtilizado { get; set; }
        public string OP { get; set; }
        public string ClaveMaquinaDesp { get; set; }
        public int Programa { get; set; }
        public int Turno { get; set; }
        public string ClaveMaquinaCap { get; set; }
        public string MaquinaConcepto { get; set; }
    }
    public class camposFrmDesp
    {
        public int DespId { get; set; }
        public string Filtro { get; set; }
        public int Programa { get; set; }
        public string OP { get; set; }
        public string ClaveMaquina { get; set; }
        public string MaquinaDesperdicio { get; set; }
        public bool CapturaDesperdicio { get; set; }
        public bool AplicaCajaRec { get; set; }
        public bool EsUtilizado { get; set; }
        public bool EsContabilizadoPLC { get; set; }
        public bool EsProcesoAnterior { get; set; }
        public int TotalCapturado { get; set; }
        public int DespTurno { get; set; }

        public List<conceptosDespMod2> gridDatos;
        camposFrmDesp()
        {
            gridDatos = new List<conceptosDespMod2>();
        }
    }

    public class validaDatosSupervisor
    {
        public string ClaveSupervisor { get; set; }
        public string NombreSupervisor { get; set; }
        public int Turno { get; set; }
        public string ClaveMaquina { get; set; }
        public string Fecha { get; set; }
        public int Programa { get; set; }
    }

    public class camposGuardado
    {
        public bool IgnoraTiempo { get; set; }
        public bool ChkMP { get; set; }
        public int IdUnico { get; set; }
        public string wFechaAnterior { get; set; }
        public bool ChkProceso { get; set; } // ULTIMO PROCESO
        public string LSupervisor { get; set; } // CLAVE SUPERVISOR
        public string LSuaje { get; set; }
        public string LGrabado { get; set; }
        public int LMinutos { get; set; }
        public string LProceso { get; set; } // CLAVE PREPARACION
        public int LVelocidad { get; set; }
        public int LEficiencia { get; set; }
        public int LMinStd { get; set; }
        public int Programa { get; set; }
        public int MinStdProd { get; set; }
        public int TxtDespCorrUtil { get; set; } // UTILIZADO PA1
        public int TxtDespImprUtil { get; set; } // CONTABILIZADO PA
        public double TxtPesoLamina { get; set; }
        public double TxtPesoCaja { get; set; }
        public double TxtRetrabajo { get; set; }
        public string CmbMaquinaPA { get; set; } // MAQUINA PA2
        public int TxtDesPAUtul { get; set; } // UTILIZADO PA2
        public int Cant1 { get; set; }
        public int Cant2 { get; set; }
        public int Cant3 { get; set; }
        public int Cant4 { get; set; }
        public int TxtCantidad { get; set; }
        public int TxtCantidadCajasRec { get; set; }
        public string FecProduccion { get; set; }
        public string HoraIni { get; set; }
        public string HoraFin { get; set; }
        public int Turno { get; set; }
        public int IdTripulacion { get; set; }
        public string CmbMaquina { get; set; }
    }
}
