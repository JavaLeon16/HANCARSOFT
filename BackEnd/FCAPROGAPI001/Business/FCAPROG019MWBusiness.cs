using Data;
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
    }
}
