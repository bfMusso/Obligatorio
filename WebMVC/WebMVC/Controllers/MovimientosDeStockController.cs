using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMVC.ClasesAuxiliares;

namespace WebMVC.Controllers
{
    public class MovimientosDeStockController : Controller
    {

        public string UrlApi;
        public MovimientosDeStockController(IConfiguration Config)
        {
            UrlApi = Config.GetValue<string>("URLAPI");
        }


        // GET: MovimientosDeStockController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MovimientosDeStockController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MovimientosDeStockController/Create
        public ActionResult Create()
        {
            DTOMovimientoDeStock movDTO = new DTOMovimientoDeStock();
            movDTO.UsuarioDeMovimientoEmail = HttpContext.Session.GetString("Email");
            movDTO.Articulos = ObtenerArticulos();
            movDTO.TiposDeMovimiento = ObtenerTiposDeMovimiento();
            return View(movDTO);      
        }

        // POST: MovimientosDeStockController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DTOMovimientoDeStock movDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    //movDTO.UsuarioDeMovimientoEmail = HttpContext.Session.GetString("Email");
                    movDTO.UsuarioDeMovimientoEmail = "Fernando@gmail.com";
                    HttpClient cliente = new HttpClient();
                    string url = UrlApi + "MovimientosDeStock";
                    var tarea = cliente.PostAsJsonAsync(url, movDTO);
                    tarea.Wait();
                    var respuesta = tarea.Result;
                    var cuerpo = HerramientasAPI.LeerContenidoRespuesta(respuesta);
                    if (respuesta.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else if ((int)respuesta.StatusCode == StatusCodes.Status400BadRequest
                        || (int)respuesta.StatusCode == StatusCodes.Status500InternalServerError)
                    {
                        ViewBag.Mensaje = respuesta.Content.ReadAsStringAsync().Result;
                    }

                }

            }
            catch(Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
            }

            //movDTO.Usuarios = ObtenerUsuarios();
            movDTO.Articulos = ObtenerArticulos();
            movDTO.TiposDeMovimiento = ObtenerTiposDeMovimiento();
            movDTO.UsuarioDeMovimientoEmail = HttpContext.Session.GetString("Email");
            return View(movDTO);
        }

        // GET: MovimientosDeStockController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MovimientosDeStockController/Edit/5
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

        // GET: MovimientosDeStockController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MovimientosDeStockController/Delete/5
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


        /*
         METODO PARA OBTENER LOS DESPLEGABLES
         */
        public List<DTOArticulos> ObtenerArticulos()
        {
            List<DTOArticulos> articulos = new List<DTOArticulos>();
            HttpClient cliente = new HttpClient();
            string url = UrlApi + "Articulos";
            var tarea = cliente.GetAsync(url);
            tarea.Wait();
            var respuesta = tarea.Result;
            string cuerpo = HerramientasAPI.LeerContenidoRespuesta(tarea.Result);
            if (respuesta.IsSuccessStatusCode)
            {
                articulos = JsonConvert.DeserializeObject<List<DTOArticulos>>(cuerpo);
            }
            return articulos;
        }

        public List<DTOUsuario> ObtenerUsuarios()
        {
            List<DTOUsuario> usuarios = new List<DTOUsuario>();
            HttpClient cliente = new HttpClient();
            string url = UrlApi + "Usuarios";
            var tarea = cliente.GetAsync(url);
            tarea.Wait();
            var respuesta = tarea.Result;
            string cuerpo = HerramientasAPI.LeerContenidoRespuesta(tarea.Result);
            if (respuesta.IsSuccessStatusCode)
            {
                usuarios = JsonConvert.DeserializeObject<List<DTOUsuario>>(cuerpo);
            }
            return usuarios;
        }

        public List<DTOTipoDeMovimiento> ObtenerTiposDeMovimiento()
        {
            List<DTOTipoDeMovimiento> tiposMov = new List<DTOTipoDeMovimiento>();
            HttpClient cliente = new HttpClient();
            string url = UrlApi + "TipoDeMovimiento";
            var tarea = cliente.GetAsync(url);
            tarea.Wait();
            var respuesta = tarea.Result;
            string cuerpo = HerramientasAPI.LeerContenidoRespuesta(tarea.Result);
            if (respuesta.IsSuccessStatusCode)
            {
                tiposMov = JsonConvert.DeserializeObject<List<DTOTipoDeMovimiento>>(cuerpo);
            }
            return tiposMov;
        }

    }
}
