using Dapper;
using Entity;
using Entity.DTO.Common;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Data
{
    public class FlautasData
    {
        public async Task<Result> GetFlautas(string strConexion, int startRow, int endRow, string filtro)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(strConexion))
                {
                    var result = await con.QueryMultipleAsync(
                       "FCAPROGCAT007CWSPC1",
                        new
                        {
                            Opcion = 1,
                            startRow,
                            endRow,
                            filtro = string.IsNullOrEmpty(filtro) ? "" : filtro
                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.Correcto = true;
                    objResult.data = await result.ReadAsync<FlautasEntity>();
                    objResult.totalRecords = await result.ReadFirstAsync<int>();
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Result> GetCorrugados(string strConexion)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(strConexion))
                {
                    var result = await con.QueryMultipleAsync(
                       "FCAPROGCAT007CWSPC1",
                        new
                        {
                            Opcion = 2,
                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.Correcto = true;
                    objResult.data = await result.ReadAsync<CorrugadosEntity>();
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<Result> GuardarFlautas(TokenData datosToken, FlautasEntity obj)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(datosToken.Conexion))
                {
                    //SPNombre nombre = new SPNombre(Enums.SpTipo.Actualiza);
                    TranformaDataTable Ds = new TranformaDataTable();
                    var result = await con.QueryFirstOrDefaultAsync(
                        "FCAPROGCAT007CWSPA2",
                        new
                        {
                            flauta = obj.flauta,
                            factor1 = obj.factor1,
                            factor2 = obj.factor2,
                            factor3 = obj.factor3,
                            pegamento =  obj.pegamento,
                            corrugado = obj.corrugado,
                            laminasmt = obj.laminasmt,
                            factormts = obj.factormts,
                            piezaspulgadas = obj.piezaspulgadas,
                            Usuario = datosToken.Usuario,
                            opcion = 1

                        },
                        commandType: CommandType.StoredProcedure);
                    objResult.Correcto = true;
                    objResult.data = result;
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
        public async Task<Result> ModificarFlautas(TokenData datosToken, FlautasEntity obj)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(datosToken.Conexion))
                {
                    //SPNombre nombre = new SPNombre(Enums.SpTipo.Actualiza);
                    TranformaDataTable Ds = new TranformaDataTable();
                    await con.QueryFirstOrDefaultAsync(
                        "FCAPROGCAT007CWSPA2",
                        new
                        {
                            obj.flauta,
                            obj.factor1,
                            obj.factor2,
                            obj.factor3,
                            obj.pegamento,
                            obj.corrugado,
                            obj.laminasmt,
                            obj.factormts,
                            obj.piezaspulgadas,
                            datosToken.Usuario,
                            opcion = 2

                        },
                        commandType: CommandType.StoredProcedure);
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
        public async Task<Result> EliminarFlautas(TokenData datosToken, FlautasEntity obj)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(datosToken.Conexion))
                {
                    //SPNombre nombre = new SPNombre(Enums.SpTipo.Actualiza);
                    TranformaDataTable Ds = new TranformaDataTable();
                    await con.QueryFirstOrDefaultAsync(
                        "FCAPROGCAT007CWSPA2",
                        new
                        {
                            obj.flauta,
                            datosToken.Usuario,
                            opcion = 3

                        },
                        commandType: CommandType.StoredProcedure);
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
    }
}
