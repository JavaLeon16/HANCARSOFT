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
    public class DesperdiciosData
    {
        public async Task<Result> GetDesperdicios(TokenData datosToken, int startRow, int endRow, string DescripcionDesperdicio, bool? AplicaImpresora, bool? AplicaCorrugadora , bool? AplicaAcabado, bool? AplicaRecuperacionCaja)
        {
            Result objResult = new Result();
            try
            {
                
                using (var con = new SqlConnection(datosToken.Conexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        nombre.Desperdicio + "",
                        new
                        {
                            Opcion = 1,
                            startRow,
                            endRow,
                            DescripcionDesperdicio = DescripcionDesperdicio,
                            ZonaId = datosToken.Zona,
                            AplicaImpresora = AplicaImpresora == false ? null : AplicaImpresora,
                            AplicaCorrugadora = AplicaCorrugadora == false ? null : AplicaCorrugadora,
                            AplicaAcabado = AplicaAcabado == false ? null : AplicaAcabado,
                            AplicaRecuperacionCaja = AplicaRecuperacionCaja == false ? null : AplicaRecuperacionCaja,

                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.Correcto = true;
                    objResult.data = await result.ReadAsync<FCAPRODCAT016Entity>();
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
        public async Task<Result> GetConfigAreaDesperdicios(TokenData datosToken, int startRow, int endRow, int ClaveDesperdicio)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(datosToken.Conexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        nombre.Desperdicios + "1",
                        new
                        {
                            Opcion = 1,
                            startRow,
                            endRow,
                            ClaveDesperdicio,
                            ZonaId = datosToken.Zona
                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.Correcto = true;
                    objResult.data = await result.ReadAsync<FCAPRODCAT011Entity>();
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Result> GetListaDesperdicio(TokenData datosToken)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(datosToken.Conexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        nombre.Desperdicios + "1",
                        new
                        {
                            Opcion = 2,
                            ZonaId = datosToken.Zona

                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.Correcto = true;
                    objResult.data = await result.ReadAsync<DesperdicioDTO>();
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Result> GetListaArea(TokenData datosToken, bool EsAreaCaptura)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(datosToken.Conexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        nombre.Desperdicios + "1",
                        new
                        {
                            Opcion = 3,
                            EsAreaCaptura = EsAreaCaptura,
                            ZonaId = datosToken.Zona

                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.Correcto = true;
                    objResult.data = await result.ReadAsync<AreaCapturaCargoDTO>();
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Result> Agregar(TokenData datosToken, FCAPRODCAT016Entity Datos)
        {
            Result objResult = new Result();
            TranformaDataTable tfDataTable = new TranformaDataTable();
            try
            {
                using (var con = new SqlConnection(datosToken.Conexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Actualiza);
                    TranformaDataTable Ds = new TranformaDataTable();
                    await con.QueryFirstOrDefaultAsync(
                        nombre.Desperdicio + "",
                        new
                        {
                            Opcion = 1,
                            Usuario = datosToken.Usuario,
                            DescripcionDesperdicio = Datos.DescripcionDesperdicio,
                            AplicaImpresora = Datos.AplicaImpresora,
                            AplicaCorrugadora = Datos.AplicaCorrugadora,
                            AplicaAcabado = Datos.AplicaAcabado,
                            AplicaRecuperacionCaja = Datos.AplicaRecuperacionCaja,
                            tblConfigDesperdicio = tfDataTable.CreateDataTable(Datos.datConfiAreaDes).AsTableValuedParameter("FCAPRODCAT011TD001"),
                            ZonaId = datosToken.Zona

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
        public async Task<Result> Editar(TokenData datosToken, FCAPRODCAT016Entity Datos)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(datosToken.Conexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Actualiza);
                    TranformaDataTable Ds = new TranformaDataTable();

                    await con.QueryFirstOrDefaultAsync(
                        nombre.Desperdicio + "",
                        new
                        {
                            Opcion = 2,
                            Usuario = datosToken.Usuario,
                            DescripcionDesperdicio = Datos.DescripcionDesperdicio,
                            AplicaImpresora = Datos.AplicaImpresora,
                            AplicaCorrugadora = Datos.AplicaCorrugadora,
                            AplicaAcabado = Datos.AplicaAcabado,
                            AplicaRecuperacionCaja = Datos.AplicaRecuperacionCaja,
                            tblConfigDesperdicio = Ds.CreateDataTable(Datos.datConfiAreaDes).AsTableValuedParameter("FCAPRODCAT011TD001"),
                            IdDesperdicio = Datos.IdDesperdicio,
                            ZonaId = datosToken.Zona

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
        public async Task<Result> Eliminar(TokenData datosToken, FCAPRODCAT016Entity Datos)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(datosToken.Conexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Actualiza);
                    TranformaDataTable Ds = new TranformaDataTable();
                    await con.QueryFirstOrDefaultAsync(
                        nombre.Desperdicio + "",
                        new
                        {
                            Opcion = 3,
                            Usuario = datosToken.Usuario,
                            IdDesperdicio = Datos.IdDesperdicio,
                            ZonaId = datosToken.Zona

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
        public async Task<Result> GetAreaDesperdicios(TokenData datosToken, int startRow, int endRow, string Desperdicio)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(datosToken.Conexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        nombre.AreaDesperdicios,
                        new
                        {
                            Opcion = 1,
                            startRow,
                            endRow,
                            ZonaId = datosToken.Zona,
                            Desperdicio
                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.Correcto = true;
                    objResult.data = await result.ReadAsync<FCAPRODCAT014Entity>();
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
        public async Task<Result> AgregarAreaDesperdicios(TokenData datosToken, FCAPRODCAT014Entity Datos)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(datosToken.Conexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Actualiza);
                    TranformaDataTable Ds = new TranformaDataTable();
                    await con.QueryFirstOrDefaultAsync(
                        nombre.AreaDesperdicios,
                        new
                        {
                            Opcion = 1,
                            Usuario = datosToken.Usuario,
                            Clave = Datos.Clave,
                            Nombre = Datos.Nombre,
                            ZonaId = datosToken.Zona

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
        public async Task<Result> EditarAreaDesperdicios(TokenData datosToken, FCAPRODCAT014Entity Datos)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(datosToken.Conexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Actualiza);
                    TranformaDataTable Ds = new TranformaDataTable();
                    await con.QueryFirstOrDefaultAsync(
                        nombre.AreaDesperdicios,
                        new
                        {
                            Opcion = 2,
                            Usuario = datosToken.Usuario,
                            Clave = Datos.Clave,
                            Nombre = Datos.Nombre,
                            ZonaId = datosToken.Zona

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
        public async Task<Result> EliminarAreaDesperdicios(TokenData datosToken, FCAPRODCAT014Entity Datos)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(datosToken.Conexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Actualiza);
                    TranformaDataTable Ds = new TranformaDataTable();
                    await con.QueryFirstOrDefaultAsync(
                        nombre.AreaDesperdicios,
                        new
                        {
                            Opcion = 3,
                            Usuario = datosToken.Usuario,
                            Clave = Datos.Clave,
                            ZonaId = datosToken.Zona

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
