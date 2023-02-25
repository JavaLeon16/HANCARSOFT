using Data;
using Entity;
using Entity.DTO.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class FCAPROG019MWBusiness
    {
        public Task<Result> leeHoraLocal(TokenData DatosToken)
        {
            return new FCAPROG019MWData().leeHoraLocal(DatosToken);
        }
        public Task<Result> buscaPrograma(TokenData DatosToken, string programa)
        {
            return new FCAPROG019MWData().buscaPrograma(DatosToken, programa);
        }
        public Task<Result> buscaProduccion(TokenData DatosToken, string programa)
        {
            return new FCAPROG019MWData().buscaProduccion(DatosToken, programa);
        }
        public Task<Result> buscaTripulacionMaquina(TokenData DatosToken, string claveMaquina)
        {
            return new FCAPROG019MWData().buscaTripulacionMaquina(DatosToken, claveMaquina);
        }
        public Task<Result> leePrograma(TokenData DatosToken, string fechaAnterior, string programa, string claveMaquina, string turno)
        {
            return new FCAPROG019MWData().leePrograma(DatosToken, fechaAnterior, programa, claveMaquina, turno);
        }
        public Task<Result> buscaSupervisor(TokenData DatosToken)
        {
            return new FCAPROG019MWData().buscaSupervisor(DatosToken);
        }
        public Task<Result> buscaParafina(TokenData DatosToken)
        {
            return new FCAPROG019MWData().buscaParafina(DatosToken);
        }
        public Task<Result> validarGuardado(TokenData DatosToken, string programa, string claveMaquina, string turno, string fecha)
        {
            return new FCAPROG019MWData().validarGuardado(DatosToken, programa, claveMaquina, turno, fecha);
        }
        public Task<Result> guardar(TokenData DatosToken,
            string fecha, string horaIni, string horaFin, string turno, string supervisor, string minutos, string despCorrguradora,
            string despImpresora, string despAcabados, string fechaNow, string parafina, string pesoLamina, string pesoCaja, string retrabajo,
            string actCantidad, string idTripulacion, string programa, string claveMaquina, string wFechaAnterior, string idUnico
        )
        {
            return new FCAPROG019MWData().guardar(DatosToken,
                fecha, horaIni, horaFin, turno, supervisor, minutos, despCorrguradora,
                despImpresora, despAcabados, fechaNow, parafina, pesoLamina, pesoCaja, retrabajo,
                actCantidad, idTripulacion, programa, claveMaquina, wFechaAnterior, idUnico
            );
        }

        // METODOS DE PAGINA L
        public Task<Result> cargaComboMaquinas(TokenData DatosToken)
        {
            return new FCAPROG019MWData().cargaComboMaquinas(DatosToken);
        }
        public Task<Result> buscaProgramas(TokenData DatosToken, string fecha, string fechaF, string turno, string claveMaquina, string sinFechaProd)
        {
            return new FCAPROG019MWData().buscaProgramas(DatosToken, fecha, fechaF, turno, claveMaquina, sinFechaProd);
        }
        public async Task<Result> actualizaSupTrip(TokenData DatosToken, programasSeleccionadosL datos)
        {
            try
            {
                return await new FCAPROG019MWData().actualizaSupTrip(DatosToken, datos);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }


        // =========================================================================================================================================
        // MODULO 2

        public Task<Result> buscaProgramaMod2(TokenData DatosToken, string programa)
        {
            return new FCAPROG019MWData().buscaProgramaMod2(DatosToken, programa);
        }
        public Task<Result> getClavesPreparacion(TokenData DatosToken, string claveMaquina)
        {
            return new FCAPROG019MWData().getClavesPreparacion(DatosToken, claveMaquina);
        }
        public Task<Result> obtenerDesperdicios(TokenData DatosToken, string op, string programa, string turno, string tipoConsulta)
        {
            return new FCAPROG019MWData().obtenerDesperdicios(DatosToken, op, programa, turno, tipoConsulta);
        }
        public Task<Result> cargaSupsMod2(TokenData DatosToken, string programa, string turno)
        {
            return new FCAPROG019MWData().cargaSupsMod2(DatosToken, programa, turno);
        }
        public Task<Result> cargaSuajeMod2(TokenData DatosToken, string suaje)
        {
            return new FCAPROG019MWData().cargaSuajeMod2(DatosToken, suaje);
        }
        public Task<Result> cargaGrabadosMod2(TokenData DatosToken, string articulo)
        {
            return new FCAPROG019MWData().cargaGrabadosMod2(DatosToken, articulo);
        }
        public Task<Result> cargaMaqProcAntMod2(TokenData DatosToken)
        {
            return new FCAPROG019MWData().cargaMaqProcAntMod2(DatosToken);
        }
        public Task<Result> cargaCantidadRecMod2(TokenData DatosToken, string op, string claveMaquina, string programa)
        {
            return new FCAPROG019MWData().cargaCantidadRecMod2(DatosToken, op, claveMaquina, programa);
        }
        public Task<Result> cargaConceptosDesp(
            TokenData DatosToken, string maquinaDesperdicio, string op, string programa, string claveMaquina, string turno,
            string aplicaCajaRec, string esUtilizado, string esContabilizadoPLC, string esProcesoAnterior
        )
        {
            return new FCAPROG019MWData().cargaConceptosDesp(
                DatosToken, maquinaDesperdicio, op, programa, claveMaquina, turno,
                aplicaCajaRec, esUtilizado, esContabilizadoPLC, esProcesoAnterior
            );
        }
        public Task<Result> guardarDespMod2(TokenData DatosToken, camposFrmDesp datos)
        {
            return new FCAPROG019MWData().guardarDespMod2(DatosToken, datos);
        }
        public Task<Result> validaDatosSupervisorMod2(TokenData DatosToken, string claveMaquina, string turno, string fecha, string claveSup, string programa)
        {
            return new FCAPROG019MWData().validaDatosSupervisorMod2(DatosToken, claveMaquina, turno, fecha, claveSup, programa);
        }
        public Task<Result> gardarDatosMod2(TokenData DatosToken, camposGuardado datos)
        {
            return new FCAPROG019MWData().gardarDatosMod2(DatosToken, datos);
        }
    }
}
