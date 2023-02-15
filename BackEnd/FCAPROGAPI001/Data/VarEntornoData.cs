using Dapper;
using Entity;
using Entity.DTO.Common;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Data
{
    public class VarEntornoData
    {
        public async Task<Result> getVarEntorno(string strConexion)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(strConexion))
                {
                    var result = await con.QueryMultipleAsync(
                         "FCAPROG010MWSPC1_1",
                          new
                          {
                              Opcion = 1
                          },
                      commandType: CommandType.StoredProcedure);
                    objResult.Correcto = true;
                    objResult.data = await result.ReadAsync<FCAPROG010MWEntity>();
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
        public async Task<Result> ModificarVariables(TokenData datosToken, FCAPROG010MWEntity Variables)
        {
            Result objResult = new Result();
            try
            {
                using (var con = new SqlConnection(datosToken.Conexion))
                {
                    //SPNombre nombre = new SPNombre(Enums.SpTipo.Actualiza);
                    TranformaDataTable Ds = new TranformaDataTable();
                    await con.QueryFirstOrDefaultAsync(
                        "FCAPROG010MWSPA2_1",
                        new
                        {
 
                            Datos = Ds.CreateDataTable(Variables.lstdatos).AsTableValuedParameter("FCAPROGDAT024TD_001"),
                            usuario = datosToken.Usuario,
                            opcion = 1

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
