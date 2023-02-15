using System;
using System.Threading.Tasks;
using Business;
using Entity.DTO.Common;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FCAPROGAPI001.Controllers
{
    [Authorize]
    [Route("/[controller]")]
    [ApiController]
    public class HorarioController : ControllerBase
    {
        private readonly AppSettings appSettings;
        private readonly IHttpContextAccessor _httpContext;
        private readonly TokenData datosToken = new TokenData();

        public HorarioController(IOptions<AppSettings> AppSettings, IHttpContextAccessor httpContext)
        {
            appSettings = AppSettings.Value;

            _httpContext = httpContext;

            datosToken.Conexion = httpContext.HttpContext.Items["Conexion"].ToString();
            datosToken.Usuario = httpContext.HttpContext.Items["UsuarioERP"].ToString();
        }

        [HttpGet("getMaquinas")]
        public async Task<IActionResult> GetMaquinas()
        {
            try
            {
                return Ok(await new clsHorarioBusiness().GetMaquinas(datosToken));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }

        }

        [HttpGet("getHorario")]
        public async Task<IActionResult> GetHorario(string ClaveMaquina)
        {
            try
            {
                return Ok(await new clsHorarioBusiness().GetHorario(datosToken,  ClaveMaquina));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }

        }


        [HttpPost("Agregar")]
        public async Task<IActionResult> Agregar(clsHorario parHorario)
        {
            try
            {

                return Ok(await new clsHorarioBusiness().Agregar(datosToken, parHorario));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Modificar")]
        public async Task<IActionResult> Modificar(clsHorario parHorario)
        {
            try
            {
                return Ok(await new clsHorarioBusiness().Modificar(datosToken, parHorario));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
