using Data;
using Entity;
using Entity.DTO.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class TipoCajaBusiness
    {
        public Task<Result> GetTipoCaja(string strConexion, int startRow, int endRow, string filtro)
        {
            return new TipoCajaData().GetTipoCaja(strConexion, startRow, endRow, filtro);
        }
        public async Task<Result> Agregar(TokenData datosToken, FCAPROGCAT010Entity tipocaja)
        {
            try
            {
                return await new TipoCajaData().Agregar(datosToken, tipocaja);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Result> Editar(TokenData datosToken, FCAPROGCAT010Entity tipocaja)
        {
            try
            {
                return await new TipoCajaData().Editar(datosToken, tipocaja);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Result> Eliminar(TokenData datosToken, FCAPROGCAT010Entity tipocaja)
        {
            try
            {
                return await new TipoCajaData().Eliminar(datosToken, tipocaja);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
