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
    public class ParosController : ControllerBase
    {
        private readonly TokenData datosToken = new TokenData();
        public ParosController(IOptions<AppSettings> appSettings, IHttpContextAccessor httpContext)
        {
            datosToken.Conexion = httpContext.HttpContext.Items["Conexion"].ToString();
            datosToken.Usuario = httpContext.HttpContext.Items["UsuarioERP"].ToString();
        }

        [HttpGet("getParos")]
        public async Task<IActionResult> GetParos(int startRow, int endRow, string filtro, int Estatus)
        {
            try
            {
                return Ok(await new ParosBusiness().GetParos(datosToken.Conexion, startRow, endRow, filtro ,Estatus));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }

        }
        [HttpPost("Agregar")]
        public async Task<IActionResult> Agregar(FCAPRODCAT012Entity Paro)
        {
            try
            {
                return Ok(await new ParosBusiness().Agregar(datosToken, Paro));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }
        [HttpPost("Editar")]
        public async Task<IActionResult> Editar(FCAPRODCAT012Entity Paro)
        {
            try
            {
                return Ok(await new ParosBusiness().Editar(datosToken, Paro));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }
        [HttpPost("Eliminar")]
        public async Task<IActionResult> Elimitar(FCAPRODCAT012Entity Paro)
        {
            try
            {
                return Ok(await new ParosBusiness().Eliminar(datosToken, Paro));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }
    }
}