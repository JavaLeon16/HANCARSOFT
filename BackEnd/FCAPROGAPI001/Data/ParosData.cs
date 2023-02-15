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
    public class ParosData
    {
        public async Task<Result> GetParos(string strConexion, int startRow, int endRow, string filtro, int Estatus)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(strConexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        nombre.Paros + "1",
                        new
                        {
                            Opcion = 1,
                            startRow,
                            endRow,
                            TipoMaquina = filtro,
                            Estatus = Estatus
                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.Correcto = true;
                    objResult.data = await result.ReadAsync<FCAPRODCAT012Entity>();
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
        public async Task<Result> Agregar(TokenData datosToken, FCAPRODCAT012Entity Paro)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(datosToken.Conexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Actualiza);
                    TranformaDataTable Ds = new TranformaDataTable();
                    await con.QueryFirstOrDefaultAsync(
                        nombre.Paros + "2",
                        new
                        {
                            Opcion = 1,
                            Usuario = datosToken.Usuario,
                            ClaveParo = Paro.ClaveParo,
                            TipoParo = Paro.TipoParo,
                            TipoParoValida = Paro.TipoParoValida,
                            TipoMaquina = Paro.TipoMaquina,
                            AplicaPareto = Paro.AplicaPareto,
                            DescuentoIndicador = Paro.DescuentoIndicador,
                            Observaciones = Paro.Observaciones,
                            Autoriza = Paro.Autoriza,
                            EsProgramado = Paro.EsProgramado

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
        public async Task<Result> Editar(TokenData datosToken, FCAPRODCAT012Entity Paro)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(datosToken.Conexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Actualiza);
                    TranformaDataTable Ds = new TranformaDataTable();
                    await con.QueryFirstOrDefaultAsync(
                        nombre.Paros + "2",
                        new
                        {
                            Opcion = 2,
                            Usuario = datosToken.Usuario,
                            ClaveParo = Paro.ClaveParo,
                            TipoParo = Paro.TipoParo,
                            TipoParoValida = Paro.TipoParoValida,
                            TipoMaquina = Paro.TipoMaquina,
                            AplicaPareto = Paro.AplicaPareto,
                            DescuentoIndicador = Paro.DescuentoIndicador,
                            Observaciones = Paro.Observaciones,
                            Autoriza = Paro.Autoriza,
                            EsProgramado = Paro.EsProgramado

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
        public async Task<Result> Eliminar(TokenData datosToken, FCAPRODCAT012Entity Paro)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(datosToken.Conexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Actualiza);
                    TranformaDataTable Ds = new TranformaDataTable();
                    await con.QueryFirstOrDefaultAsync(
                        nombre.Paros + "2",
                        new
                        {
                            Opcion = 3,
                            Usuario = datosToken.Usuario,
                            ClaveParo = Paro.ClaveParo

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
