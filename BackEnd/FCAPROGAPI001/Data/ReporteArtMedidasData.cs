using Dapper;
using Entity;
using Entity.DTO.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ReporteArtMedidasData
    {

        public async Task<Result> GetDataTipoIndustria(string strConexion)
        {
            Result result = new Result();

            try
            {
                SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                string SP = nombre.ReporteArtMedidas + "1";
                SP = "FCAPROG007RSSPC1";
              
                using (var con = new SqlConnection(strConexion))
                {
                    var results = await con.QueryMultipleAsync(SP, new { Accion = 1 }, commandType: CommandType.StoredProcedure);
                    result.data = await results.ReadAsync<TipoIndustriaEntity>();
                    result.Correcto = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<Result> GetDataArticulosMaster(string strConexion, FCAPROG002RWParametrosSPEntity modelo, string pZona)
        {
            Result result = new Result();

            try
            {
                // asigno los parametros del modelo que recibo
                FCAPROG002RWParametrosSPEntity model = new FCAPROG002RWParametrosSPEntity();
                model.Accion = 2;
                model.TipoDeConsulta = modelo.TipoDeConsulta;
                model.AnchoMin = modelo.AnchoMin;
                model.AnchoMax = modelo.AnchoMax;
                model.LargoMin = modelo.LargoMin;
                model.LargoMax = modelo.LargoMax;
                model.TipoIndustria = modelo.TipoIndustria;
                model.FechaIni = modelo.FechaIni;
                model.FechaFin = modelo.FechaFin;
                model.OrderBy = modelo.OrderBy;
                model.Zona = pZona;

                SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                string SP = nombre.ReporteArtMedidas + "1";
                SP = "FCAPROG007RSSPC1";

                using (var con = new SqlConnection(strConexion))
                {
                    var results = await con.QueryMultipleAsync(SP, model, commandType: CommandType.StoredProcedure);
                    result.data = await results.ReadAsync<FCAPROG002RWDataEntity>();
                    result.Correcto = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }

        /*
        public async Task<Result> InsertCargaMaquinas(TokenData datosToken, FCAPROGDAT023Entity modelo)
        {
            Result objResult = new Result();

            try
            {
                SPNombre nombre = new SPNombre(Enums.SpTipo.Actualiza);
                string SP = nombre.CapProdRepGerencial + "2";

                // Parametros para el SP
                FCAPROGDAT023ParametrosEntity model = new FCAPROGDAT023ParametrosEntity();
                model.Accion = 1;
                model.Maquina1 = modelo.claveMaquina;
                model.Maquina2 = "";
                model.Turnos = modelo.turnos;
                model.Valor = modelo.m2CargaPendiente;
                model.Prom = modelo.m2PromTurno;
                model.UsuarioERP = datosToken.Usuario;

                using (var con = new SqlConnection(datosToken.Conexion))
                {
                    await con.QueryFirstOrDefaultAsync(SP, model, commandType: CommandType.StoredProcedure);
                    objResult.Correcto = true;
                    return objResult;
                }
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                objResult.Mensaje = ex.Message;
                return objResult;
            }
        }
        */
    }
}
