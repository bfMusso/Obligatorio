using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMVC.ClasesAuxiliares;


namespace WebMVC.Controllers
{
    public class UsuariosController : Controller
    {
        public string UrlApi;
        public UsuariosController(IConfiguration Config)
        {
            UrlApi = Config.GetValue<string>("URLAPI");
        }

        // GET: UsuariosController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UsuariosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsuariosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuariosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsuariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuariosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsuariosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult IniciarSesion(DTOUsuarioLogin DtoUsuario)
        {

            try
            {
                //Valida el DTO
                if (ModelState.IsValid)
                {
                    HttpClient client = new HttpClient();                  
                    var tarea = client.PostAsJsonAsync(UrlApi+"Usuarios/Login", DtoUsuario);
                    tarea.Wait();
                    var respuesta = tarea.Result;
                    string cuerpo = HerramientasAPI.LeerContenidoRespuesta(respuesta);
                    if (respuesta.IsSuccessStatusCode)
                    {
                        DTOUsuarioLogueado usuarioLogueado = JsonConvert.DeserializeObject<DTOUsuarioLogueado>(cuerpo);
                        if (usuarioLogueado != null)
                        {
                            HttpContext.Session.SetString("Rol", usuarioLogueado.Rol);
                            HttpContext.Session.SetString("Token", usuarioLogueado.Token);
                            // HttpContext.Session.SetString("Id", usuarioLogueado.Id.ToString());
                            HttpContext.Session.SetInt32("Id", usuarioLogueado.Id);
                            HttpContext.Session.SetString("Email", usuarioLogueado.Email);                          
                            return RedirectToAction("Index", "Home"); //Ir la vista del controller deseado tras login exitoso.
                        }
                        ViewBag.Mensaje = "Datos incorrectos";
                    }
                    else if ((int)respuesta.StatusCode == StatusCodes.Status400BadRequest
                        || (int)respuesta.StatusCode == StatusCodes.Status404NotFound
                        || (int)respuesta.StatusCode == StatusCodes.Status500InternalServerError)
                    {
                        ViewBag.Mensaje = cuerpo;
                    }
                    else
                    {
                        ViewBag.Mensaje = "Error en los datos";
                    }
                }
                else
                {
                    ViewBag.Mensaje = "Datos incorrectos";
                }             
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Error:" + ex;
            }
            return View("Login");
        }

    }
}
