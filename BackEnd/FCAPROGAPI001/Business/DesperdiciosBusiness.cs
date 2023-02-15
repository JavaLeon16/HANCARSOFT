using Data;
using Entity;
using Entity.DTO;
using Entity.DTO.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class DesperdiciosBusiness
    {
        public Task<Result> GetDesperdicios(TokenData datosToken, int startRow, int endRow, string DescripcionDesperdicio, bool AplicaImpresora, bool AplicaCorrugadora, bool AplicaAcabado, bool AplicaRecuperacionCaja)
        {
            return new DesperdiciosData().GetDesperdicios(datosToken, startRow, endRow, DescripcionDesperdicio, AplicaImpresora, AplicaCorrugadora , AplicaAcabado, AplicaRecuperacionCaja);
        }
        public Task<Result> GetConfigAreaDesperdicios(TokenData datosToken, int startRow, int endRow, int ClaveDesperdicio)
        {
            return new DesperdiciosData().GetConfigAreaDesperdicios(datosToken, startRow, endRow, ClaveDesperdicio);
        }
        public Task<Result> GetListaDesperdicio(TokenData datosToken)
        {
            return new DesperdiciosData().GetListaDesperdicio(datosToken);
        }
        public Task<Result> GetListaArea(TokenData datosToken, bool EsAreaCaptura)
        {
            return new DesperdiciosData().GetListaArea(datosToken, EsAreaCaptura);
        }
        public async Task<Result> Agregar(TokenData datosToken, FCAPRODCAT016Entity data)
        {
            try
            {
                return await new DesperdiciosData().Agregar(datosToken, data);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Result> Editar(TokenData datosToken, FCAPRODCAT016Entity data)
        {
            try
            {
                return await new DesperdiciosData().Editar(datosToken, data);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Result> Eliminar(TokenData datosToken, FCAPRODCAT016Entity data)
        {
            try
            {
                return await new DesperdiciosData().Eliminar(datosToken, data);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public Task<Result> GetAreaDesperdicios(TokenData datosToken, int startRow, int endRow, string Desperdicio)
        {
            return new DesperdiciosData().GetAreaDesperdicios(datosToken, startRow, endRow, Desperdicio);
        }
        public async Task<Result> AgregarAreaDesperdicios(TokenData datosToken, FCAPRODCAT014Entity data)
        {
            try
            {
                return await new DesperdiciosData().AgregarAreaDesperdicios(datosToken, data);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Result> EditarAreaDesperdicios(TokenData datosToken, FCAPRODCAT014Entity data)
        {
            try
            {
                return await new DesperdiciosData().EditarAreaDesperdicios(datosToken, data);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public async Task<Result> EliminarAreaDesperdicios(TokenData datosToken, FCAPRODCAT014Entity data)
        {
            try
            {
                return await new DesperdiciosData().EliminarAreaDesperdicios(datosToken, data);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
