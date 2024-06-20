using DTOs;
using LogicaAplicacion.CasosDeUso.CasosDeUsoMovimientosDeStock;
using LogicaAplicacion.CasosDeUso.CasosDeUsoUsuario;
using LogicaAplicacion.CasosDeUso.CasosDeUsoValoresFijos;
using LogicaAplicacion.InterfacesCasosDeUso.Genericas;
using LogicaAplicacion.InterfacesCasosDeUso.Impuesto;
using LogicaAplicacion.InterfacesCasosDeUso.MovimientoDeStock;
using LogicaAplicacion.InterfacesCasosDeUso.Usuario;
using LogicaNegocio.Dominio;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Authorization;
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

        public ICUListarMovimientosDeStock<DTOMovimientoDeStock> CUListarMovimientosDeStock { get; set; }

        public ICUListarMovimientosYTipos<DTOMovimientoStockYTipoPaginado> CUBuscarMovimientosYTipos { get; set; }

        public ICUListarArticulosEnMovimientosEntreFechas<DTOArticulosPaginados> CUListarArticulosEnMovimientosEntreFechas { get; set; }

        public ICUCantidadMovimientosPorTipoYFecha<DTOCantidad> CUListarMovimientosPorTipoYFechas { get; set; }

        public ICUCantidadTotalMovimientos CUCantidadTotalMovimientos { get; set; }

       

        public ICUListarSimpleMOvimientoDeStockYTipo<DTOMovimientoStockYTipo> CUListarSimpleMovimientoDeStockYTipo { get; set; }
        public ICUObtenerTotalPaginas CUObtenerTotalPaginado { get; set; }

        public MovimientosDeStockController(ICUAltaMovimientoDeStock<DTOMovimientoDeStock> cUAltaMovimientoDeStock, ICUBuscarMovimientoPorId<DTOMovimientoDeStock> cUBuscarMovimientoPorId,
            ICUListarMovimientosYTipos<DTOMovimientoStockYTipoPaginado> cUBuscarMovimientosYTipos, ICUListarMovimientosDeStock<DTOMovimientoDeStock> cUListarMovimientosDeStock,
            ICUListarArticulosEnMovimientosEntreFechas<DTOArticulosPaginados> cUListarArticulosEnMovimientosEntreFechas, ICUCantidadMovimientosPorTipoYFecha<DTOCantidad> cUListarMovimientosPorTipoYFechas,
            ICUCantidadTotalMovimientos cUCantidadTotalMovimientos, ICUListarSimpleMOvimientoDeStockYTipo<DTOMovimientoStockYTipo> cUListarSimpleMovimientoDeStockYTipo, ICUObtenerTotalPaginas cUObtenerTotalPaginas)
        {
            CUAltaMovimientoDeStock = cUAltaMovimientoDeStock;
            CUBuscarMovimientoPorId = cUBuscarMovimientoPorId;
            CUListarMovimientosDeStock = cUListarMovimientosDeStock;
            CUBuscarMovimientosYTipos = cUBuscarMovimientosYTipos;
            CUListarArticulosEnMovimientosEntreFechas = cUListarArticulosEnMovimientosEntreFechas;
            CUListarMovimientosPorTipoYFechas = cUListarMovimientosPorTipoYFechas;
            CUCantidadTotalMovimientos = cUCantidadTotalMovimientos;           
            CUListarSimpleMovimientoDeStockYTipo = cUListarSimpleMovimientoDeStockYTipo;
            CUObtenerTotalPaginado = cUObtenerTotalPaginas;
        }


        [HttpGet("AltaMovimientosDeStock")]
        [Authorize(Roles = "Encargado")]
        public IActionResult GetAlta()
        {
            return Ok();
        }

        [HttpGet("CantidadDeMovimientosPorAnioYTipo")]
        //[Authorize]
        public IActionResult CantidadDeMovimientosPorAnioYTipo() {

            try
            {
                List<DTOCantidad> cantidad = CUListarMovimientosPorTipoYFechas.cantidadMovPorTipoyFecha();

                if (cantidad.Count <= 0)
                {
                    return BadRequest("No hay elementos que coincidan.");
                }

                return Ok(cantidad);
            }
            catch (ExcepcionCustomException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error inesperado: " + ex.Message);
            }

        }

        /*
        //
        // GET: api/<MovimientosDeStockController>
        [HttpGet("MovimientosDeStockYTipo/{pagina}")]
        //[Authorize]
        public IActionResult MovimientosDeStockYTipo(int pagina)
        {
            try
            {
                if (pagina == 0)
                {
                    return BadRequest("El número de página recibida no es correcta");
                }
                List<DTOMovimientoStockYTipo> DTOMovimientosDeStockYTipo = CUListarSimpleMovimientoDeStockYTipo.ListarMovimientosDeStockYTipo(pagina);
                return Ok(DTOMovimientosDeStockYTipo);
            }
            catch (ExcepcionCustomException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error inesperado: " + ex.Message);
            }

        }
        */



        // GET: api/<MovimientosDeStockController>
        [HttpGet("MovimientoPorTipo/{articuloId}/{tipoId}/{pagina}")]
        [Authorize]
        public IActionResult Get(int articuloId, int tipoId, int pagina)
        {
            try
            {
                if (pagina == 0)
                {
                    return BadRequest("El número de página recibida no es correcta");
                }
                DTOMovimientoStockYTipoPaginado DTOmovimientos = CUBuscarMovimientosYTipos.ListarMovimientosYTipos(articuloId, tipoId, pagina);

                if (DTOmovimientos.TotalElementos <= 0) {
                    return BadRequest("No hay elementos que coincidan.");
                }

                return Ok(DTOmovimientos);
            }
            catch (ExcepcionCustomException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error inesperado: " + ex.Message);
            }
        }

        // GET: api/<MovimientosDeStockController>

        [HttpGet("ArticulosEnMovimientosEntreFechas/{fecha1}/{fecha2}/{pagina}")]
        [Authorize]
        public IActionResult Get(DateTime fecha1, DateTime fecha2, int pagina)
        {
            try
            {


                if (fecha1 > fecha2)
                {
                    return BadRequest("La fecha inicial no puede ser mayor que la fecha final");
                }

                DTOArticulosPaginados DTOArticulos = CUListarArticulosEnMovimientosEntreFechas.ListarArticulosEntreFechas(fecha1, fecha2, pagina);

                if (DTOArticulos.Articulos.Count() <= 0)
                {
                    return BadRequest("No hay articulos en movimientos entre las fechas dadas.");
                }

                return Ok(DTOArticulos);
            }
            catch (ExcepcionCustomException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error inesperado: " + ex.Message);
            }
        }


        // GET: api/<MovimientosDeStockController>
        [HttpGet]
        [Authorize]
        public IActionResult ListarTodo()
        {
            try
            {
                List<DTOMovimientoDeStock> DTOMovimientosDeStocks = CUListarMovimientosDeStock.Listar();
                return Ok(DTOMovimientosDeStocks);
            }
            catch (ExcepcionCustomException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error inesperado: " + ex.Message);
            }

        }

        // GET api/<MovimientosDeStockController>/5
        [HttpGet("{id}", Name = "FindByIdMovimientoDeStock")]
        [Authorize]
        public IActionResult Get(int id)
        {
            try
            {
                //Controla si el Id es menor o igual a 0
                if (id <= 0)
                {
                    return BadRequest("El Id del objeto no es un Id valido.");
                }
                //Se usa el casode uso para buscar el movimiento por Id
                DTOMovimientoDeStock objDTOMovimiento = CUBuscarMovimientoPorId.Buscar(id);

                //En caso de que el objeto sea null se retorna 4004
                if (objDTOMovimiento == null)
                {
                    return BadRequest("No se encontro el elemento que busca.");
                }

                //Si hay resultado se devuelve con un Ok 
                return Ok(objDTOMovimiento);

            }
            catch (ExcepcionCustomException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(50, "Error inesperado.");
            }
        }

        // POST api/<MovimientosDeStockController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] DTOMovimientoDeStock dtoMovimiento)
        {
            try
            {
                //DTOUsuario usuario = CUBuscarUsuarioConMail.BuscarUsuarioConMail(dtoMovimiento.UsuarioDeMovimientoEmail);

                //dtoMovimiento.UsuarioDeMovimiento = usuario.Id; 

                if (dtoMovimiento == null)
                {
                    return BadRequest("Datos incorrectos");
                }
                if (dtoMovimiento.Id != 0)
                {
                    return BadRequest("El Id de nuevo elemento no puede ser distinto a 0.");
                }
                if (dtoMovimiento.ArticuloDeMovimientoId <= 0)
                {
                    return BadRequest("Problemas con el articulo, Id de articulo debe ser mayor a 0.");
                }
                if (dtoMovimiento.UsuarioDeMovimiento <= 0)
                {
                    return BadRequest("Problemas con el usuario, Id de usuario debe ser mayor a 0..");
                }

                CUAltaMovimientoDeStock.Alta(dtoMovimiento);
                return CreatedAtRoute("FindByIdMovimientoDeStock", new { id = dtoMovimiento.Id }, dtoMovimiento);
            }
            catch (ExcepcionCustomException ex)
            {

                return NotFound(ex.Message);

            }
            catch (Exception ex)
            {
                // Log the exception details for debugging
                // Logger.LogError(ex, "Unexpected error occurred while creating MovimientoDeStock.");
                return StatusCode(500, $"Error inesperado, no se pudo realizar el alta. Detalles: {ex.Message}");

            }
        }


        [HttpGet("CantidadDePaginas")]

        public IActionResult CantidadTotalDePaginas()
        {
            try
            {
                return Ok((double)CUCantidadTotalMovimientos.CantidadTotalMovimientos() / CUObtenerTotalPaginado.ObtenerPaginado());
            }
            catch (ExcepcionCustomException ex) {
                return BadRequest(ex.Message);
            }
            catch {

                return StatusCode(500, "Error inesperado");
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
