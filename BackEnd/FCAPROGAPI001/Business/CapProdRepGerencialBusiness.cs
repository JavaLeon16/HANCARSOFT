using Data;
using Entity;
using Entity.DTO.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    // Creo: Manuel Valenzuela, 27/ene/2022
    public class CapProdRepGerencialBusiness
    {

        public Task<Result> GetCargaMaquinas(string strConexion, int pStartRow, int pEndRow)
        {
            return new CapProdRepGerencialData().GetCargaMaquinas(strConexion, pStartRow, pEndRow);
        }

        public async Task<Result> InsertCargaMaquinas(TokenData datosToken, FCAPROGDAT023Entity modelo)
        {
            try
            {
                return await new CapProdRepGerencialData().InsertCargaMaquinas(datosToken, modelo);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

    }
}
