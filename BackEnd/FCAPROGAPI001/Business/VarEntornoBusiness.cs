using Data;
using Entity;
using Entity.DTO.Common;
using System;
using System.Threading.Tasks;

namespace Business
{
    public class VarEntornoBusiness
    {
        public Task<Result> getVarEntorno(string strConexion)
        {
            return new VarEntornoData().getVarEntorno(strConexion);
        }
        public async Task<Result> ModificarVariables(TokenData datosToken, FCAPROG010MWEntity Variables)
        {
            try
            {
                return await new VarEntornoData().ModificarVariables(datosToken, Variables);

            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
