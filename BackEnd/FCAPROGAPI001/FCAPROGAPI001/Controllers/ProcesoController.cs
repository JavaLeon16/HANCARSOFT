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

namespace FCAPROGAPI001.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ProcesoController : ControllerBase
    {
        private readonly TokenData datosToken = new TokenData();
        public ProcesoController(IOptions<AppSettings> appSettings, IHttpContextAccessor httpContext)
        {
            datosToken.Conexion = httpContext.HttpContext.Items["Conexion"].ToString();
            datosToken.Usuario = httpContext.HttpContext.Items["UsuarioERP"].ToString();
        }

        [HttpGet("getProceso")]
        public async Task<IActionResult> GetProceso(int startRow, int endRow, string filtro, string TipoMaquina , int Estatus)
        {
            try
            {
                return Ok(await new ProcesoBusiness().GetProceso(datosToken.Conexion, startRow, endRow, filtro, TipoMaquina, Estatus));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }

        }
        [HttpGet("GetProcesoCosto")]
        public async Task<IActionResult> GetProcesoPorTipoMaquina(string TipoMaquina)
        {
            try
            {
                return Ok(await new ProcesoBusiness().GetProcesoPorTipoMaquina(datosToken.Conexion, TipoMaquina));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }

        }
        [HttpGet("GetTipoMaquina")]
        public async Task<IActionResult> GetTipoMaquina()
        {
            try
            {
                return Ok(await new ProcesoBusiness().GetTipoMaquina(datosToken.Conexion));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }

        }
        [HttpPost("Agregar")]
        public async Task<IActionResult> Agregar(FCAPROGCAT009Entity proceso)
        {
            try
            {
                return Ok(await new ProcesoBusiness().Agregar(datosToken, proceso));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }
        [HttpPost("Editar")]
        public async Task<IActionResult> Editar(FCAPROGCAT009Entity proceso)
        {
            try
            {
                return Ok(await new ProcesoBusiness().Editar(datosToken, proceso));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }
        [HttpPost("Estatus")]
        public async Task<IActionResult> Estatus(FCAPROGCAT009Entity proceso)
        {
            try
            {
                return Ok(await new ProcesoBusiness().Estatus(datosToken, proceso));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }
    }
}