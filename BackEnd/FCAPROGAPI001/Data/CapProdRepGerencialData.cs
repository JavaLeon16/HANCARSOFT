using Dapper;
using Entity;
using Entity.DTO.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    // Creo: Manuel Valenzuela, 27/ene/2022
    public class CapProdRepGerencialData
    {//GetCargaNaquinas
        public async Task<Result> GetCargaMaquinas(string strConexion, int pStartRow, int pEndRow)
        {
            Result objResult = new Result();

            try
            {
                SPNombre nombre = new SPNombre(Enums.SpTipo.Consulta);
                string SP = nombre.CapProdRepGerencial + "1";
                
                // Parametros para el SP
                FCAPROGDAT023ParametrosEntity obj = new FCAPROGDAT023ParametrosEntity();
                obj.startRow = pStartRow;
                obj.endRow = pEndRow;

                using (var con = new SqlConnection(strConexion))
                {
                    var result = await con.QueryMultipleAsync(SP, obj, commandType: CommandType.StoredProcedure);
                    objResult.Correcto = true;
                    objResult.data = await result.ReadAsync<FCAPROGDAT023Entity>();
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
    }
}


/*
          * Accion = 2,
         Maquina1 = capRepProdGer.Maquina1,
         Maquina2 = capRepProdGer.Maquina2,
         Turnos = capRepProdGer.Turnos,
         Valor = capRepProdGer.Valor,
         Prom = capRepProdGer.Prom,
         UsuarioERP = datosToken.Usuario
          * */
