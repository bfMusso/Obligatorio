using DTOs;
using LogicaAplicacion.InterfacesCasosDeUso.Genericas;
using LogicaAplicacion.InterfacesCasosDeUso.MovimientoDeStock;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosDeStockController : ControllerBase
    {

        public ICUAltaMovimientoDeStock<DTOMovimientoDeStock> CUAltaMovimientoDeStock { get; set; }

        public ICUBuscarMovimientoPorId<DTOMovimientoDeStock> CUBuscarMovimientoPorId { get; set; }

        public MovimientosDeStockController(ICUAltaMovimientoDeStock<DTOMovimientoDeStock> cUAltaMovimientoDeStock, ICUBuscarMovimientoPorId<DTOMovimientoDeStock> cUBuscarMovimientoPorId)
        {
            CUAltaMovimientoDeStock = cUAltaMovimientoDeStock;
            CUBuscarMovimientoPorId = cUBuscarMovimientoPorId;
        }



        // GET: api/<MovimientosDeStockController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MovimientosDeStockController>/5
        [HttpGet("{id}", Name = "FindByIdMovimientoDeStock")]

        public IActionResult Get(int id)
        {
            try
            {
                //Controla si el Id es 
                if (id == 0)
                {
                    return BadRequest("El Id del objeto no es un Id valido.");
                }
                DTOMovimientoDeStock objDTOMovimiento = CUBuscarMovimientoPorId.Buscar(id);
                if (objDTOMovimiento == null)
                {
                    return BadRequest("No se encontro el elemento que busca.");
                }
                return Ok(objDTOMovimiento);

            }
            catch (ExcepcionCustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(50, "Error inesperado.");
            }
        }

        // POST api/<MovimientosDeStockController>
        [HttpPost]
        public IActionResult Post([FromBody] DTOMovimientoDeStock dtoMovimiento)
        {
            try
            {
                if (dtoMovimiento == null)
                {
                    return BadRequest("Datos incorrectos");
                }
                if (dtoMovimiento.Id != 0)
                {
                    return BadRequest("Problemas con el usuario.");
                }
                if (dtoMovimiento.ArticuloDeMovimiento != 0)
                {
                    return BadRequest("Problemas con el usuario.");
                }
                if (dtoMovimiento.UsuarioDeMovimiento != 0)
                {
                    return BadRequest("Problemas con el usuario.");
                }

                CUAltaMovimientoDeStock.Alta(dtoMovimiento);
                return CreatedAtRoute("FindByIdMovimientoDeStock", new { id = dtoMovimiento.Id }, dtoMovimiento);
            }
            catch (ExcepcionCustomException ex)
            {

                return BadRequest(ex.Message);

            }
            catch
            {
                return StatusCode(500, "Error inesperado, no se pudo realizar el alta...");

            }
        }

        // PUT api/<MovimientosDeStockController>/5
        /*
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
        */


        // DELETE api/<MovimientosDeStockController>/5
        /*
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */

    }
}
