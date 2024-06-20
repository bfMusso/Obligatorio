using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;
using System.Security.Policy;
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
                HttpClient cliente = new HttpClient();
                string url = UrlApi + "MovimientosDeStock/CantidadDeMovimientosPorAnioYTipo";                      
                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                HttpContext.Session.GetString("Token"));
                var tarea = cliente.GetAsync(url);
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

      

        // GET: ListarMovimientosPorTipoYArticulo/articulo, tipo
        [HttpGet]
        public ActionResult ListarMovimientosPorTipoYArticulo(int articulo, int tipo, int? pagina)
        {
            if (HttpContext.Session.GetString("Token") == null || HttpContext.Session.GetString("Rol") != "Encargado")
            {
                return RedirectToAction("Login", "Usuarios");
            }//Fin Checkeo sesion

            if (articulo == null && tipo == null || articulo == 0 && tipo == 0)
            {
                ViewBag.Mensaje = "Ingrese un articulo y un valor";
                return View();
            }
            else
            {
                ViewBag.ArticuloAlmacenado = articulo;
                ViewBag.TipoAlmacenado = tipo;


               DTOMovimientoStockYTipoPaginado movimientos = new DTOMovimientoStockYTipoPaginado();
                try
                {
                    if (pagina == null)
                    {
                        pagina = 1;
                    }

                    HttpClient cliente = new HttpClient();
                    cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                    string url = UrlApi + "MovimientosDeStock/MovimientoPorTipo/" + articulo + "/" + tipo + "/" + pagina;
                    var tarea = cliente.GetAsync(url);
                    tarea.Wait();
                    var respuesta = tarea.Result;
                    string cuerpo = HerramientasAPI.LeerContenidoRespuesta(respuesta);

                    if (respuesta.IsSuccessStatusCode) //ES 200
                    {
                        movimientos = JsonConvert.DeserializeObject<DTOMovimientoStockYTipoPaginado>(cuerpo);



                        //  ViewBag.Paginas = (int)(movimientos.TotalElementos / movimientos.TopeDePagina);
                        ViewBag.Paginas = movimientos.PaginasTotales;

                    }
                    else if ((int)respuesta.StatusCode == StatusCodes.Status400BadRequest
                     || (int)respuesta.StatusCode == StatusCodes.Status500InternalServerError)
                    {
                        return ViewBag.Mensaje = cuerpo;

                    }
                }
                catch
                {
                    ViewBag.Mensaje = "Ocurrión un error inesperado";
                }
                return View(movimientos.MovimientosStockYTipo);
            }

            
        }

        [HttpGet]
        public ActionResult ListarArticulosPorFechas(string inicial, string final, int? pagina) {

            if (HttpContext.Session.GetString("Token") == null || HttpContext.Session.GetString("Rol") != "Encargado")
            {
                return RedirectToAction("Login", "Usuarios");
            }//Fin Checkeo sesion


            if (inicial == null && final == null)
            {
                ViewBag.Mensaje = "Ingrese ambas fechas para buscar";
                return View();
            }
            else
            {
                ViewBag.FechaInicial = inicial;
                ViewBag.FechaFinal = final;


                DTOArticulosPaginados articulos = new DTOArticulosPaginados();
                try
                {
                    if (pagina == null)
                    {
                        pagina = 1;
                    }
                    HttpClient cliente = new HttpClient();
                    string url = UrlApi + "MovimientosDeStock/ArticulosEnMovimientosEntreFechas/" + inicial + "/" + final + "/" + pagina;
                    cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                    HttpContext.Session.GetString("Token"));
                    var tarea = cliente.GetAsync(url);
                    tarea.Wait();
                    var respuesta = tarea.Result;
                    string cuerpo = HerramientasAPI.LeerContenidoRespuesta(respuesta);

                    if (respuesta.IsSuccessStatusCode) //ES 200
                    {
                        articulos = JsonConvert.DeserializeObject<DTOArticulosPaginados>(cuerpo);

                        ViewBag.Paginas = articulos.PaginasTotales;

                    }
                    else if ((int)respuesta.StatusCode == StatusCodes.Status400BadRequest
                        || (int)respuesta.StatusCode == StatusCodes.Status500InternalServerError)
                    {
                        return ViewBag.Mensaje = cuerpo;

                    }
                }
                catch
                {
                    ViewBag.Mensaje = "Ocurrión un error inesperado";
                }

                return View(articulos.Articulos);
            }
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
