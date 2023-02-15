using Dapper;
using Entity.DTO;
using Entity.DTO.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Data
{
    public class MaquinasData
    {
        public async Task<Result> ListarMaquinas(string strConexion, int startRow, int endRow, string ClaveMaquina, string zona)
        {
            Result objResult = new Result();
            try
            {

                using (var con = new SqlConnection(strConexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        nombre.Maquina + "1",
                        new
                        {
                            Opcion = 1,
                            startRow,
                            endRow,
                            ClaveMaquina,
                            ZonaId = zona
                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.Correcto = true;
                    objResult.data = await result.ReadAsync<ListadoMaquinas>();
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
        public async Task<Result> ListarDesperdicios(string strConexion, string ClaveMaquina)
        {
            Result objResult = new Result();
            try
            {

                using (var con = new SqlConnection(strConexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        nombre.Maquina + "1",
                        new
                        {
                            Opcion = 2,
                            ClaveMaquina
                        },
                    commandType: CommandType.StoredProcedure);
                    //objResult.Correcto = true;
                    objResult.data = await result.ReadAsync<Desperdicios>();
                    //objResult.totalRecords = await result.ReadFirstAsync<int>();
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Result> ListarProcesos(string strConexion, string zona)
        {
            Result objResult = new Result();
            try
            {

                using (var con = new SqlConnection(strConexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        nombre.Maquina + "1",
                        new
                        {
                            Opcion = 3,
                            ZonaId = zona
                        },
                    commandType: CommandType.StoredProcedure);
                    //objResult.Correcto = true;
                    objResult.data = await result.ReadAsync<Procesos>();
                    //objResult.totalRecords = await result.ReadFirstAsync<int>();
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<Result> ListarEvaluaciones(string strConexion, string zona)
        {
            Result objResult = new Result();
            try
            {

                using (var con = new SqlConnection(strConexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        nombre.Maquina + "1",
                        new
                        {
                            Opcion = 4,
                            ZonaId = zona
                        },
                    commandType: CommandType.StoredProcedure);
                    //objResult.Correcto = true;
                    objResult.data = await result.ReadAsync<Evaluaciones>();
                    //objResult.totalRecords = await result.ReadFirstAsync<int>();
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<Result> ListarTripulaciones(string strConexion, string zona)
        {
            Result objResult = new Result();
            try
            {

                using (var con = new SqlConnection(strConexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        nombre.Maquina + "1",
                        new
                        {
                            Opcion = 5,
                            ZonaId = zona
                        },
                    commandType: CommandType.StoredProcedure);
                    //objResult.Correcto = true;
                    objResult.data = await result.ReadAsync<Tripulaciones>();
                    //objResult.totalRecords = await result.ReadFirstAsync<int>();
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<Result> ListarTripulacionesPorMaquina(string strConexion, string ClaveMaquina, string zona)
        {
            Result objResult = new Result();
            try
            {

                using (var con = new SqlConnection(strConexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        nombre.Maquina + "1",
                        new
                        {
                            Opcion = 6, ClaveMaquina,
                            ZonaId = zona
                        },
                    commandType: CommandType.StoredProcedure);
                    //objResult.Correcto = true;
                    objResult.data = await result.ReadAsync<Tripulaciones>();
                    //objResult.totalRecords = await result.ReadFirstAsync<int>();
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<Result> CargaInformacionMaquina(string strConexion, string zona, string ClaveMaquina)
        {
            Result objResult = new Result();
            try
            {

                using (var con = new SqlConnection(strConexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        nombre.Maquina + "1",
                        new
                        {
                            Opcion = 7,
                            ClaveMaquina,
                            ZonaId = zona
                        },
                    commandType: CommandType.StoredProcedure);
                    //objResult.Correcto = true;
                    objResult.data = await result.ReadAsync<Maquina>();
                    //objResult.totalRecords = await result.ReadFirstAsync<int>();
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<Result> ListarPuestos(string strConexion, string ClaveMaquina, int startRow, int endRow)
        {
            Result objResult = new Result();
            try
            {

                using (var con = new SqlConnection(strConexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        nombre.Maquina + "1",
                        new
                        {
                            Opcion = 9,
                            ClaveMaquina
                            ,startRow,endRow
                        },
                    commandType: CommandType.StoredProcedure);
                    //objResult.Correcto = true;
                    objResult.data = await result.ReadAsync<Puestos>();
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

        public async Task<Result> ValidaMaquinaDefault(string strConexion, string ClaveMaquina,int Proceso)
        {
            Result objResult = new Result();
            try
            {

                using (var con = new SqlConnection(strConexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        nombre.Maquina + "1",
                        new
                        {
                            Opcion = 8,
                            ClaveMaquina,
                            Proceso
                        },
                    commandType: CommandType.StoredProcedure);
                    //objResult.Correcto = true;
                    //objResult.data = await result.ReadAsync<Maquina>();
                    objResult.Correcto = await result.ReadFirstAsync<bool>();
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<Result> ValidaMaquinaExiste(string strConexion, string ClaveMaquina)
        {
            Result objResult = new Result();
            try
            {

                using (var con = new SqlConnection(strConexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        nombre.Maquina + "1",
                        new
                        {
                            Opcion = 10,
                            ClaveMaquina
                        },
                    commandType: CommandType.StoredProcedure);
                    //objResult.Correcto = true;
                    //objResult.data = await result.ReadAsync<Maquina>();
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

        public async Task<Result> GuardaMaquina(string strConexion, string UsuarioERP, guardaMaquina obj)
        {
            Result objResult = new Result();
            int result = 0;
            try
            {
                using (var con = new SqlConnection(strConexion))
                {
                    SPNombre nombre = new SPNombre(Data.Enums.SpTipo.Actualiza);
                    DataSet dtsDesperdicios;
                    dtsDesperdicios = SerializedDataSet(obj.Desperdicios);

                    DataSet dtsTripulacionestmp;                  
                    dtsTripulacionestmp = SerializedDataSet(obj.Tripulaciones);
                     dtsTripulacionestmp.Tables[0].Columns.Remove("Nombre");

                    result = await con.ExecuteAsync(
                        nombre.Maquina+"2",
                        new 
                        { 
                            Opcion = 1,
                            TipoMaquina=obj.Maquinas[0].TipoMaquina,
                            ClaveMaquina =obj.Maquinas[0].ClaveMaquina,
                            Nombre =obj.Maquinas[0].Nombre,
                            Troquela= obj.Maquinas[0].Troquela,
                            TintasMax= obj.Maquinas[0].TintasMax,
                            AnchoMax= obj.Maquinas[0].AnchoMax,
                            AnchoMaxT= obj.Maquinas[0].AnchoMaxT,
                            AnchoMaxAlt= obj.Maquinas[0].AnchoMaxAlt,
                            AnchoMin= obj.Maquinas[0].AnchoMin,
                            LargoMax= obj.Maquinas[0].LargoMax,
                            LargoMaxT= obj.Maquinas[0].LargoMaxT,
                            LargoMin= obj.Maquinas[0].LargoMin,
                            CostoMinuto= obj.Maquinas[0].CostoMinuto,
                            CapacidadSemanal= obj.Maquinas[0].CapacidadSemanal,
                            SalariosTurno= obj.Maquinas[0].SalariosTurno,
                            Desp1000= obj.Maquinas[0].Desp1000,
                            Desp10000 = obj.Maquinas[0].Desp10000,
                            DespMayor10000 = obj.Maquinas[0].DespMayor10000,
                            M2Hora = obj.Maquinas[0].M2Hora,
                            Eficiencia = obj.Maquinas[0].Eficiencia,
                            Tproceso = obj.Maquinas[0].Tproceso,
                            Default = obj.Maquinas[0].Default,
                            CodEvaluacion = obj.Maquinas[0].CodEvaluacion,
                            MinStd = obj.Maquinas[0].MinStd,
                            TurnosxDia = obj.Maquinas[0].TurnosxDia,
                            EvaluaMtto = obj.Maquinas[0].EvaluaMtto,
                            PorcenEvaluaMtto = obj.Maquinas[0].PorcenEvaluaMtto,
                            UsuarioERP,
                            Desperdicios=dtsDesperdicios.Tables[0],
                            Tripulaciones=dtsTripulacionestmp.Tables[0],
                            CambioEficiencia=obj.CambioEficiencia,
                            CambioCodEvaluacion = obj.CambioCodEvaluacion
                        }, commandType: CommandType.StoredProcedure
                        );
                    objResult.Correcto = result != 0 ? true : false;
                    objResult.Mensaje = result != 0 ? "Se guardo correctamente la Maquina" : "";
                    return objResult;
                }
            }
            catch (SqlException e)
            {
                objResult.Mensaje = e.Message;
                return objResult;
            }
        }

        public async Task<Result> EliminaMaquina(string strConexion, string ClaveMaquina, string UsuarioERP)
        {
            Result objResult = new Result();
            try
            {

                using (var con = new SqlConnection(strConexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Actualiza);
                    var result = await con.ExecuteAsync(
                        nombre.Maquina + "2",
                        new
                        {
                            Opcion = 2,
                            ClaveMaquina,
                            UsuarioERP
                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.Correcto = result != 0 ? true : false;
                    objResult.Mensaje = result != 0 ? "Se Elimino correctamente la Maquina" : "";
                    return objResult;
                }
                
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<Result> ActivaMaquina(string strConexion, string ClaveMaquina, string UsuarioERP)
        {
            Result objResult = new Result();
            try
            {

                using (var con = new SqlConnection(strConexion))
                {
                    SPNombre nombre = new SPNombre(Enums.SpTipo.Actualiza);
                    var result = await con.ExecuteAsync(
                        nombre.Maquina + "2",
                        new
                        {
                            Opcion = 3,
                            ClaveMaquina,
                            UsuarioERP
                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.Correcto = result != 0 ? true : false;
                    objResult.Mensaje = result != 0 ? "Se Activo correctamente la Maquina" : "";
                   // objResult.Mensaje = await result.ReadFirstAsync<string>();
                    return objResult;
                }

            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }

        DataSet SerializedDataSet(object[] obj)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            System.IO.StringWriter sw = new System.IO.StringWriter();
            serializer.Serialize(sw, obj);
            DataSet ds = new DataSet();
            System.IO.StringReader reader = new System.IO.StringReader(sw.ToString());
            ds.ReadXml(reader);
            return ds;
        }

    }
}
