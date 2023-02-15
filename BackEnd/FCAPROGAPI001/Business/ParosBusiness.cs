using Data;
using Entity;
using Entity.DTO.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ParosBusiness
    {
        public Task<Result> GetParos(string strConexion, int startRow, int endRow, string filtro, int Estatus)
        {
            return new ParosData().GetParos(strConexion, startRow, endRow, filtro , Estatus);
        }
        public async Task<Result> Agregar(TokenData datosToken, FCAPRODCAT012Entity paro)
        {
            try
            {
                return await new ParosData().Agregar(datosToken, paro);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Result> Editar(TokenData datosToken, FCAPRODCAT012Entity paro)
        {
            try
            {
                return await new ParosData().Editar(datosToken, paro);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Result> Eliminar(TokenData datosToken, FCAPRODCAT012Entity paro)
        {
            try
            {
                return await new ParosData().Eliminar(datosToken, paro);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
