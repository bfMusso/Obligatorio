using DTOs;
using LogicaAplicacion.CasosDeUso.CasosDeUsoTipoDeMovimiento;
using LogicaAplicacion.InterfacesCasosDeUso.Genericas;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDeMovimientoController : ControllerBase
    {

        public ICUListar<DTOTipoDeMovimiento> CUListar { get; set; }

        public ICUBuscarPorId<DTOTipoDeMovimiento> CUBuscarPorId { get; set; }

        public ICUAlta<DTOTipoDeMovimiento> CUAlta { get; set; }

        public ICUBaja<DTOTipoDeMovimiento> CUBaja { get; set; }

        public ICUActualizar<DTOTipoDeMovimiento> CUACtualizar { get; set; }

        public TipoDeMovimientoController(ICUListar<DTOTipoDeMovimiento> cUListar, ICUBuscarPorId<DTOTipoDeMovimiento> cUBuscarPorId, ICUAlta<DTOTipoDeMovimiento> cUAlta, ICUBaja<DTOTipoDeMovimiento> cUBaja, ICUActualizar<DTOTipoDeMovimiento> cUACtualizar)
        {
            CUListar = cUListar;
            CUBuscarPorId = cUBuscarPorId;
            CUAlta = cUAlta;
            CUBaja = cUBaja;
            CUACtualizar = cUACtualizar;
        }

        //api/TipoDeMovimientoController
        //Verbo: GET 
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<DTOTipoDeMovimiento> tiposDeMovimientos = CUListar.ObtenerListado();
                return Ok(tiposDeMovimientos);
            }
            catch (ExcepcionCustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch 
            {
                return StatusCode(500, "Error inesperado.");
            }
           
        }

        //api/TipoDeMovimientoController/5
        //Verbo: Delete
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //Si el Id es 0, se devuelve 400
            if (id <= 0)
            {
                return BadRequest("Elemento invalido.");
            }

            try
            {   
                //Se trae el objeto DTO con el buscar por Id
                DTOTipoDeMovimiento aBorrar = CUBuscarPorId.Buscar(id);
                if (aBorrar == null)
                {   
                    //Si es null, se devuelve 404
                    return NotFound("El Tipo de movimiento con el id" + id + "no existe");
                }
                //Si llega hasta aca, se elimina el objeto con el id y devuelve 201
                CUBaja.Baja(id);
                return NoContent();
            }
            catch (ExcepcionCustomException ex) 
            {   
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<TipoDeMovimientoController>
        [HttpPost]
        public IActionResult Post([FromBody] DTOTipoDeMovimiento DtoTipo)
        {
            if (DtoTipo == null)
            {
                //Se devuelve 400 con Falta informacion
                return BadRequest("Falta informacion para el Alta.");
            }
            if (DtoTipo.Id !=0) 
            {
                //Se devuelve 400 con id no debe proporcionarse
                return BadRequest("El id no debe proporcionarse, el mismo es autonumerico.");
            }

            try
            {   
                //Se agrega elemento
                CUAlta.Alta(DtoTipo);
                return CreatedAtRoute("BuscarPorId", new { id = DtoTipo.Id }, DtoTipo);
            }
            catch (ExcepcionCustomException ex)
            {
                //Mostramos error de la excepcion
                return BadRequest(ex.Message);
            }
            catch 
            {
                //Error generico
                return StatusCode(500, "Error inesperado");
            }
        }

        //api/TipoDeMovimientoController/5
        //Verbo: PUT 
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] DTOTipoDeMovimiento? DtoTipo)
        {
            if (DtoTipo == null)
            {
                //Se devuelve 400 con Falta informacion
                return BadRequest("Falta informacion para la operacion.");
            }

            if (id <= 0)
            {
                //Se devuelve 400 con id debe proporcionarse como mayor a 0
                return BadRequest("El id debe ser un valor positivo.");
            }

            if (id != DtoTipo.Id)
            {
                //Se devuelve 400 con id no debe ser un valor positivo
                return BadRequest("El id debe ser un valor positivo.");
            }

            try
            {
                //Se modifica, y se devuelve con Ok
                CUACtualizar.Actualizar(DtoTipo);
                return Ok(DtoTipo);
            }
            catch (ExcepcionCustomException ex)
            {
                //Se muestra escepcion custom
                return BadRequest(ex.Message);
            }
            catch {
                //Se deuvelve error inesperado con 500
                return StatusCode(500, "Error inesperado.");
            }


        }

        //api/TipoDeMovimientoController/5
        //Verbo: GET
        [HttpGet("{id}", Name = "BuscarPorId")]
        public IActionResult Get(int id)
        {
            try
            {
                //En caso de Id=0 se retorna BadRequest 400
                if (id <= 0)
                {
                    return BadRequest("El elemento ingresado no es correcto.");
                }
                //Se usa el casode uso para buscar el tipo de movimiento
                DTOTipoDeMovimiento tiposDeMovimiento = CUBuscarPorId.Buscar(id);

                //En caso de null se retorna NotFound 404
                if (tiposDeMovimiento == null)
                {
                    return NotFound("El Tipo de movimiento con el id" + id + "no existe");
                }

                //Si hay resultado se devuelve con un Ok
                return Ok(tiposDeMovimiento);
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
    }
}
