using Business;
using Entity;
using Entity.DTO.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace FCAPROGAPI001.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class RepConsumoRollosCorrugadoraController : ControllerBase
    {

        private readonly TokenData datosToken = new TokenData();

        public RepConsumoRollosCorrugadoraController(IOptions<AppSettings> appSettings, IHttpContextAccessor httpContext)
        {
            datosToken.Conexion = httpContext.HttpContext.Items["Conexion"].ToString();
            datosToken.Usuario = httpContext.HttpContext.Items["UsuarioERP"].ToString();
            datosToken.Zona = httpContext.HttpContext.Items["Zona"].ToString();

            //datosToken.Conexion = "Data Source=172.16.2.235;Initial Catalog=CecsoPlan01;User id=sa;Password=Sql-Desarrollo";
            //datosToken.Conexion = "Data Source=172.16.2.28;Initial Catalog=FcaCajas01;User id=sa;Password=desarrollo2008";
            //datosToken.Usuario = "001000";
            //datosToken.Zona = zona;
        }

        [HttpGet("GetClases")]
        public async Task<IActionResult> GetClases()
        {
            try
            {
                return Ok(await new RepConsumoRollosCorrugadoraBusiness().GetClases(datosToken.Conexion));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }

        }

        [HttpGet("GetSubClases")]
        public async Task<IActionResult> GetSubClases()
        {
            try
            {
                return Ok(await new RepConsumoRollosCorrugadoraBusiness().GetSubClases(datosToken.Conexion));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }

        }


        [HttpGet("GetTripulaciones")]
        public async Task<IActionResult> GetTripulaciones()
        {
            try
            {
                return Ok(await new RepConsumoRollosCorrugadoraBusiness().GetTripulaciones(datosToken.Conexion));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }

        }

        [HttpPost("GetDatosConsumoRollos")]
        public async Task<IActionResult> GetDatosConsumoRollos(FCAPROG001RWParametrosEntity modelo)
        {
            try
            {
                return Ok(await new RepConsumoRollosCorrugadoraBusiness().GetDatosConsumoRollos(datosToken.Conexion, modelo, datosToken.Zona));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }

        }

    }
}
