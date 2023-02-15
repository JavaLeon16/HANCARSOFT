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
    public class TipoCajaController : ControllerBase
    {
        private readonly TokenData datosToken = new TokenData();
        public TipoCajaController(IOptions<AppSettings> appSettings, IHttpContextAccessor httpContext)
        {
            // Data Source=172.16.2.28;Initial Catalog=FcaCajas01;User id=sa;Password=desarrollo2008
            // 001000
            datosToken.Conexion = httpContext.HttpContext.Items["Conexion"].ToString();
            datosToken.Usuario = httpContext.HttpContext.Items["UsuarioERP"].ToString();
            //datosToken.Conexion = "Data Source=172.16.2.28;Initial Catalog=FcaCajas01;User id=sa;Password=desarrollo2008";
            //datosToken.Usuario = "001000";
        }

        [HttpGet("getTipoCaja")]
        public async Task<IActionResult> GetTipoCaja(int startRow, int endRow, string filtro)
        {
            try
            {
                return Ok(await new TipoCajaBusiness().GetTipoCaja(datosToken.Conexion, startRow, endRow, filtro));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }

        }
        [HttpPost("Agregar")]
        public async Task<IActionResult> Agregar(FCAPROGCAT010Entity tipocaja)
        {
            try
            {
                return Ok(await new TipoCajaBusiness().Agregar(datosToken, tipocaja));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }
        [HttpPost("Editar")]
        public async Task<IActionResult> Editar(FCAPROGCAT010Entity tipocaja)
        {
            try
            {
                return Ok(await new TipoCajaBusiness().Editar(datosToken, tipocaja));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }
        [HttpPost("Eliminar")]
        public async Task<IActionResult> Elimitar(FCAPROGCAT010Entity tipocaja)
        {
            try
            {
                return Ok(await new TipoCajaBusiness().Eliminar(datosToken, tipocaja));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }
    }
}