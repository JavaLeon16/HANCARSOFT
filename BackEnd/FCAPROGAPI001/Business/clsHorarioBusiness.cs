using System;
using System.Collections.Generic;
using System.Text;
using Data;
using Entity.DTO.Common;
using Entity;
using System.Threading.Tasks;


namespace Business
{
    public class clsHorarioBusiness
    {
        public Task<Result> GetMaquinas(TokenData datosToken)
        {
            return new clsHorarioData().GetMaquinas(datosToken);
        }

        public Task<Result> GetHorario(TokenData datosToken, string ClaveMaquina)
        {
            return new clsHorarioData().GetHorario(datosToken, ClaveMaquina);
        }

        public async Task<clsHorario> Agregar(TokenData datosToken, clsHorario parHorario)
        {
            try
            {
                return await new clsHorarioData().Agregar(datosToken, parHorario);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<clsHorario> Modificar(TokenData datosToken, clsHorario parHorario)
        {
            try
            {
                return await new clsHorarioData().Modificar(datosToken, parHorario);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
