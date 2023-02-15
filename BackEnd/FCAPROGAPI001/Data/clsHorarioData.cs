using Dapper;
using Entity;
using Entity.DTO.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Entity.DTO;
using System;
using System.Data;

namespace Data
{
    public class clsHorarioData
    {
        public async Task<Result> GetMaquinas(TokenData datosToken)
        {
            var Result = new Result();
            using (var con = new SqlConnection(datosToken.Conexion))
            {
                var results = await con.QueryMultipleAsync("FCAPROGCAT006CWSPC1", new { Opcion = 1},
                    commandType: System.Data.CommandType.StoredProcedure);
                Result.data = await results.ReadAsync<clsMaquina>();
            }
            return Result;
        }

        public async Task<Result> GetHorario(TokenData datosToken, string ClaveMaquina)
        {
            var Result = new Result();
            using (var con = new SqlConnection(datosToken.Conexion))
            {
                var results = await con.QueryMultipleAsync("FCAPROGCAT006CWSPC1", new { Opcion = 2, ClaveMaquina},
                    commandType: System.Data.CommandType.StoredProcedure);
                Result.data = await results.ReadAsync<clsHorario>();
                Result.totalRecords = await results.ReadFirstAsync<int>();
            }
            return Result;
        }

        public async Task<clsHorario> Agregar(TokenData datosToken, clsHorario parHorario)
        {
            try
            {
                using (var con = new SqlConnection(datosToken.Conexion))
                {                   

                    con.Execute("FCAPROGCAT006CWSPA2",
                            new
                            {
                                Opcion = 1,
                                clavemaquina = parHorario.clavemaquina,
                                inicioprimero = parHorario.inicioprimero,
                                finprimero = parHorario.finprimero,
                                activoprimero = parHorario.activoprimero,
                                iniciosegundo = parHorario.iniciosegundo,
                                finsegundo = parHorario.finsegundo,
                                activosegundo = parHorario.activosegundo,
                                iniciotercero = parHorario.iniciotercero,
                                fintercero = parHorario.fintercero,
                                activotercero = parHorario.activotercero,
                                inicioprimerosabado = parHorario.inicioprimerosabado,
                                finprimerosabado = parHorario.finprimerosabado,
                                activoprimerosabado = parHorario.activoprimerosabado,
                                activoprimerodomingo = parHorario.activoprimerodomingo,
                                iniciosegundosabado = parHorario.iniciosegundosabado,
                                finsegundosabado = parHorario.finsegundosabado,
                                activosegundosabado = parHorario.activosegundosabado,
                                activosegundodomingo = parHorario.activosegundodomingo,
                                iniciotercerosabado = parHorario.iniciotercerosabado,
                                fintercerosabado = parHorario.fintercerosabado,
                                activotercerosabado = parHorario.activotercerosabado,
                                activotercerdomingo = parHorario.activotercerdomingo,
                                usuario = datosToken.Usuario
                            },
                            commandType: CommandType.StoredProcedure);

                    return parHorario;
                }
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
                using (var con = new SqlConnection(datosToken.Conexion))
                {
                    con.Execute("FCAPROGCAT006CWSPA2",
                            new
                            {
                                Opcion = 1,
                                clavemaquina = parHorario.clavemaquina,
                                inicioprimero = parHorario.inicioprimero,
                                finprimero = parHorario.finprimero,
                                activoprimero = parHorario.activoprimero,
                                iniciosegundo = parHorario.iniciosegundo,
                                finsegundo = parHorario.finsegundo,
                                activosegundo = parHorario.activosegundo,
                                iniciotercero = parHorario.iniciotercero,
                                fintercero = parHorario.fintercero,
                                activotercero = parHorario.activotercero,
                                inicioprimerosabado = parHorario.inicioprimerosabado,
                                finprimerosabado = parHorario.finprimerosabado,
                                activoprimerosabado = parHorario.activoprimerosabado,
                                activoprimerodomingo = parHorario.activoprimerodomingo,
                                iniciosegundosabado = parHorario.iniciosegundosabado,
                                finsegundosabado = parHorario.finsegundosabado,
                                activosegundosabado = parHorario.activosegundosabado,
                                activosegundodomingo = parHorario.activosegundodomingo,
                                iniciotercerosabado = parHorario.iniciotercerosabado,
                                fintercerosabado = parHorario.fintercerosabado,
                                activotercerosabado = parHorario.activotercerosabado,
                                activotercerdomingo = parHorario.activotercerdomingo,
                                usuario = datosToken.Usuario
                            },
                            commandType: CommandType.StoredProcedure);

                    return parHorario;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
