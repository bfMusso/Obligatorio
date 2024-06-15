using DTOs;
using LogicaAplicacion.InterfacesCasosDeUso.Usuario;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Mvc;
using WebApi.Token;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        public ICULogin<DTOUsuario> CULoginUsuario { get; set; }

        public UsuariosController(ICULogin<DTOUsuario> cULoginUsuario) 
        {
            CULoginUsuario = cULoginUsuario;
        }

        // GET: api/<UsuariosController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UsuariosController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsuariosController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UsuariosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsuariosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody]DTOUsuario DtoUsuario)
        {
            try
            {
                if(DtoUsuario == null)
                {
                    return BadRequest("Datos incorrectos.");
                }
                DtoUsuario = CULoginUsuario.Login(DtoUsuario.Email, DtoUsuario.Password);
                if(DtoUsuario == null)
                {
                    return NotFound("Datos incorrectos");
                }
                return Ok(new DTOUsuarioLogueado()
                {
                    Rol = DtoUsuario.Rol,
                    Token = ManejadorToken.GenerarToken(DtoUsuario)
                });
            }
            catch (ExcepcionCustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Error inesperado");
            }
        }
    }

}
