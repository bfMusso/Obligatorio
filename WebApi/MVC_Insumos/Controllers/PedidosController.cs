using DTOs;
using LogicaAplicacion.CasosDeUso.CasosDeUsoArticulo;
using LogicaAplicacion.CasosDeUso.CasosDeUsoCliente;
using LogicaAplicacion.CasosDeUso.CasosDeUsoPedido;
using LogicaAplicacion.InterfacesCasosDeUso.Cliente;
using LogicaAplicacion.InterfacesCasosDeUso.Genericas;
using LogicaAplicacion.InterfacesCasosDeUso.Pedido;
using LogicaAplicacion.InterfacesCasosDeUso.Usuario;
using LogicaNegocio.Dominio;
using LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_Insumos.Models;

namespace MVC_Insumos.Controllers
{
    public class PedidosController : Controller
    {

        //casos de uso de pedidos
        public ICUAlta<DTOPedidoAlta> CUAlta { get; set; }
        public ICUAnularPedido<Pedido> CUAnularPedido { get; set; }
        public ICUBuscarPorId<Pedido> CUBuscarPedidoPorId { get; set; }
        public ICUListar<DTOPedidoListar> CUListado { get; set; }
        public ICUActualizar<DTOPedidoEditar> CUActualizar { get; set; }
        public ICUBuscarConMail<Usuario> CUBuscarConMail { get; set; }

        public ICUListarAnulados<DTOPedidoListar> CUListarPedidosAnulados { get; set; }
        //Casos de uso de Clientes
        ICUListar<DTOCliente> CUlistarClientes {  get; set; }
        ICUBuscarPorId<Cliente> CUlBuscarClientePorID { get; set; }

        //Casos de uso de articulos 
        ICUListar<DTOListarArticulos> CUlistarArticulos { get; set; }
        ICUBuscarPorId<Articulo> CUlBuscarArticuloPorID { get; set; }
        //Casos de uso pedidos
        ICUBuscarPedidoPorFecha<DTOPedidoListar> CUBuscarPedidoPorFecha { get; set; }

        public PedidosController(ICUAlta<DTOPedidoAlta> cUAlta, ICUAnularPedido<Pedido> cUAnularPedido, ICUBuscarPorId<Pedido> cUBuscarPedidoPorId, ICUListar<DTOPedidoListar> cUListado, ICUActualizar<DTOPedidoEditar> cUActualizar,
            ICUBuscarConMail<Usuario> cUBuscarConMail, ICUListar<DTOCliente> cUlistarClientes, ICUBuscarPorId<Cliente> cUlBuscarClientePorID, ICUListar<DTOListarArticulos> cUlistarArticulos, ICUBuscarPorId<Articulo> cUlBuscarArticuloPorID, ICUBuscarPedidoPorFecha<DTOPedidoListar> cUBuscarPedidoPorFecha, ICUListarAnulados<DTOPedidoListar> cUListarPedidosAnulados)
        {
            CUAlta = cUAlta;
            CUAnularPedido = cUAnularPedido;
            CUBuscarPedidoPorId = cUBuscarPedidoPorId;
            CUListado = cUListado;
            CUActualizar = cUActualizar;
            CUBuscarConMail = cUBuscarConMail;
            CUlistarClientes = cUlistarClientes;
            CUlBuscarClientePorID = cUlBuscarClientePorID;
            CUlistarArticulos = cUlistarArticulos;
            CUlBuscarArticuloPorID = cUlBuscarArticuloPorID;
            CUBuscarPedidoPorFecha = cUBuscarPedidoPorFecha;
            CUListarPedidosAnulados = cUListarPedidosAnulados;
        }

        // GET: PedidosController
        public ActionResult Index()
        {
            //Checkeo sesion
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }//Fin Checkeo sesion
        //Se recibe una lista de DTOs obtenidos de un mapper en casos de uso
          return View(CUListado.ObtenerListado());
        }


