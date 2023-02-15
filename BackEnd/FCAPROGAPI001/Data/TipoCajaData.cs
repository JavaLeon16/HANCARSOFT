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
    public class TipoCajaData
    {
        public async Task<Result> GetTipoCaja(string strConexion, int startRow, int endRow, string filtro)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(strConexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        nombre.TipoCaja + "1",
                        new
                        {
                            Opcion = 1,
                            startRow,
                            endRow,
                            filtro = string.IsNullOrEmpty(filtro) ? "" : filtro
                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.Correcto = true;
                    objResult.data = await result.ReadAsync<FCAPROGCAT010Entity>();
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
        public async Task<Result> Agregar(TokenData datosToken, FCAPROGCAT010Entity TipoCaja)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(datosToken.Conexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Actualiza);
                    TranformaDataTable Ds = new TranformaDataTable();
                    await con.QueryFirstOrDefaultAsync(
                        nombre.TipoCaja + "2" ,
                        new
                        {
                            Opcion = 1,
                            Usuario = datosToken.Usuario,
                            ClaveDiseno = TipoCaja.ClaveDiseno,
                            Descripcion = TipoCaja.Descripcion,
                            RegExporta = TipoCaja.RegExporta,
                            PermiteCotDirecta = TipoCaja.PermiteCotDirecta,
                            ConSuaje = TipoCaja.ConSuaje,
                            FraccionArancelaria = TipoCaja.FraccionArancelaria,
  
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
        public async Task<Result> Editar(TokenData datosToken, FCAPROGCAT010Entity TipoCaja)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(datosToken.Conexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Actualiza);
                    TranformaDataTable Ds = new TranformaDataTable();
                    await con.QueryFirstOrDefaultAsync(
                        nombre.TipoCaja +"2",
                        new
                        {
                            Opcion = 2,
                            Usuario = datosToken.Usuario,
                            Descripcion = TipoCaja.Descripcion,
                            RegExporta = TipoCaja.RegExporta,
                            PermiteCotDirecta = TipoCaja.PermiteCotDirecta,
                            ConSuaje = TipoCaja.ConSuaje,
                            FraccionArancelaria = TipoCaja.FraccionArancelaria,
                            IdTipoCaja = TipoCaja.IdTipoCaja

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
        public async Task<Result> Eliminar(TokenData datosToken, FCAPROGCAT010Entity TipoCaja)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(datosToken.Conexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Actualiza);
                    TranformaDataTable Ds = new TranformaDataTable();
                    await con.QueryFirstOrDefaultAsync(
                        nombre.TipoCaja + "2",
                        new
                        {
                            Opcion = 3,
                            Usuario = datosToken.Usuario,
                            IdTipoCaja = TipoCaja.IdTipoCaja

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
