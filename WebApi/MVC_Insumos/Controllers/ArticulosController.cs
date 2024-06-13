using DTOs;
using LogicaAplicacion.InterfacesCasosDeUso.Genericas;
using LogicaNegocio.Dominio;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Obl_Insumos.Controllers
{

    public class ArticulosController : Controller
    {


        public ICUAlta<Articulo> CUAlta { get; set; }
        public ICUBaja<Articulo> CUBaja { get; set; }
        public ICUBuscarPorId<Articulo> CUBuscarArticuloPorId { get; set; }
        public ICUListar<DTOListarArticulos> CUListado { get; set; }
        public ICUActualizar<Articulo> CUActualizar { get; set; }


        public ArticulosController(ICUAlta<Articulo> cUAlta, ICUBaja<Articulo> cUBaja, ICUBuscarPorId<Articulo> cUBuscarArticuloPorId, ICUListar<DTOListarArticulos> cUListado, ICUActualizar<Articulo> cUActualizar)
        {
            CUAlta = cUAlta;
            CUBaja = cUBaja;
            CUBuscarArticuloPorId = cUBuscarArticuloPorId;
            CUListado = cUListado;
            CUActualizar = cUActualizar;
        }


        // GET: ArticulosController
        public ActionResult Index()
        {
            //Checkeo sesion
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }
            //Fin Checkeo sesion
            return View(CUListado.ObtenerListado());
        }

        // GET: ArticulosController/Create
        public ActionResult Create()
        {
            //Checkeo sesion
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }
            //Fin Checkeo sesion
            return View();
        }

        // POST: ArticulosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Articulo nuevo)
        {
            try
            {
                CUAlta.Alta(nuevo);
                return RedirectToAction(nameof(Index));
            }
            catch(ExcepcionCustomException ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Ocurrió un error inesperado";
            }
            return View();
        }

        // GET: ArticulosController/Edit/5
        public ActionResult Edit(int id)
        {
            //Checkeo sesion
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }
            //Fin Checkeo sesion
            Articulo a = CUBuscarArticuloPorId.Buscar(id);
            return View(a);
        }

        // POST: ArticulosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Articulo nuevo)
        {
            //Checkeo sesion
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }

            try
            {
                CUActualizar.Actualizar(nuevo);
                return RedirectToAction(nameof(Index));
            }
            catch (ExcepcionCustomException ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Ocurrió un error inesperado";
            }
            return View();
        }

        // GET: ArticulosController/Delete/5
        public ActionResult Delete(int id)
        {
            //Checkeo sesion
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }
            //Fin Checkeo sesion
            Articulo a = CUBuscarArticuloPorId.Buscar(id);
            return View(a);
        }

        // POST: ArticulosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Articulo articulo)
        {
            //Checkeo sesion
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }
            try
            {
                CUBaja.Baja(articulo.Id);
                return RedirectToAction(nameof(Index));
            }
            catch (ExcepcionCustomException ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Ocurrió un error inesperado";
            }
            return View();
        }
    }
}
