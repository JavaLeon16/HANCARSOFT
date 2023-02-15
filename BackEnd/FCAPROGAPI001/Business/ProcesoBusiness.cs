using Data;
using Entity;
using Entity.DTO.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ProcesoBusiness
    {
        public Task<Result> GetProceso(string strConexion, int startRow, int endRow, string filtro, string TipoMaquina, int Estatus)
        {
            return new ProcesoData().GetProceso(strConexion, startRow, endRow, filtro, TipoMaquina, Estatus);
        }
        public Task<Result> GetProcesoPorTipoMaquina(string strConexion, string TipoMaquina)
        {
            return new ProcesoData().GetProcesoPorTipoMaquina(strConexion, TipoMaquina);
        }
        public Task<Result> GetTipoMaquina(string strConexion)
        {
            return new ProcesoData().GetTipoMaquina(strConexion);
        }
        public async Task<Result> Agregar(TokenData datosToken, FCAPROGCAT009Entity proceso)
        {
            try
            {
                return await new ProcesoData().Agregar(datosToken, proceso);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Result> Editar(TokenData datosToken, FCAPROGCAT009Entity proceso)
        {
            try
            {
                return await new ProcesoData().Editar(datosToken, proceso);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Result> Estatus(TokenData datosToken, FCAPROGCAT009Entity proceso)
        {
            try
            {
                return await new ProcesoData().Estatus(datosToken, proceso);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
