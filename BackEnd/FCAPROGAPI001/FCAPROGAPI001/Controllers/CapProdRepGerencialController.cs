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
    // Creo: Manuel Valenzuela, 27/ene/2022

    [Route("/[controller]")]
    [ApiController]
    public class CapProdRepGerencialController : ControllerBase
    {

        private readonly TokenData datosToken = new TokenData();
        //private readonly string SP = "CmoSp372";
        public CapProdRepGerencialController(IOptions<AppSettings> appSettings, IHttpContextAccessor httpContext)
        {

            datosToken.Conexion = httpContext.HttpContext.Items["Conexion"].ToString();
            datosToken.Usuario = httpContext.HttpContext.Items["UsuarioERP"].ToString();
            //datosToken.Conexion = "Data Source=172.16.2.235;Initial Catalog=Cajas" + datosToken.Zona + ";User id=sa;Password=desarrollo2008";
            //datosToken.Conexion = "Data Source=172.16.2.28;Initial Catalog=FcaCajas01;User id=sa;Password=desarrollo2008";
            //datosToken.Usuario = "001000";
        }

        [HttpGet("GetCargaMaquinas")]
        public async Task<IActionResult> GetCargaMaquinas(int startRow, int endRow, string filtro = null)
        {
            try
            {
                return Ok(await new CapProdRepGerencialBusiness().GetCargaMaquinas(datosToken.Conexion, startRow, endRow));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }

        }

        [HttpPost("InsertCargaMaquinas")]
        public async Task<IActionResult> InsertCargaMaquinas(FCAPROGDAT023Entity capRepProdGerencial)
        {
            try
            {
                return Ok(await new CapProdRepGerencialBusiness().InsertCargaMaquinas(datosToken, capRepProdGerencial));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }

    }
}