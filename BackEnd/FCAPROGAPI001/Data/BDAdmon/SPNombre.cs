using Data.BDAdmon;
using Data.Enums;

namespace Data
{
    public class SPNombre
    {
        private static string TipoAccionNombre;

        public static string SubProProg;
        public static string SubProProd;

        public string TipoCaja;
        public string Proceso;
        public string Maquina;
        public string Combinaciones;
        public string Desperdicios;
        public string Desperdicio;
        public string AreaDesperdicios;
        public string Paros;
        public string CapProdRepGerencial;
        public string ReporteArtMedidas;


        public SPNombre(SpTipo TipoAccion = SpTipo.Consulta)
        {
            TipoAccionNombre = TipoAccion == SpTipo.Actualiza ? "SPA" : "SPC";

            SubProProg = ProcesosCecso.FcaCajas + SubProcesos.FcaProgramacion;
            SubProProd = ProcesosCecso.FcaCajas + SubProcesos.FcaProduccion;

            TipoCaja = SubProProg + TablaTipo.Catalogo + "010CW" + TipoAccionNombre;
            Proceso = SubProProg + TablaTipo.Catalogo + "009CW" + TipoAccionNombre;
            Maquina = SubProProg + TablaTipo.Catalogo + "008CW" + TipoAccionNombre;
            Combinaciones = SubProProg + TablaTipo.Catalogo + "004CW" + TipoAccionNombre;
            Desperdicios = SubProProd + TablaTipo.Catalogo + "011" + TipoAccionNombre;
            Paros = SubProProd + TablaTipo.Catalogo + "012CW" + TipoAccionNombre;
            Desperdicio = SubProProd + TablaTipo.Catalogo + "016" + TipoAccionNombre;
            AreaDesperdicios = SubProProd + TablaTipo.Catalogo + "014CW" + TipoAccionNombre;

            // Manuel Valenzuela, 28/02/2022, armo mi SP para CapProdRepGerencial
            CapProdRepGerencial = SubProProg + "010MW" + TipoAccionNombre;
            ReporteArtMedidas = SubProProg + "010MW" + TipoAccionNombre;
            //CapProdRepGerencial = SubProProg + TablaTipo.Datos + "023" + TipoAccionNombre;
        }
    }
}

