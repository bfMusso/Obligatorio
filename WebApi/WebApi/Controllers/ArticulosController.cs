using DTOs;
using LogicaAplicacion.InterfacesCasosDeUso.Genericas;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        public ICUListar<DTOListarArticulos> CUListado { get; set; }

        public ArticulosController(ICUListar<DTOListarArticulos> cUListado)
        {
            CUListado = cUListado;
        }

        //api/Articulos
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<DTOListarArticulos> DTOArticulos = CUListado.ObtenerListado();
                return Ok(DTOArticulos);
            }
            catch (ExcepcionCustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex) {
                return StatusCode(500, "Error inesperado: " + ex.Message);
            }
           
        }
    }
}
