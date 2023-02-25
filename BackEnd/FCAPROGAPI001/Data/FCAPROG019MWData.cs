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
    public class FCAPROG019MWData
    {
        public async Task<Result> leeHoraLocal(TokenData DatosToken)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(DatosToken.Conexion))
                {
                    //SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        "FCAPROG019MWSPC1",
                        new
                        {
                            Opcion = 1
                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.data = await result.ReadAsync<FCAPROG019MWEntity>();
                    //objResult.totalRecords = await result.ReadFirstAsync<int>();
                    objResult.Correcto = true;
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<Result> buscaPrograma(TokenData DatosToken, string programa)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(DatosToken.Conexion))
                {
                    //SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        "FCAPROG019MWSPC1",
                        new
                        {
                            Opcion = 2,
                            Programa = Convert.ToInt32(programa)
                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.data = await result.ReadAsync<datosBusquedaPrograma>();
                    //objResult.totalRecords = await result.ReadFirstAsync<int>();
                    objResult.Correcto = true;
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<Result> buscaProduccion(TokenData DatosToken, string programa)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(DatosToken.Conexion))
                {
                    var result = await con.QueryMultipleAsync(
                        "FCAPROG019MWSPC1",
                        new
                        {
                            Opcion = 3,
                            Programa = Convert.ToInt32(programa)
                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.data = await result.ReadAsync<datosBusquedaProduccion>();
                    objResult.Correcto = true;
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<Result> buscaTripulacionMaquina(TokenData DatosToken, string claveMaquina)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(DatosToken.Conexion))
                {
                    var result = await con.QueryMultipleAsync(
                        "FCAPROG019MWSPC1",
                        new
                        {
                            Opcion = 4,
                            ClaveMaquina = claveMaquina
                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.data = await result.ReadAsync<datosTripulacion>();
                    objResult.Correcto = true;
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<Result> leePrograma(TokenData DatosToken, string fechaAnterior, string programa, string claveMaquina, string turno)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(DatosToken.Conexion))
                {
                    var result = await con.QueryMultipleAsync(
                        "FCAPROG019MWSPC1",
                        new
                        {
                            Opcion = 5,
                            wFechaAnterior = fechaAnterior != null ? fechaAnterior.Replace("-", "") : fechaAnterior,
                            Programa = Convert.ToInt32(programa),
                            ClaveMaquina = claveMaquina,
                            Turno = Convert.ToInt32(turno)
                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.data = await result.ReadAsync<datosPrograma>();
                    objResult.Correcto = true;
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<Result> buscaSupervisor(TokenData DatosToken)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(DatosToken.Conexion))
                {
                    var result = await con.QueryMultipleAsync(
                        "FCAPROG019MWSPC1",
                        new
                        {
                            Opcion = 6
                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.data = await result.ReadAsync<comboSupervisor>();
                    objResult.Correcto = true;
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<Result> buscaParafina(TokenData DatosToken)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(DatosToken.Conexion))
                {
                    var result = await con.QueryMultipleAsync(
                        "FCAPROG019MWSPC1",
                        new
                        {
                            Opcion = 7
                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.data = await result.ReadAsync<comboParafina>();
                    objResult.Correcto = true;
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<Result> validarGuardado(TokenData DatosToken, string programa, string claveMaquina, string turno, string fecha)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(DatosToken.Conexion))
                {
                    var result = await con.QueryMultipleAsync(
                        "FCAPROG019MWSPC1",
                        new
                        {
                            Opcion = 8,
                            Programa = Convert.ToInt32(programa),
                            ClaveMaquina = claveMaquina,
                            Turno = Convert.ToInt32(turno),
                            Fecha = fecha.Replace("-", "")
                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.data = await result.ReadAsync<datosBusquedaPrograma>();
                    objResult.Correcto = true;
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<Result> guardar(TokenData DatosToken,
            string fecha, string horaIni, string horaFin, string turno, string supervisor, string minutos, string despCorrguradora,
            string despImpresora, string despAcabados, string fechaNow, string parafina, string pesoLamina, string pesoCaja, string retrabajo,
            string actCantidad, string idTripulacion, string programa, string claveMaquina, string wFechaAnterior, string idUnico
        )
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(DatosToken.Conexion))
                {
                    var result = await con.QueryMultipleAsync(
                        "FCAPROG019MWSPA2",
                        new
                        {
                            Opcion = 1,
                            Fecha = fecha.Replace("-", ""),
                            HoraIni = horaIni,
                            HoraFin = horaFin,
                            Turno = Convert.ToInt32(turno),
                            Supervisor = supervisor,
                            Minutos = Convert.ToInt32(minutos),
                            DespCorrugadora = Convert.ToInt32(despCorrguradora),
                            DespImpresora = Convert.ToInt32(despImpresora),
                            DespAcabados = Convert.ToInt32(despAcabados),
                            FechaNow = fechaNow.Replace("-", ""),
                            UsuarioERP = DatosToken.Usuario,
                            Parafina = parafina,
                            PesoLamina = Convert.ToInt32(pesoLamina),
                            PesoCaja = Convert.ToInt32(pesoCaja),
                            Retrabajo = Convert.ToInt32(retrabajo),
                            ActCantidad = Convert.ToInt32(actCantidad),
                            IdTripulacion = Convert.ToInt32(idTripulacion),

                            Programa = Convert.ToInt32(programa),
                            ClaveMaquina = claveMaquina,
                            wFechaAnterior = wFechaAnterior.Replace("-", ""),
                            IdUnico = Convert.ToInt32(idUnico)
                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.data = await result.ReadAsync<string>();
                    objResult.Correcto = true;
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }


        // METODOS DE PAGINA L
        public async Task<Result> cargaComboMaquinas(TokenData DatosToken)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(DatosToken.Conexion))
                {
                    var result = await con.QueryMultipleAsync(
                        "FCAPROG019MWSPC1",
                        new
                        {
                            Opcion = 9
                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.data = await result.ReadAsync<comboMaquinas>();
                    objResult.Correcto = true;
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<Result> buscaProgramas(TokenData DatosToken, string fecha, string fechaF, string turno, string claveMaquina, string sinFechaProd)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(DatosToken.Conexion))
                {
                    var result = await con.QueryMultipleAsync(
                        "FCAPROG019MWSPC1",
                        new
                        {
                            Opcion = 10,
                            Fecha = fecha.Replace("-", ""),
                            FechaF = fechaF.Replace("-", ""),
                            Turno = Convert.ToInt32(turno),
                            ClaveMaquina = claveMaquina,
                            SinFechaProd = sinFechaProd == "0" ? false : true
                        },
                    commandType: CommandType.StoredProcedure, commandTimeout: 120); // 2 minutos
                    objResult.data = await result.ReadAsync<datosPrograma>();
                    objResult.Correcto = true;
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Result> actualizaSupTrip(TokenData DatosToken, programasSeleccionadosL datos)
        {
            Result objResult = new Result();

            List<programas> eDato = new List<programas>();
            programas tmp;
            for (int i = 0; i < datos.programasSeleccionados.Count; i++)
            {
                tmp = new programas();
                tmp.Fecha = datos.programasSeleccionados[i].Fecha.Replace("-", "");
                tmp.Supervisor = datos.programasSeleccionados[i].Supervisor.Trim();
                tmp.IdTripulacion = datos.programasSeleccionados[i].IdTripulacion;
                tmp.IdUnico = datos.programasSeleccionados[i].IdUnico;
                eDato.Add(tmp);
            }

            try
            {
                using (var con = new SqlConnection(DatosToken.Conexion))
                {
                    TranformaDataTable Ds = new TranformaDataTable();

                    var result = await con.QueryMultipleAsync(
                        "FCAPROG019MWSPA2",
                        new
                        {
                            Opcion = 2,
                            CMODAT021TD_001 = Ds.CreateDataTable(eDato).AsTableValuedParameter("CMODAT021TD_001")
                        },
                    commandType: CommandType.StoredProcedure, commandTimeout: 60); // 1 mins
                    objResult.data = await result.ReadAsync<string>();
                    objResult.Correcto = true;
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }

        // =========================================================================================================================================
        // MODULO 2

        // BUSCA PROGRAMA
        public async Task<Result> buscaProgramaMod2(TokenData DatosToken, string programa)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(DatosToken.Conexion))
                {
                    //SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        "FCAPROG019MWSPC1",
                        new
                        {
                            Opcion = 11,
                            Programa = Convert.ToInt32(programa)
                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.data = await result.ReadAsync<datosBusquedaPrograma>();
                    //objResult.totalRecords = await result.ReadFirstAsync<int>();
                    objResult.Correcto = true;
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Result> getClavesPreparacion(TokenData DatosToken, string claveMaquina)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(DatosToken.Conexion))
                {
                    //SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        "FCAPROG019MWSPC1",
                        new
                        {
                            Opcion = 12,
                            ClaveMaquina = claveMaquina
                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.data = await result.ReadAsync<clavesProcesos>();
                    objResult.Correcto = true;
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Result> obtenerDesperdicios(TokenData DatosToken, string op, string programa, string turno, string tipoConsulta)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(DatosToken.Conexion))
                {
                    //SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        "FCAPROG019MWSPC1",
                        new
                        {
                            Opcion = 13,
                            TipoConsulta = Convert.ToInt32(tipoConsulta),
                            OP = op,
                            Programa = Convert.ToInt32(programa),
                            Turno = Convert.ToInt32(turno)
                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.data = await result.ReadAsync<desperdiciosCapturados>();
                    objResult.Correcto = true;
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Result> cargaSupsMod2(TokenData DatosToken, string programa, string turno)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(DatosToken.Conexion))
                {
                    //SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        "FCAPROG019MWSPC1",
                        new
                        {
                            Opcion = 14,
                            Programa = Convert.ToInt32(programa),
                            Turno = Convert.ToInt32(turno)
                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.data = await result.ReadAsync<comboSupervisor>();
                    objResult.Correcto = true;
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Result> cargaSuajeMod2(TokenData DatosToken, string suaje)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(DatosToken.Conexion))
                {
                    //SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        "FCAPROG019MWSPC1",
                        new
                        {
                            Opcion = 15,
                            Suaje = suaje
                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.data = await result.ReadAsync<comboSuaje>();
                    objResult.Correcto = true;
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Result> cargaGrabadosMod2(TokenData DatosToken, string articulo)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(DatosToken.Conexion))
                {
                    //SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        "FCAPROG019MWSPC1",
                        new
                        {
                            Opcion = 16,
                            Articulo = articulo
                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.data = await result.ReadAsync<comboGrabado>();
                    objResult.Correcto = true;
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Result> cargaMaqProcAntMod2(TokenData DatosToken)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(DatosToken.Conexion))
                {
                    //SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        "FCAPROG019MWSPC1",
                        new
                        {
                            Opcion = 17,
                            ZonaERP = DatosToken.Zona
                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.data = await result.ReadAsync<comboMaquinas>();
                    objResult.Correcto = true;
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Result> cargaCantidadRecMod2(TokenData DatosToken, string op, string claveMaquina, string programa)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(DatosToken.Conexion))
                {
                    //SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        "FCAPROG019MWSPC1",
                        new
                        {
                            Opcion = 18,
                            OP = op,
                            ClaveMaquina = claveMaquina,
                            Programa = Convert.ToInt32(programa)
                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.data = await result.ReadAsync<datosBusquedaProduccion>();
                    objResult.Correcto = true;
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Result> cargaConceptosDesp(
            TokenData DatosToken, string maquinaDesperdicio, string op, string programa, string claveMaquina, string turno, 
            string aplicaCajaRec, string esUtilizado, string esContabilizadoPLC, string esProcesoAnterior
        )
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(DatosToken.Conexion))
                {
                    //SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                    var result = await con.QueryMultipleAsync(
                        "FCAPROG019MWSPC1",
                        new
                        {
                            Opcion = 19,
                            MaquinaDesperdicio = maquinaDesperdicio,
                            OP = op,
                            Programa = Convert.ToInt32(programa),
                            ClaveMaquina = claveMaquina,
                            Turno = Convert.ToInt32(turno),
                            AplicaCajaRec = Convert.ToBoolean(aplicaCajaRec == "1" ? true : false),
                            EsUtilizado = Convert.ToBoolean(esUtilizado == "1" ? true : false),
                            EsContabilizadoPLC = Convert.ToBoolean(esContabilizadoPLC == "1" ? true : false),
                            EsProcesoAnterior = Convert.ToBoolean(esProcesoAnterior == "1" ? true : false)
                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.data = await result.ReadAsync<conceptosDespMod2>();
                    objResult.Correcto = true;
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Result> guardarDespMod2(TokenData DatosToken, camposFrmDesp datos) 
        {
            Result objResult = new Result();

            List<conceptosDespMod2> eDato = new List<conceptosDespMod2>();
            conceptosDespMod2 tmp;
            for (int i = 0; i < datos.gridDatos.Count; i++)
            {
                tmp = new conceptosDespMod2();
                tmp.IdConcepto = datos.gridDatos[i].IdConcepto;
                tmp.Concepto = datos.gridDatos[i].Concepto;
                tmp.Cantidad = datos.gridDatos[i].Cantidad;
                tmp.EsUtilizado = datos.gridDatos[i].EsUtilizado;
                tmp.OP = datos.gridDatos[i].OP;
                tmp.ClaveMaquinaDesp = datos.gridDatos[i].ClaveMaquinaDesp;
                tmp.Programa = datos.gridDatos[i].Programa;
                tmp.Turno = datos.gridDatos[i].Turno;
                tmp.ClaveMaquinaCap = datos.gridDatos[i].ClaveMaquinaCap;
                tmp.MaquinaConcepto = datos.gridDatos[i].MaquinaConcepto;
                eDato.Add(tmp);
            }

            try
            {
                using (var con = new SqlConnection(DatosToken.Conexion))
                {
                    TranformaDataTable Ds = new TranformaDataTable();

                    var result = await con.QueryMultipleAsync(
                        "FCAPROG019MWSPA2",
                        new
                        {
                            Opcion = 3,
                            AplicaCajaRec = datos.AplicaCajaRec,
                            ClaveMaquinaCap = datos.AplicaCajaRec ? datos.ClaveMaquina : datos.MaquinaDesperdicio,
                            ClaveMaquinaDesp = datos.MaquinaDesperdicio,
                            Programa = datos.Programa,
                            Turno = datos.DespTurno,
                            EsUtilizado = datos.EsUtilizado,
                            EsProcesoAnterior = datos.EsProcesoAnterior,
                            EsContabilizadoPLC = datos.EsContabilizadoPLC,
                            IdTipoDesp = datos.DespId,
                            UsuarioERP = DatosToken.Usuario,

                            CMODAT382_259TD_001 = Ds.CreateDataTable(eDato).AsTableValuedParameter("CMODAT382_259TD_001")
                        },
                    commandType: CommandType.StoredProcedure, commandTimeout: 60); // 1 mins
                    objResult.data = await result.ReadAsync<string>();
                    objResult.Correcto = true;
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<Result> validaDatosSupervisorMod2(TokenData DatosToken, string claveMaquina, string turno, string fecha, string claveSup, string programa)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(DatosToken.Conexion))
                {
                    var result = await con.QueryMultipleAsync(
                        "FCAPROG019MWSPC1",
                        new
                        {
                            Opcion = 20,
                            Programa = Convert.ToInt32(programa),
                            ClaveMaquina = claveMaquina,
                            Turno = Convert.ToInt32(turno),
                            Fecha = fecha.Replace("-", ""),
                            ClaveSupervisor = claveSup
                        },
                    commandType: CommandType.StoredProcedure);
                    objResult.data = await result.ReadAsync<validaDatosSupervisor>();
                    objResult.Correcto = true;
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<Result> gardarDatosMod2(TokenData DatosToken, camposGuardado datos)
        {
            Result objResult = new Result();

            try
            {
                using (var con = new SqlConnection(DatosToken.Conexion))
                {
                    TranformaDataTable Ds = new TranformaDataTable();

                    var result = await con.QueryMultipleAsync(
                        "FCAPROG019MWSPA2",
                        new
                        {
                            Opcion = 4,
                            UsuarioERP = DatosToken.Usuario,
                            IgnoraTiempo = datos.IgnoraTiempo,
                            IdUnico = datos.IdUnico,
                            wFechaAnterior = datos.wFechaAnterior,
                            ChkProceso = datos.ChkProceso,
                            LSupervisor = datos.LSupervisor,
                            LSuaje = datos.LSuaje,
                            LGrabado = datos.LGrabado,
                            LMinutos = datos.LMinutos,
                            LProceso = datos.LProceso,
                            LVelocidad = datos.LVelocidad,
                            LEficiencia = datos.LEficiencia,
                            LMinStd = datos.LMinStd,
                            Programa = datos.Programa,
                            MinStdProd = datos.MinStdProd,
                            TxtDespCorrUtil = datos.TxtDespCorrUtil,
                            TxtDespImprUtil = datos.TxtDespImprUtil,
                            TxtPesoLamina = datos.TxtPesoLamina,
                            TxtPesoCaja = datos.TxtPesoCaja,
                            TxtRetrabajo = datos.TxtRetrabajo,
                            CmbMaquinaPA = datos.CmbMaquinaPA,
                            TxtDesPAUtul = datos.TxtDesPAUtul,
                            Cant1 = datos.Cant1,
                            Cant2 = datos.Cant2,
                            Cant3 = datos.Cant3,
                            Cant4 = datos.Cant4,
                            TxtCantidad = datos.TxtCantidad,
                            TxtCantidadCajasRec = datos.TxtCantidadCajasRec,
                            FecProduccion = datos.FecProduccion,
                            HoraIni = datos.HoraIni,
                            HoraFin = datos.HoraFin,
                            Turno = datos.Turno,
                            IdTripulacion = datos.IdTripulacion,
                            CmbMaquina = datos.CmbMaquina,
                            ChkMP = datos.ChkMP
                        },
                    commandType: CommandType.StoredProcedure, commandTimeout: 60); // 1 mins
                    //objResult.data = await result.ReadAsync<string>();
                    objResult.Correcto = true;
                }
                return objResult;
            }
            catch (Exception ex)
            {
                objResult.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
