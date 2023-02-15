using Business;
using Entity;
using Entity.DTO.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCAPROGAPI001.Controllers
{

    // Creo: Manuel Valenzuela, 10/marzo/2022
    //[Authorize]
    [Route("/[controller]")]
    [ApiController]
    public class ReporteArtMedidasController : Controller
    {

        private readonly TokenData datosToken = new TokenData();
        public ReporteArtMedidasController(IOptions<AppSettings> appSettings, IHttpContextAccessor httpContext)
        {

            datosToken.Conexion = httpContext.HttpContext.Items["Conexion"].ToString();
            datosToken.Usuario = httpContext.HttpContext.Items["UsuarioERP"].ToString();
            datosToken.Zona = httpContext.HttpContext.Items["Zona"].ToString();

            //datosToken.Conexion = "Data Source=172.16.2.235;Initial Catalog=Cajas01;User id=sa;Password=Sql-Desarrollo";
            //datosToken.Conexion = "Data Source=172.16.2.28;Initial Catalog=FcaCajas01;User id=sa;Password=desarrollo2008";
            //datosToken.Usuario = "001000";
            //datosToken.Zona = zona;
        }

        [HttpGet("GetDataTipoIndustria")]
        public async Task<IActionResult> GetDataTipoIndustria()
        {
            try
            {
                return Ok(await new ReporteArtMedidasBusiness().GetDataTipoIndustria(datosToken.Conexion));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }

        }

        [HttpPost("GetDataArticulosMaster")]
        public async Task<IActionResult> GetDataArticulosMaster(FCAPROG002RWParametrosSPEntity modelo)
        {
            try
            {
                return Ok(await new ReporteArtMedidasBusiness().GetDataArticulosMaster(datosToken.Conexion, modelo, datosToken.Zona));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }

        }


        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
