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
    public class CombinacionesData
    {
        public async Task<Result> GetCombinaciones(string strConexion, int startRow, int endRow, string filtro)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(strConexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        nombre.Combinaciones + "1",
                        new
                        {
                            Opcion = 1,
                            startRow,
                            endRow,
                            filtro = string.IsNullOrEmpty(filtro) ? "" : filtro,
                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.Correcto = true;
                    objResult.data = await result.ReadAsync<FCAPROGCAT004Entity>();
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
        public async Task<Result> GetPapel(string strConexion, int startRow, int endRow, string filtro, string TipoPapel)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(strConexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        nombre.Combinaciones + "1",
                        new
                        {
                            Opcion = 2,
                            startRow,
                            endRow,
                            filtro = string.IsNullOrEmpty(filtro) ? "" : filtro,
                            TipoPapel = TipoPapel
                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.Correcto = true;
                    objResult.data = await result.ReadAsync<PapelDTO>();
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
       
        public async Task<Result> Agregar(TokenData datosToken, FCAPROGCAT004Entity data)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(datosToken.Conexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Actualiza);
                    TranformaDataTable Ds = new TranformaDataTable();
                    await con.QueryFirstOrDefaultAsync(
                        nombre.Combinaciones + "2",
                        new
                        {
                            Opcion = 1,
                            Usuario = datosToken.Usuario,
                            Clave = data.Clave,
                            Presentacion = data.Presentacion,
                            Liner1 = data.Liner1,
                            Medium1 = data.Medium1,
                            Liner2 = data.Liner2,
                            Medium2 = data.Medium2,
                            Liner3 = data.Liner3,
                            Medium3 = data.Medium3,
                            Liner4 = data.Liner4,
                            PrimeraFlauta = data.PrimeraFlauta,
                            SegundaFlauta = data.SegundaFlauta,
                            TerceraFlauta = data.TerceraFlauta,
                            PesoM2 = data.PesoM2,
                            Descripcion = data.Descripcion,
                            ClaveIngles = data.ClaveIngles,
                            Mullen = data.Mullen

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
        public async Task<Result> Editar(TokenData datosToken, FCAPROGCAT004Entity data)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(datosToken.Conexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Actualiza);
                    TranformaDataTable Ds = new TranformaDataTable();
                    await con.QueryFirstOrDefaultAsync(
                        nombre.Combinaciones + "2",
                        new
                        {
                            Opcion = 2,
                            Usuario = datosToken.Usuario,
                            Clave = data.Clave,
                            Presentacion = data.Presentacion,
                            Liner1 = data.Liner1,
                            Medium1 = data.Medium1,
                            Liner2 = data.Liner2,
                            Medium2 = data.Medium2,
                            Liner3 = data.Liner3,
                            Medium3 = data.Medium3,
                            Liner4 = data.Liner4,
                            PrimeraFlauta = data.PrimeraFlauta,
                            SegundaFlauta = data.SegundaFlauta,
                            TerceraFlauta = data.TerceraFlauta,
                            PesoM2 = data.PesoM2,
                            Descripcion = data.Descripcion,
                            ClaveIngles = data.ClaveIngles,
                            Mullen = data.Mullen

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
        public async Task<Result> Eliminar(TokenData datosToken, FCAPROGCAT004Entity data)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(datosToken.Conexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Actualiza);
                    TranformaDataTable Ds = new TranformaDataTable();
                    await con.QueryFirstOrDefaultAsync(
                        nombre.Combinaciones + "2",
                        new
                        {
                            Opcion = 3,
                            Usuario = datosToken.Usuario,
                            Clave = data.Clave

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
