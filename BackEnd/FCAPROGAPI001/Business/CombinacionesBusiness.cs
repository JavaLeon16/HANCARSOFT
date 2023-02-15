using Data;
using Entity;
using Entity.DTO.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class CombinacionesBusiness
    {
        public Task<Result> GetTipoCaja(string strConexion, int startRow, int endRow, string filtro)
        {
            return new CombinacionesData().GetCombinaciones(strConexion, startRow, endRow, filtro);
        }
        public Task<Result> GetPapel(string strConexion, int startRow, int endRow, string filtro, string TipoPapel)
        {
            return new CombinacionesData().GetPapel(strConexion, startRow, endRow, filtro, TipoPapel);
        }
        public async Task<Result> Agregar(TokenData datosToken, FCAPROGCAT004Entity data)
        {
            try
            {
                return await new CombinacionesData().Agregar(datosToken, data);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Result> Editar(TokenData datosToken, FCAPROGCAT004Entity data)
        {
            try
            {
                return await new CombinacionesData().Editar(datosToken, data);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Result> Eliminar(TokenData datosToken, FCAPROGCAT004Entity data)
        {
            try
            {
                return await new CombinacionesData().Eliminar(datosToken, data);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
