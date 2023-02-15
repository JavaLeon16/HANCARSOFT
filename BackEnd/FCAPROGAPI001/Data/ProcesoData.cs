using Dapper;
using Entity;
using Entity.DTO;
using Entity.DTO.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ProcesoData
    {
        public async Task<Result> GetProceso(string strConexion, int startRow, int endRow, string filtro, string TipoMaquina, int Estatus)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(strConexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        nombre.Proceso + "1",
                        new
                        {
                            Opcion = 1,
                            startRow,
                            endRow,
                            filtro = string.IsNullOrEmpty(filtro) ? "" : filtro,
                            TipoMaquina = TipoMaquina,
                            Estatus = Estatus
                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.Correcto = true;
                    objResult.data = await result.ReadAsync<FCAPROGCAT009Entity>();
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
        public async Task<Result> GetProcesoPorTipoMaquina(string strConexion, string TipoMaquina)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(strConexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        nombre.Proceso + "1",
                        new
                        {
                            Opcion = 2,
                            TipoMaquina = TipoMaquina

                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.Correcto = true;
                    objResult.data = await result.ReadAsync<FCAPROGCAT013Entity>();
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Result> GetTipoMaquina(string strConexion)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(strConexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        nombre.Proceso + "1",
                        new
                        {
                            Opcion = 3,

                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.Correcto = true;
                    objResult.data = await result.ReadAsync<TipoMaquinaDTO>();
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Result> Agregar(TokenData datosToken, FCAPROGCAT009Entity proceso)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(datosToken.Conexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Actualiza);
                    TranformaDataTable Ds = new TranformaDataTable();
                    await con.QueryFirstOrDefaultAsync(
                        nombre.Proceso + "2",
                        new
                        {
                            Opcion = 1,
                            Usuario = datosToken.Usuario,
                            TipoMaquina = proceso.TipoMaquina,
                            ClaveProceso = proceso.ClaveProceso,
                            Descripcion  = proceso.Descripcion,
                            Tratamiento = proceso.Tratamiento,
                            SubProceso = proceso.SubProceso,
                            Tintas = proceso.Tintas,
                            ConSuaje = proceso.ConSuaje

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
        public async Task<Result> Editar(TokenData datosToken, FCAPROGCAT009Entity proceso)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(datosToken.Conexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Actualiza);
                    TranformaDataTable Ds = new TranformaDataTable();
                    await con.QueryFirstOrDefaultAsync(
                        nombre.Proceso + "2",
                        new
                        {
                            Opcion = 2,
                            Usuario = datosToken.Usuario,
                            TipoMaquina = proceso.TipoMaquina,
                            ClaveProceso = proceso.ClaveProceso,
                            Descripcion = proceso.Descripcion,
                            Estatus = proceso.Estatus,
                            Tratamiento = proceso.Tratamiento,
                            SubProceso = proceso.SubProceso,
                            Tintas = proceso.Tintas,
                            ConSuaje = proceso.ConSuaje

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
        public async Task<Result> Estatus(TokenData datosToken, FCAPROGCAT009Entity proceso)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(datosToken.Conexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Actualiza);
                    TranformaDataTable Ds = new TranformaDataTable();
                    await con.QueryFirstOrDefaultAsync(
                        nombre.Proceso + "2",
                        new
                        {
                            Opcion = 3,
                            Usuario = datosToken.Usuario,
                            TipoMaquina = proceso.TipoMaquina,
                            ClaveProceso = proceso.ClaveProceso,
                            Estatus = proceso.Estatus
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
