using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;
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

        // GET: ListarMovimientosPorTipoYFecha
        public ActionResult ListarMovimientosPorTipoYFecha()
        {

            if (HttpContext.Session.GetString("Token") == null || HttpContext.Session.GetString("Rol") != "Encargado")
            {
                return RedirectToAction("Login", "Usuarios");
            }//Fin Checkeo sesion

            List <DTOCantidad> listadoPorFecha = new List<DTOCantidad>();

            try
            {
                HttpClient client = new HttpClient();
                string url = UrlApi + "MovimientosDeStock/CantidadDeMovimientosPorAnioYTipo";        
                var tarea = client.GetAsync(url);
                tarea.Wait();
                var respuesta = tarea.Result;
                string cuerpo = HerramientasAPI.LeerContenidoRespuesta(respuesta);
               
                if (respuesta.IsSuccessStatusCode) //ES 200
                {
                     listadoPorFecha = JsonConvert.DeserializeObject<List<DTOCantidad>>(cuerpo);
                   return View(listadoPorFecha);
                }

                ViewBag.Mensaje = cuerpo;
                return View(listadoPorFecha);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Ocurrión un error inesperado: " + ex.Message;
                return View(listadoPorFecha);
            }      
        }

        // GET: ListarMovimientosPorTipoYArticulo
        public ActionResult ListarMovimientosPorTipoYArticulo() {

            if (HttpContext.Session.GetString("Token") == null || HttpContext.Session.GetString("Rol") != "Encargado")
            {
                return RedirectToAction("Login", "Usuarios");
            }//Fin Checkeo sesion

            try
            {
                HttpClient client = new HttpClient();
                string url = UrlApi + "MovimientosDeStock";///ListarTodo
                var tarea = client.GetAsync(url);

                tarea.Wait();
                var respuesta = tarea.Result;
                string cuerpo = HerramientasAPI.LeerContenidoRespuesta(respuesta);

                if (respuesta.IsSuccessStatusCode) //ES 200
                {
                    List<DTOMovimientoDeStock> movimientos = JsonConvert.DeserializeObject<List<DTOMovimientoDeStock>>(cuerpo);
                    return View(movimientos);
                }
                else
                {
                    ViewBag.Mensaje = cuerpo;
                    return View(new List<DTOMovimientoDeStock>());
                }
            }
            catch
            {
                ViewBag.Mensaje = "Ocurrión un error inesperado";
                return View(new List<DTOMovimientoDeStock>());
            }

        }

        // GET: ListarMovimientosPorTipoYArticulo/articulo, tipo
        [HttpPost]
        public ActionResult ListarMovimientosPorTipoYArticuloPost(int articulo, int tipo)
        {
            try
            {
                HttpClient client = new HttpClient();
                string url = UrlApi + "MovimientosDeStock/MovimientoPorTipo/" + articulo + "/" + tipo;
                var tarea = client.GetAsync(url);

                tarea.Wait();
                var respuesta = tarea.Result;
                string cuerpo = HerramientasAPI.LeerContenidoRespuesta(respuesta);

                if (respuesta.IsSuccessStatusCode) //ES 200
                {
                    List<DTOMovimientoStockYTipo> movimientos = JsonConvert.DeserializeObject<List<DTOMovimientoStockYTipo>>(cuerpo);
                    return View(movimientos);
                }
                else
                {
                    ViewBag.Mensaje = "Error: " + cuerpo;
                    return View(new List<DTOMovimientoStockYTipo>());
                }
            }
            catch
            {
                ViewBag.Mensaje = "Ocurrión un error inesperado";
                return View(new List<DTOMovimientoStockYTipo>());
            }
        }



        // GET: MovimientosDeStockController
        public ActionResult ListarArticulosPorFechas()
        {
            if (HttpContext.Session.GetString("Token") == null || HttpContext.Session.GetString("Rol") != "Encargado")
            {
                return RedirectToAction("Login", "Usuarios");
            }//Fin Checkeo sesion

            try
            {
                HttpClient client = new HttpClient();
                string url = UrlApi + "Articulos";
                var tarea = client.GetAsync(url);

                tarea.Wait();
                var respuesta = tarea.Result;
                string cuerpo = HerramientasAPI.LeerContenidoRespuesta(respuesta);

                if (respuesta.IsSuccessStatusCode) //ES 200
                {
                    List<DTOListarArticulos> articulos = JsonConvert.DeserializeObject<List<DTOListarArticulos>>(cuerpo);
                    return View(articulos);
                }
                else
                {
                    ViewBag.Mensaje = cuerpo;
                    return View(new List<DTOListarArticulos>());
                }
            }
            catch
            {
                ViewBag.Mensaje = "Ocurrión un error inesperado";
                return View(new List<DTOListarArticulos>());
            }
        }

        [HttpPost]
        public ActionResult ListarArticulosPorFechas(string inicial, string final) {

            try
            {
                HttpClient client = new HttpClient();
                string url = UrlApi + "MovimientosDeStock/ArticulosEnMovimientosEntreFechas/" + inicial + "/" + final;
                var tarea = client.GetAsync(url);

                tarea.Wait();
                var respuesta = tarea.Result;
                string cuerpo = HerramientasAPI.LeerContenidoRespuesta(respuesta);

                if (respuesta.IsSuccessStatusCode) //ES 200
                {
                    List<DTOListarArticulos> articulos = JsonConvert.DeserializeObject<List<DTOListarArticulos>>(cuerpo);
                    return View(articulos);
                }else {
                    ViewBag.Mensaje = "Error: " + cuerpo;
                    return View(new List<DTOListarArticulos>());
                }
            }
            catch
            {
                ViewBag.Mensaje = "Ocurrión un error inesperado";
                return View(new List<DTOListarArticulos>());
            }
        }

        // GET: MovimientosDeStockController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MovimientosDeStockController/Create
        public ActionResult Create()
        {
            

            if (HttpContext.Session.GetString("Token") == null || HttpContext.Session.GetString("Rol") != "Encargado")
            {
                return RedirectToAction("Login", "Usuarios");
            }//Fin Checkeo sesion desde MVC
            

            HttpClient cliente = new HttpClient();
            string url = UrlApi + "MovimientosDeStock/AltaMovimientosDeStock";
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                HttpContext.Session.GetString("Token"));
            var tarea = cliente.GetAsync(url);
            tarea.Wait();
            var respuesta = tarea.Result;
            var cuerpo = HerramientasAPI.LeerContenidoRespuesta(respuesta);
            if (respuesta.IsSuccessStatusCode)
            {

                DTOMovimientoDeStock movDTO = new DTOMovimientoDeStock();
                //movDTO.UsuarioDeMovimiento = HttpContext.Session.GetString("Id");
                movDTO.Articulos = ObtenerArticulos();
                movDTO.TiposDeMovimiento = ObtenerTiposDeMovimiento();
                return View(movDTO);

            }
            else
            {
                return RedirectToAction("Login", "Usuarios");
            }

        }

        // POST: MovimientosDeStockController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DTOMovimientoDeStock movDTO)
        {
           
            if (HttpContext.Session.GetString("Token") == null || HttpContext.Session.GetString("Rol") != "Encargado")
            {
                return RedirectToAction("Login", "Usuarios");
            }//F
            
            try
            {
                // movDTO.UsuarioDeMovimientoEmail = HttpContext.Session.GetString("Email");
                //movDTO.UsuarioDeMovimiento = int.Parse(HttpContext.Session.GetString("Id"));
                //movDTO.UsuarioDeMovimiento = int.TryParse(HttpContext.Session.GetString("Id"), out int userId) ? userId : 0;
                movDTO.UsuarioDeMovimiento = (int)HttpContext.Session.GetInt32("Id");


                if (ModelState.IsValid)
                {

                    HttpClient cliente = new HttpClient();
                    string url = UrlApi + "MovimientosDeStock";
                    cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                        HttpContext.Session.GetString("Token"));
                    var tarea = cliente.PostAsJsonAsync(url, movDTO);
                    tarea.Wait();
                    var respuesta = tarea.Result;
                    var cuerpo = HerramientasAPI.LeerContenidoRespuesta(respuesta);
                    if (respuesta.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if ((int)respuesta.StatusCode == StatusCodes.Status400BadRequest
                        || (int)respuesta.StatusCode == StatusCodes.Status404NotFound
                        || (int)respuesta.StatusCode == StatusCodes.Status500InternalServerError)
                    {
                        ViewBag.Mensaje = respuesta.Content.ReadAsStringAsync().Result;

                    }
                    else
                    {
                        //|| (int)respuesta.StatusCode == StatusCodes.Status401Unauthorized)
                        return RedirectToAction("Login", "Usuarios");
                    }

                }

            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
            }

            //movDTO.Usuarios = ObtenerUsuarios();
            movDTO.Articulos = ObtenerArticulos();
            movDTO.TiposDeMovimiento = ObtenerTiposDeMovimiento();
            //movDTO.UsuarioDeMovimientoEmail = HttpContext.Session.GetString("Email");
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
