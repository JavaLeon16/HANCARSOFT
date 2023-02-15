using Dapper;
using Entity;
using Entity.DTO.Common;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Data
{
    public class RepConsumoRollosCorrugadoraData
    {

        public async Task<Result> GetClases(string strConexion)
        {
            Result result = new Result();

            try
            {
                string SP = "FCAPROG001RWSPC1";

                using (var con = new SqlConnection(strConexion))
                {
                    var results = await con.QueryMultipleAsync(SP, new { Accion = 1 }, commandType: CommandType.StoredProcedure);
                    result.data = await results.ReadAsync<FCAPROG001RWClaseEntity>();
                    result.Correcto = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<Result> GetSubClases(string strConexion)
        {
            Result result = new Result();

            try
            {
                string SP = "FCAPROG001RWSPC1";

                using (var con = new SqlConnection(strConexion))
                {
                    var results = await con.QueryMultipleAsync(SP, new { Accion = 2 }, commandType: CommandType.StoredProcedure);
                    result.data = await results.ReadAsync<FCAPROG001RWSubClaseEntity>();
                    result.Correcto = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<Result> GetTripulaciones(string strConexion)
        {
            Result result = new Result();

            try
            {
                string SP = "FCAPROG001RWSPC1";

                using (var con = new SqlConnection(strConexion))
                {
                    var results = await con.QueryMultipleAsync(SP, new { Accion = 3 }, commandType: CommandType.StoredProcedure);
                    result.data = await results.ReadAsync<FCAPROG001RWTripulacionEntity>();
                    result.Correcto = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<Result> GetDatosConsumoRollos(string strConexion, FCAPROG001RWParametrosEntity modelo, string pZona)
        {
            Result result = new Result();

            try
            {
                string SP = "FCAPROG001RWSPC1";

                modelo.Accion = 4;
                modelo.ZonaERP = pZona;

                using (var con = new SqlConnection(strConexion))
                {
                    var results = await con.QueryMultipleAsync(SP, modelo, commandType: CommandType.StoredProcedure);
                    result.data = await results.ReadAsync<FCAPROG001RWDatosConsumoRollosEntity>();
                    result.Correcto = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Correcto = false;
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
