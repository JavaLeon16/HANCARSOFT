using Data;
using Entity;
using Entity.DTO.Common;
using System.Threading.Tasks;

namespace Business
{
    public class RepConsumoRollosCorrugadoraBusiness
    {

        public Task<Result> GetClases(string strConexion)
        {
            return new RepConsumoRollosCorrugadoraData().GetClases(strConexion);
        }

        public Task<Result> GetSubClases(string strConexion)
        {
            return new RepConsumoRollosCorrugadoraData().GetSubClases(strConexion);
        }

        public Task<Result> GetTripulaciones(string strConexion)
        {
            return new RepConsumoRollosCorrugadoraData().GetTripulaciones(strConexion);
        }

        public Task<Result> GetDatosConsumoRollos(string strConexion, FCAPROG001RWParametrosEntity modelo, string pZona)
        {
            return new RepConsumoRollosCorrugadoraData().GetDatosConsumoRollos(strConexion, modelo, pZona);
        }
    }
}
