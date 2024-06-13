using DTOs;
using LogicaAplicacion.CasosDeUso.CasosDeUsoCliente;
using LogicaAplicacion.InterfacesCasosDeUso.Cliente;
using LogicaAplicacion.InterfacesCasosDeUso.Genericas;
using LogicaNegocio.Dominio;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Obl_Insumos.Controllers
{   

    public class ClientesController : Controller
    {

        //public ICUAlta<Cliente> CUAlta { get; set; }

        //public ICUBaja<Cliente> CUBaja { get; set; }

        //public ICUBuscarPorId<Cliente> CUBuscarClientePorId { get; set; }

        //public ICUActualizar<Cliente> CUActualizar { get; set; }

        public ICUListar<DTOCliente> CUListado { get; set; }

        public ICUBuscarPorTexto<Cliente> CUBuscarPorTexto { get; set; }

        public ICUBuscarClientePorMonto<Cliente> CUBuscarClientePorMonto { get; set; }


        public ClientesController(ICUListar<DTOCliente> cUListado, ICUBuscarPorTexto<Cliente> cUBuscarPorTexto, ICUBuscarClientePorMonto<Cliente> cUBuscarClientePorMonto) // ICUAlta<Cliente> cUAlta, ICUBaja<Cliente> cUBaja, ICUBuscarPorId<Cliente> cUBuscarClientePorId, ICUActualizar<Cliente> cUActualizar,
        {
            //CUAlta = cUAlta;
            //CUBaja = cUBaja;
            //CUBuscarClientePorId = cUBuscarClientePorId;
            //CUActualizar = cUActualizar;
            CUListado = cUListado;
            CUBuscarPorTexto = cUBuscarPorTexto;
            CUBuscarClientePorMonto = cUBuscarClientePorMonto;
        }


        // GET: ClientesController
        public ActionResult Index()
        {
            //Checkeo sesion
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }
            //Fin Checkeo sesion

            //Se envia a el index, los elementos del repo clientes, mapeados en DTOs de Cliente, para mostrarlos en lista
            return View(CUListado.ObtenerListado());
        }


        [HttpPost]
        public ActionResult BuscarClientePorTexto(string texto)
        {
            //Checkeo sesion
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }
            try
            {
                //Filtramos los clientes que incluyen ese texto
                List<Cliente> encontrados = CUBuscarPorTexto.MostrarPorTexto(texto);
                if (encontrados.Count == 0)
                {
                    ViewBag.Mensaje = "No se encontraron datos";
                }
                //Los mandamos al index mapeados en DTOs de Cliente
                return View("Index", MapperClientes.toDTOClienteLista(encontrados));
            }
            catch (ExcepcionCustomException ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Ocurrió un error inesperado";
            }
            return View("Index");


        }


        [HttpPost]
        public ActionResult BuscarClientePorMonto(decimal monto)
        {
            //Checkeo sesion
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }

            try
            {
                //Filtramos los clientes que se encuentran en pedidos que superan el monto pasado como parametro
                List<Cliente> encontrados = CUBuscarClientePorMonto.MostrarPorMonto(monto);
                //Se devuelven los clientes encotnrados como DTOs de clientes a el index, para mostrarlos en lista
                return View("Index", MapperClientes.toDTOClienteLista(encontrados));
            }
            catch (ExcepcionCustomException ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Ocurrió un error inesperado";
            }
            return View("Index");


        }

        // GET: ClientesController/Details/5
        /*
        public ActionResult Details(int id)
        {
            //Checkeo sesion
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }
            //Fin Checkeo sesion
            return View(CUBuscarClientePorId.Buscar(id));
        }
        */

        /*

        // GET: ClientesController/Create
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

        // POST: ClientesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cliente nuevo)
        {
            try
            {
                CUAlta.Alta(nuevo);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
            }

            return View();
        }

        */

        /*
        // GET: ClientesController/Edit/5
        public ActionResult Edit(int id)
        {
            //Checkeo sesion
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }
            //Fin Checkeo sesion
            Cliente c = CUBuscarClientePorId.Buscar(id);
            return View(c);
        }

        // POST: ClientesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cliente cli)
        {
            try
            {
                CUActualizar.Actualizar(cli);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            return View();
        }
        */


        /*
        // GET: ClientesController/Delete/5
        public ActionResult Delete(int id)
        {
            //Checkeo sesion
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }
            //Fin Checkeo sesion
            Cliente c = CUBuscarClientePorId.Buscar(id);
            return View(c);
        }

        // POST: ClientesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Cliente cli)
        {
            try
            {
                CUBaja.Baja(cli.Id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            return View();
        }
        */
        /*
                public ActionResult BuscarClientePorTexto()
                {
                    //Checkeo sesion
                    if (HttpContext.Session.GetString("Email") == null)
                    {
                        return RedirectToAction("Login", "Usuarios");
                    }
                    //Fin Checkeo sesion
                    return View();
                }
        */


        /* public ActionResult BuscarClientePorMonto()
        {
            //Checkeo sesion
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }
            //Fin Checkeo sesion
            return View();
        }*/


    }
}
