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
    [Authorize]
    [Route("/[controller]")]
    [ApiController]
    public class FCAPROG019MWController : Controller
    {
        private readonly TokenData datosToken = new TokenData();
        public FCAPROG019MWController(IOptions<AppSettings> appSettings, IHttpContextAccessor httpContext)
        {
            datosToken.Conexion = httpContext.HttpContext.Items["Conexion"].ToString();
            datosToken.Usuario = httpContext.HttpContext.Items["UsuarioERP"].ToString();
            datosToken.Zona = httpContext.HttpContext.Items["Zona"].ToString();
        }

        [HttpGet("leeHoraLocal")]
        public async Task<IActionResult> leeHoraLocal()
        {
            try
            {
                return Ok(await new FCAPROG019MWBusiness().leeHoraLocal(datosToken));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }

        [HttpGet("buscaPrograma")]
        public async Task<IActionResult> buscaPrograma(string programa)
        {
            try
            {
                return Ok(await new FCAPROG019MWBusiness().buscaPrograma(datosToken, programa));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }

        [HttpGet("buscaProduccion")]
        public async Task<IActionResult> buscaProduccion(string programa)
        {
            try
            {
                return Ok(await new FCAPROG019MWBusiness().buscaProduccion(datosToken, programa));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }

        [HttpGet("buscaTripulacionMaquina")]
        public async Task<IActionResult> buscaTripulacionMaquina(string claveMaquina)
        {
            try
            {
                return Ok(await new FCAPROG019MWBusiness().buscaTripulacionMaquina(datosToken, claveMaquina));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }

        [HttpGet("leePrograma")]
        public async Task<IActionResult> leePrograma(string fechaAnterior, string programa, string claveMaquina, string turno)
        {
            try
            {
                return Ok(await new FCAPROG019MWBusiness().leePrograma(datosToken, fechaAnterior, programa, claveMaquina, turno));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }

        [HttpGet("buscaSupervisor")]
        public async Task<IActionResult> buscaSupervisor()
        {
            try
            {
                return Ok(await new FCAPROG019MWBusiness().buscaSupervisor(datosToken));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }

        [HttpGet("buscaParafina")]
        public async Task<IActionResult> buscaParafina()
        {
            try
            {
                return Ok(await new FCAPROG019MWBusiness().buscaParafina(datosToken));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }

        [HttpGet("validarGuardado")]
        public async Task<IActionResult> validarGuardado(string programa, string claveMaquina, string turno, string fecha)
        {
            try
            {
                return Ok(await new FCAPROG019MWBusiness().validarGuardado(datosToken, programa, claveMaquina, turno, fecha));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }

        [HttpGet("guardar")]
        public async Task<IActionResult> guardar(
            string fecha, string horaIni, string horaFin, string turno, string supervisor, string minutos, string despCorrguradora,
            string despImpresora, string despAcabados, string fechaNow, string parafina, string pesoLamina, string pesoCaja, string retrabajo,
            string actCantidad, string idTripulacion, string programa, string claveMaquina, string wFechaAnterior, string idUnico
        )
        {
            try
            {
                return Ok(await new FCAPROG019MWBusiness().guardar(datosToken,
                    fecha, horaIni, horaFin, turno, supervisor, minutos, despCorrguradora,
                    despImpresora, despAcabados, fechaNow, parafina, pesoLamina, pesoCaja, retrabajo,
                    actCantidad, idTripulacion, programa, claveMaquina, wFechaAnterior, idUnico
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }

        // METODOS DE PAGINA L

        [HttpGet("cargaComboMaquinas")]
        public async Task<IActionResult> cargaComboMaquinas()
        {
            try
            {
                return Ok(await new FCAPROG019MWBusiness().cargaComboMaquinas(datosToken));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }

        [HttpGet("buscaProgramas")]
        public async Task<IActionResult> buscaProgramas(string fecha, string fechaF, string turno, string claveMaquina, string sinFechaProd)
        {
            try
            {
                return Ok(await new FCAPROG019MWBusiness().buscaProgramas(datosToken, fecha, fechaF, turno, claveMaquina, sinFechaProd));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }

        [HttpPost("actualizaSupTrip")]
        public async Task<IActionResult> actualizaSupTrip(programasSeleccionadosL datos)
        {
            try
            {
                return Ok(await new FCAPROG019MWBusiness().actualizaSupTrip(datosToken, datos));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error, {ex.Message}");
            }
        }
    }
}
