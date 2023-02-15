using System;
using System.Threading.Tasks;
using Business;
using Entity;
using Entity.DTO.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace FCAPROGAPI001.Controllers
{
    [Authorize]
    [Route("/[controller]")]
    [ApiController]

    public class VarEntornoController : ControllerBase
    {
        private readonly TokenData datosToken = new TokenData();
        public VarEntornoController(IHttpContextAccessor httpContext)
        {
            datosToken.Conexion = httpContext.HttpContext.Items["Conexion"].ToString();
            datosToken.Usuario = httpContext.HttpContext.Items["UsuarioERP"].ToString();
        }

        [HttpGet("getVarEntorno")]
        public async Task<IActionResult> getVarEntorno()
        {
            try
            {
                return Ok(await new VarEntornoBusiness().getVarEntorno(datosToken.Conexion));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }

        [HttpPost("Editar")]
        public async Task<IActionResult> ModificarVariables(FCAPROG010MWEntity Variables)
        {
            try
            {
                return Ok(await new VarEntornoBusiness().ModificarVariables(datosToken, Variables));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }
    }


}