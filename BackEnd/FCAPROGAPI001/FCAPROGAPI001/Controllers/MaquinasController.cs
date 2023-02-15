using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Entity;
using Entity.DTO.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Entity.DTO;

namespace FCAPROGAPI001.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class MaquinasController : ControllerBase
    {
        private readonly TokenData datosToken = new TokenData();
        public MaquinasController(IOptions<AppSettings> appSettings, IHttpContextAccessor httpContext)
        {
            datosToken.Conexion = httpContext.HttpContext.Items["Conexion"].ToString();
            datosToken.Usuario = httpContext.HttpContext.Items["UsuarioERP"].ToString();
            datosToken.Zona = httpContext.HttpContext.Items["Zona"].ToString();
        }

        [HttpGet("getListarMaquinas")]
        public async Task<IActionResult> ListarMaquinas(int startRow, int endRow, string ClaveMaquina)
        {
            try
            {
                return Ok(await new MaquinasBussines().ListarMaquinas(datosToken.Conexion, startRow, endRow,ClaveMaquina, datosToken.Zona));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }

        }

        [HttpGet("getListarDesperdicios")]
        public async Task<IActionResult> ListarDesperdicios(string ClaveMaquina)
        {
            try
            {
                return Ok(await new MaquinasBussines().ListarDesperdicios(datosToken.Conexion, ClaveMaquina));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }

        }
        [HttpGet("getListarProcesos")]
        public async Task<IActionResult> ListarProcesos()
        {
            try
            {
                return Ok(await new MaquinasBussines().ListarProcesos(datosToken.Conexion, datosToken.Zona));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }

        }

        [HttpGet("getListarEvaluaciones")]
        public async Task<IActionResult> ListarEvaluaciones()
        {
            try
            {
                return Ok(await new MaquinasBussines().ListarEvaluaciones(datosToken.Conexion, datosToken.Zona));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }

        }

        [HttpGet("getListarTripulaciones")]
        public async Task<IActionResult> ListarTripulaciones()
        {
            try
            {
                return Ok(await new MaquinasBussines().ListarTripulaciones(datosToken.Conexion, datosToken.Zona));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }

        }

        [HttpGet("getListarTripulacionesPorMaquina")]
        public async Task<IActionResult> ListarTripulacionesPorMaquina(string ClaveMaquina)
        {
            try
            {
                return Ok(await new MaquinasBussines().ListarTripulacionesPorMaquina(datosToken.Conexion,ClaveMaquina, datosToken.Zona));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }

        }

        [HttpGet("getCargaInformacionMaquina")]
        public async Task<IActionResult> CargaInformacionMaquina(string ClaveMaquina)
        {
            try
            {
                return Ok(await new MaquinasBussines().CargaInformacionMaquina(datosToken.Conexion, datosToken.Zona, ClaveMaquina));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }

        }

        [HttpGet("getListarPuestos")]
        public async Task<IActionResult> ListarPuestos(string ClaveMaquina, int startRow, int endRow)
        {
            try
            {
                return Ok(await new MaquinasBussines().ListarPuestos(datosToken.Conexion, ClaveMaquina,startRow,endRow));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }

        }

        [HttpPost("postGuardaMaquina")]
        public async Task<IActionResult> GuardaMaquina(guardaMaquina obj)
        {
            try
            {
                return Ok(await new MaquinasBussines().GuardaMaquina(datosToken.Conexion,datosToken.Usuario, obj));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }

        [HttpGet("getValidaMaquinaDefault")]
        public async Task<IActionResult> ValidaMaquinaDefault(string ClaveMaquina, int Proceso)
        {
            try
            {
                return Ok(await new MaquinasBussines().ValidaMaquinaDefault(datosToken.Conexion, ClaveMaquina,Proceso));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }

        }

        [HttpGet("getValidaMaquinaExiste")]
        public async Task<IActionResult> ValidaMaquinaExiste(string ClaveMaquina)
        {
            try
            {
                return Ok(await new MaquinasBussines().ValidaMaquinaExiste(datosToken.Conexion, ClaveMaquina));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }

        }

        [HttpPost("postEliminaMaquina")]
        public async Task<IActionResult> EliminaMaquina(ListadoMaquinas obj)
        {
            try
            {
                return Ok(await new MaquinasBussines().EliminaMaquina(datosToken.Conexion, obj.ClaveMaquina,datosToken.Usuario));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }

        }

        [HttpPost("postActivaMaquina")]
        public async Task<IActionResult> ActivaMaquina(Maquina obj)
        {
            try
            {
                return Ok(await new MaquinasBussines().ActivaMaquina(datosToken.Conexion, obj.ClaveMaquina, datosToken.Usuario));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }

        }
    }
}