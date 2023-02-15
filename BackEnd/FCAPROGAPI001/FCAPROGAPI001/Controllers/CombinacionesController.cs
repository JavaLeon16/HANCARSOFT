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
    public class CombinacionesController : ControllerBase
    {
        private readonly TokenData datosToken = new TokenData();
        public CombinacionesController(IOptions<AppSettings> appSettings, IHttpContextAccessor httpContext)
        {
            datosToken.Conexion = httpContext.HttpContext.Items["Conexion"].ToString();
            datosToken.Usuario = httpContext.HttpContext.Items["UsuarioERP"].ToString();
        }

        [HttpGet("GetCombinaciones")]
        public async Task<IActionResult> GetCombinaciones(int startRow, int endRow, string filtro)
        {
            try
            {
                return Ok(await new CombinacionesBusiness().GetTipoCaja(datosToken.Conexion, startRow, endRow, filtro));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }

        [HttpGet("GetPapel")]
        public async Task<IActionResult> GetPapel(int startRow, int endRow, string filtro, string TipoPapel)
        {
            try
            {
                return Ok(await new CombinacionesBusiness().GetPapel(datosToken.Conexion, startRow, endRow, filtro, TipoPapel));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }
        [HttpPost("Agregar")]
        public async Task<IActionResult> Agregar(FCAPROGCAT004Entity data)
        {
            try
            {
                return Ok(await new CombinacionesBusiness().Agregar(datosToken, data));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }
        [HttpPost("Editar")]
        public async Task<IActionResult> Editar(FCAPROGCAT004Entity data)
        {
            try
            {
                return Ok(await new CombinacionesBusiness().Editar(datosToken, data));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }
        [HttpPost("Eliminar")]
        public async Task<IActionResult> Elimitar(FCAPROGCAT004Entity data)
        {
            try
            {
                return Ok(await new CombinacionesBusiness().Eliminar(datosToken, data));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }

    }
}