using System;
using System.Threading.Tasks;
using Business;
using Entity;
using Entity.DTO.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FCAPROGAPI002.Controllers
{
    [Authorize]
    [Route("/[controller]")]
    [ApiController]
    public class FlautasController : ControllerBase
    {
        private readonly TokenData datosToken = new TokenData();
        public FlautasController(IHttpContextAccessor httpContext)
        {
            datosToken.Conexion = httpContext.HttpContext.Items["Conexion"].ToString();
            datosToken.Usuario = httpContext.HttpContext.Items["UsuarioERP"].ToString();
        }

        [HttpGet("getFlautas")]
        public async Task<IActionResult> GetFlautas(int startRow, int endRow, string filtro)
        {
            try
            {
                return Ok(await new FlautasBusiness().GetFlautas(datosToken.Conexion, startRow, endRow,filtro));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }
        [HttpGet("getCorrugados")]
        public async Task<IActionResult> GetCorrugados()
        {
            try
            {
                return Ok(await new FlautasBusiness().GetCorrugados(datosToken.Conexion));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }
        [HttpPost("GuardarFlauta")]
        public async Task<IActionResult> GuardarFlauta(FlautasEntity obj)
        {
            try
            {
                return Ok(await new FlautasBusiness().GuardarFlautas(datosToken, obj));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }
        [HttpPost("ModificarFlautas")]
        public async Task<IActionResult> ModificarFlautas(FlautasEntity obj)
        {
            try
            {
                return Ok(await new FlautasBusiness().ModificarFlautas(datosToken, obj));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }
        [HttpPost("EliminarFlautas")]
        public async Task<IActionResult> EliminarFlautas(FlautasEntity obj)
        {
            try
            {
                return Ok(await new FlautasBusiness().EliminarFlautas(datosToken, obj));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }
    }
}
