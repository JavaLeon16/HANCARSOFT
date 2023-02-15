using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Entity;
using Entity.DTO;
using Entity.DTO.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FCAPROGAPI001.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class DesperdiciosController : ControllerBase
    {
        private readonly TokenData datosToken = new TokenData();
        public DesperdiciosController(IOptions<AppSettings> appSettings, IHttpContextAccessor httpContext)
        {
           
            datosToken.Usuario = httpContext.HttpContext.Items["UsuarioERP"].ToString();
            datosToken.Zona = httpContext.HttpContext.Items["Zona"].ToString();

            if (datosToken.Zona == "01")
            {
                datosToken.Conexion = httpContext.HttpContext.Items["Conexion"].ToString().Replace("FcaCajas01", "FcaCajas");
            } else if (datosToken.Zona == "02")
            {
                datosToken.Conexion = httpContext.HttpContext.Items["Conexion"].ToString().Replace("FcaCajas02", "FcaCajas");
            } else
            {
                datosToken.Conexion = httpContext.HttpContext.Items["Conexion"].ToString().Replace("FcaCajas05", "FcaCajas");
            }
        }
        [HttpGet("getDesperdicios")]
        public async Task<IActionResult> GetDesperdicios(int startRow, int endRow, string DescripcionDesperdicio, bool AplicaImpresora, bool AplicaCorrugadora, bool AplicaAcabado, bool AplicaRecuperacionCaja)
        {
            try
            {
                return Ok(await new DesperdiciosBusiness().GetDesperdicios(datosToken, startRow, endRow, DescripcionDesperdicio, AplicaImpresora, AplicaCorrugadora, AplicaAcabado, AplicaRecuperacionCaja));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }

        }
        [HttpGet("getConfigAreaDesperdicios")]
        public async Task<IActionResult> GetConfigAreaDesperdicios(int startRow, int endRow, int ClaveDesperdicio)
        {
            try
            {
                return Ok(await new DesperdiciosBusiness().GetConfigAreaDesperdicios(datosToken, startRow, endRow, ClaveDesperdicio ));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }

        }
        [HttpGet("getListaDesperdicio")]
        public async Task<IActionResult> GetListaDesperdicio()
        {
            try
            {
                return Ok(await new DesperdiciosBusiness().GetListaDesperdicio(datosToken));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }

        }
        [HttpGet("getListaArea")]
        public async Task<IActionResult> GetListaArea(bool EsAreaCaptura)
        {
            try
            {
                return Ok(await new DesperdiciosBusiness().GetListaArea(datosToken , EsAreaCaptura));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }

        }
        [HttpPost("Agregar")]
        public async Task<IActionResult> Agregar(FCAPRODCAT016Entity data)
        {
            try
            {
                return Ok(await new DesperdiciosBusiness().Agregar(datosToken, data));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }
        [HttpPost("Editar")]
        public async Task<IActionResult> Editar(FCAPRODCAT016Entity data)
        {
            try
            {
                return Ok(await new DesperdiciosBusiness().Editar(datosToken, data));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }
        [HttpPost("Eliminar")]
        public async Task<IActionResult> Elimitar(FCAPRODCAT016Entity data)
        {
            try
            {
                return Ok(await new DesperdiciosBusiness().Eliminar(datosToken, data));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }
        [HttpGet("getAreaDesperdicios")]
        public async Task<IActionResult> GetAreaDesperdicios(int startRow, int endRow, string Desperdicio)
        {
            try
            {
                return Ok(await new DesperdiciosBusiness().GetAreaDesperdicios(datosToken, startRow, endRow, Desperdicio));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }

        }
        [HttpPost("AgregarAreaDesperdicios")]
        public async Task<IActionResult> AgregarAreaDesperdicios(FCAPRODCAT014Entity data)
        {
            try
            {
                return Ok(await new DesperdiciosBusiness().AgregarAreaDesperdicios(datosToken, data));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }
        [HttpPost("EditarAreaDesperdicios")]
        public async Task<IActionResult> EditarAreaDesperdicios(FCAPRODCAT014Entity data)
        {
            try
            {
                return Ok(await new DesperdiciosBusiness().EditarAreaDesperdicios(datosToken, data));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }
        [HttpPost("EliminarAreaDesperdicios")]
        public async Task<IActionResult> EliminarAreaDesperdicios(FCAPRODCAT014Entity data)
        {
            try
            {
                return Ok(await new DesperdiciosBusiness().EliminarAreaDesperdicios(datosToken, data));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }

    }
}