        // GET: PedidosController/Create
        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("Email") != null)
            {

                //Se instancia la VM para el alta de pedido, con los elementos Clientes y Articulos seteados en ella, ambos listas conseguidas con sus casos de uso
                PedidoAltaViewModel vm = new PedidoAltaViewModel()
                {
                    Clientes = CUlistarClientes.ObtenerListado(),
                    Articulos = CUlistarArticulos.ObtenerListado(),
                };
                
                //Se retorna con la VM(A la misma se le carga desde la vista el resto de las prop)
                return View(vm);

            }

            //Fin Checkeo sesion
            return RedirectToAction("Login", "Usuarios");
        }

        // POST: PedidosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PedidoAltaViewModel vm)
        { 
            //Checkeo sesion
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }

            try
            {
                //Se genera un DTO y se setea con los datos del VM(Se le da id = 0, pero en la BD se le asigna automaticamente)
                 DTOPedidoAlta dto = new DTOPedidoAlta()
                {
                    Id = 0,
                    FechaDeEntrega = vm.FechaDeEntrega,
                    ClienteId = vm.ClienteId,
                    ArticuloId = vm.ArticuloId,
                    ArticuloCantidad = vm.ArticuloCantidad,
                    Tipo = vm.Tipo

                };
                 //Alta con el DTO Declarado y seteado anteriormente
                 CUAlta.Alta(dto);
                //Se trae objeto pedido con el Id del dto, y se pasa mensaje con CostoDelPedido mediante TempData a la vista Index
                Pedido objAmostrar = CUBuscarPedidoPorId.Buscar(dto.Id);
                //Total(Inicialmente con cantidad)
                TempData["SuccessMessage"] = $"Pedido creado exitosamente. Total: {objAmostrar.CostoDelPedido}";
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                //En caso que suceda un error, se vuelven a cargar las listas(Articulos y Clientes) en la vista mediante la VM
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                ViewBag.Mensaje = "Error al guardar el pedido: " + message;
                vm.Clientes = CUlistarClientes.ObtenerListado();
                vm.Articulos = CUlistarArticulos.ObtenerListado();
                return View(vm);
            }
           

        }

       
        //Editar queda presentado pero no funcional

        // GET: PedidosController/Edit/5
        public ActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }
            //Fin Checkeo sesion

            //Se genera un VM con el Id del pedido y una lista de la totalidad de articulos
            PedidoNuevaLineaViewModel vm = new PedidoNuevaLineaViewModel()
            {
                Id = id,
                Articulos = CUlistarArticulos.ObtenerListado(),
            };

            //Se retorna con la VM como parametro(Para usarla en la vista)
            return View(vm);

        }

        // POST: PedidosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PedidoNuevaLineaViewModel vm)
        {
            //Checkeo sesion
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }
            try
            {
                //Se genera un DTO con los datos del VM, los cuales fueron en parte, agregados desde la vista del edit(ArticuloCantidad, ArticuloId)
                DTOPedidoEditar dto = new DTOPedidoEditar()
                {
                    Id = vm.Id,
                    ArticuloId = vm.ArticuloId,
                    ArticuloCantidad = vm.ArticuloCantidad,

                };
                //Se actualiza el pedido mediante el caso de uso que recibe el DTo seteado anteriormente
                CUActualizar.Actualizar(dto);
                //Se retorna al index
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

        // GET: PedidosController/Delete/5
        public ActionResult Baja(int id)
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }
            //Fin Checkeo sesion

            //Se trae objeto de tipo pedido con el id que viene como parametro
            Pedido p = CUBuscarPedidoPorId.Buscar(id);

            //Si el estado de anulado es true, se manda confirmacion a la vista principal con TempData
            if (p.Anulado)
            {
                TempData["ErrorMessage"] = "El pedido ya se encontraba anulado.";
                return RedirectToAction(nameof(Index));
            }

            //Si anulado es false, generamos un nuevo DTO a partir de ese pedido(Se quita por que no se usa)
            /*
            DTOPedido dto = new DTOPedido()
            {
                Id = p.Id,
                FechaDeEntrega = p.FechaDeEntrega,
                ClienteId = p.Cliente.Id,
                CostoDelPedido = p.CostoDelPedido,

            };
             */


            //Si el pedido aun no fue anulado, se anula pedido con el id que se recibe como parametro 
            try
            {
                CUAnularPedido.Anular(id);
                TempData["SuccessMessage"] = "El pedido ha sido anulado correctamente.";
                return RedirectToAction(nameof(Index));
            }

            catch (ExcepcionCustomException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }  

            catch (Exception ex)
            {
                ViewBag.Mensaje = "Ocurrió un error inesperado";
                return RedirectToAction(nameof(Index));
            }

        }

        // POST: PedidosController/Delete/5

        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Baja(DTOPedido pedido)
        {
            int elId = pedido.Id;

            try
            {
                CUAnularPedido.Anular(pedido.Id);
                TempData["SuccessMessage"] = "El pedido ha sido anulado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                ViewBag.Mensaje = ex.Message;
            }

            return View();
        }
        */

        public ActionResult BuscarPedidosPorFecha()
        {
            //Checkeo sesion
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }
            //Fin Checkeo sesion

            return View();
        }


        [HttpPost]
        public ActionResult BuscarPedidosPorFecha(DateTime fecha)
        {
            try
            {
                //Se genera lista de DTO pedidos que coinciden con la fecha recibida como parametro
                List<DTOPedidoListar> encontrados = CUBuscarPedidoPorFecha.BuscarPedidoSegunFecha(fecha);
                //La devolucion de la busqueda es en DTO para que se cargue en la vista Index
                return View("Index", encontrados);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            return View("Index");           
        }


        [HttpPost]
        public ActionResult BuscarPedidosAnulados()
        {
            //Checkeo sesion
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "Usuarios");
            }
            //Fin Checkeo sesion
            try
            {   
                //Se genera busqueda de pedidos anulados(Anulado = true) con devolucion de lista de DTO de pedidos
                List<DTOPedidoListar> anulados = CUListarPedidosAnulados.ObtenerAnulados();
                //La devolucion de la busqueda es en DTO para que se cargue en la vista Index
                return View("Index", anulados);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            return View("Index");
        }
    }
}
