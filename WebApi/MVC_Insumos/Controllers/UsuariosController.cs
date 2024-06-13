using LogicaAplicacion.CasosDeUso.CasosDeUsoImpuesto;
using LogicaAplicacion.CasosDeUso.CasosDeUsoUsuario;
using LogicaAplicacion.InterfacesCasosDeUso.Genericas;
using LogicaAplicacion.InterfacesCasosDeUso.Impuesto;
using LogicaAplicacion.InterfacesCasosDeUso.Usuario;
using LogicaNegocio.Dominio;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Obl_Insumos.Controllers
{
    public class UsuariosController : Controller
    {

        public ICUAlta<Usuario> CUAlta { get; set; }
        public ICUBaja<Usuario> CUBaja { get; set; }
        public ICUBuscarPorId<Usuario> CUBuscarUsuarioPorId { get; set; }
        public ICUListar<Usuario> CUListado { get; set; }
        public ICUActualizar<Usuario> CUActualizar { get; set; }

        public ICULogin<Usuario> CULogin { get; set; }

        public ICUCargarImpuestos<Impuesto> CUCargarImpuestos { get; set; }


        public UsuariosController(ICUAlta<Usuario> cUAlta, ICUBaja<Usuario> cUBaja, ICUBuscarPorId<Usuario> cUBuscarUsuarioPorId, ICUListar<Usuario> cUListado, ICUActualizar<Usuario> cUActualizar, ICULogin<Usuario> cULogin, ICUCargarImpuestos<Impuesto> cUCargarImpuestos)
        {
            CUAlta = cUAlta;
            CUBaja = cUBaja;
            CUBuscarUsuarioPorId = cUBuscarUsuarioPorId;
            CUListado = cUListado;
            CUActualizar = cUActualizar;
            CULogin = cULogin;
            CUCargarImpuestos = cUCargarImpuestos;
        }


        // GET: UsuariosController
        public ActionResult Index()
        {
            if(HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }
            else
            {
                return View(CUListado.ObtenerListado());
            }    
        }

        // GET: UsuariosController/Create
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

        // POST: UsuariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario nuevo)
        {
            //Checkeo sesion
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }
            try
            {
                CUAlta.Alta(nuevo);
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

        // GET: UsuariosController/Edit/5
        public ActionResult Edit(int id)
        {
            //Checkeo sesion
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }
            //Fin Checkeo sesion

            Usuario us = CUBuscarUsuarioPorId.Buscar(id);
            return View(us);
        }

        // POST: UsuariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuario usuario)
        {
            //Checkeo sesion
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }
            try
            {
                CUActualizar.Actualizar(usuario);
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

        // GET: UsuariosController/Delete/5
        public ActionResult Delete(int id)
        {
            //Checkeo sesion
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }
            //Fin Checkeo sesion

            Usuario us = CUBuscarUsuarioPorId.Buscar(id);
            if (HttpContext.Session.GetString("Email") != us.Email)
            {
                return View(us);
            }
            else
            {
                ViewBag.Mensaje = "No se puede eliminar a si mismo.";
                return RedirectToAction(nameof(Index));
            }
            
        }

        // POST: UsuariosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Usuario usuario)
        {
            //Checkeo sesion
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }

            try
            {
                CUBaja.Baja(usuario.Id);
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


        //Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            try
            {
                Usuario usuario = CULogin.Login(email, password);
                if (usuario != null)
                {
                    HttpContext.Session.SetString("Email", usuario.Email);

                    try
                    {
                        //CARGAR DATOS IVA
                        CUCargarImpuestos.Cargar("IVA");
                        //CARGAR DATOS PLAZO MAXIMO
                        CUCargarImpuestos.Cargar("Plazo maximo");
                        //Cargar datos de express
                        CUCargarImpuestos.Cargar("RecargoMinimo");
                        CUCargarImpuestos.Cargar("RecargoMaximo");
                        //Cargar datos de comun
                        CUCargarImpuestos.Cargar("RecargoComun");
                    }catch (ExcepcionCustomException ex)
                    {
                        ViewBag.Mensaje = ex.Message;
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Mensaje = "Error inesperado en el sistema";
                    }
                   
                    return RedirectToAction("Index", "Articulos");
                }
                else {
                    ViewBag.Mensaje = "El usuario no existe o la contraseña es incorrecta.";
                }
                
            }
            catch (ExcepcionCustomException ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Ocurrió un error inesperado";
            }

            return View(); //Se quita esto del resto y se agrega solo al final del action
        }

        //Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/Usuarios/Login");
        }


    }
}
