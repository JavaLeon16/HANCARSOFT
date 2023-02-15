using Data;
using Entity.DTO;
using Entity.DTO.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Business
{
    public class MaquinasBussines
    {
        public Task<Result> ListarMaquinas(string strConexion, int startRow, int endRow, string ClaveMaquina, string zona)
        {
            return new MaquinasData().ListarMaquinas(strConexion, startRow, endRow,ClaveMaquina, zona);
        }
        public Task<Result> ListarDesperdicios(string strConexion, string ClaveMaquina)
        {
            return new MaquinasData().ListarDesperdicios(strConexion, ClaveMaquina);
        }
        public Task<Result> ListarProcesos(string strConexion, string zona)        {
            return new MaquinasData().ListarProcesos(strConexion, zona);
        }

        public Task<Result> ListarEvaluaciones(string strConexion, string zona)
        {
            return new MaquinasData().ListarEvaluaciones(strConexion, zona);
        }

        public Task<Result> ListarTripulaciones(string strConexion, string zona)
        {
            return new MaquinasData().ListarTripulaciones(strConexion, zona);
        }

        public Task<Result> ListarTripulacionesPorMaquina(string strConexion, string ClaveMaquina, string zona)
        {
            return new MaquinasData().ListarTripulacionesPorMaquina(strConexion,ClaveMaquina, zona);
        }
        public Task<Result> CargaInformacionMaquina(string strConexion, string zona, string ClaveMaquina)
        {
            return new MaquinasData().CargaInformacionMaquina(strConexion, zona, ClaveMaquina);
        }
        public Task<Result> ListarPuestos(string strConexion, string ClaveMaquina,int startRow, int endRow)
        {
            return new MaquinasData().ListarPuestos(strConexion, ClaveMaquina,startRow,endRow);
        }
        public Task<Result> GuardaMaquina(string strConexion, string UsuarioERP, guardaMaquina obj)
        {
            return new MaquinasData().GuardaMaquina(strConexion, UsuarioERP,obj);
        }
        public Task<Result> ValidaMaquinaDefault(string strConexion, string ClaveMaquina, int Proceso)
        {
            return new MaquinasData().ValidaMaquinaDefault(strConexion, ClaveMaquina,Proceso);
        }
        public Task<Result> ValidaMaquinaExiste(string strConexion, string ClaveMaquina)
        {
            return new MaquinasData().ValidaMaquinaExiste(strConexion, ClaveMaquina);
        }
        public Task<Result> EliminaMaquina(string strConexion, string ClaveMaquina,string UsuarioERP)
        {
            return new MaquinasData().EliminaMaquina(strConexion, ClaveMaquina,UsuarioERP);
        }

        public Task<Result> ActivaMaquina(string strConexion, string ClaveMaquina, string UsuarioERP)
        {
            return new MaquinasData().ActivaMaquina(strConexion, ClaveMaquina, UsuarioERP);
        }
    }
}
