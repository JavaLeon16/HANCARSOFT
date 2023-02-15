using Data;
using Entity;
using Entity.DTO.Common;
using System;
using System.Threading.Tasks;


namespace Business
{
    public class FlautasBusiness
    {
        public Task<Result> GetFlautas(string strConexion, int startRow, int endRow,string filtro)
        {
            return new FlautasData().GetFlautas(strConexion, startRow, endRow, filtro);
        }
        public Task<Result> GetCorrugados(string strConexion)
        {
            return new FlautasData().GetCorrugados(strConexion);
        }
        public async Task<Result> GuardarFlautas(TokenData datosToken, FlautasEntity obj)
        {
            try
            {

                return await new FlautasData().GuardarFlautas(datosToken, obj);

            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Result> ModificarFlautas(TokenData datosToken, FlautasEntity obj)
        {
            try
            {
                return await new FlautasData().ModificarFlautas(datosToken, obj);

            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Result> EliminarFlautas(TokenData datosToken, FlautasEntity obj)
        {
            try
            {
                return await new FlautasData().EliminarFlautas(datosToken, obj);

            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
