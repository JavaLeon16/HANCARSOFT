using Data;
using Entity;
using Entity.DTO.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ReporteArtMedidasBusiness
    {


        public Task<Result> GetDataTipoIndustria(string strConexion)
        {
            return new ReporteArtMedidasData().GetDataTipoIndustria(strConexion);
        }

        public Task<Result> GetDataArticulosMaster(string strConexion, FCAPROG002RWParametrosSPEntity modelo, string pZona)
        {
            return new ReporteArtMedidasData().GetDataArticulosMaster(strConexion, modelo, pZona);
        }

        //public async Task<Result> InsertCargaMaquinas(TokenData datosToken, FCAPROGDAT100Entity modelo)
        //{
        //    try
        //    {
        //        return await new CapProdRepGerencialData().InsertCargaMaquinas(datosToken, modelo);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ArgumentException(ex.Message);
        //    }
        //}

    }
}
