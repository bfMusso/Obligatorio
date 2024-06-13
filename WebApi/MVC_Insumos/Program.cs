//Referencias a los tipos de logica
using DTOs;
using LogicaAplicacion.CasosDeUso.CasosDeUsoUsuario;
using LogicaAplicacion.CasosDeUso.CasosDeUsoArticulo;
using LogicaAplicacion.CasosDeUso.CasosDeUsoCliente;
using LogicaAplicacion.CasosDeUso.CasosDeUsoImpuesto;
using LogicaAplicacion.CasosDeUso.CasosDeUsoPedido;
using LogicaAplicacion.InterfacesCasosDeUso.Genericas;
using LogicaAplicacion.InterfacesCasosDeUso.Cliente;
using LogicaAplicacion.InterfacesCasosDeUso.Usuario;
using LogicaAplicacion.InterfacesCasosDeUso.Pedido;
using LogicaAplicacion.InterfacesCasosDeUso.Impuesto;
using LogicaDatos.Repositorios;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

//Builders de Interfaces

//Articulo
builder.Services.AddScoped<ICUAlta<Articulo>, CUAltaArticulo>();
builder.Services.AddScoped<ICUBaja<Articulo>, CUBajaArticulo>();
builder.Services.AddScoped<ICUBuscarPorId<Articulo>, CUBuscarArticuloPorId>();
builder.Services.AddScoped<ICUActualizar<Articulo>, CUActualizarArticulo>();
builder.Services.AddScoped<ICUListar<DTOListarArticulos>, CUListarArticulos>();
//Usuarios
builder.Services.AddScoped<ICUAlta<Usuario>, CUAltaUsuario>();
builder.Services.AddScoped<ICUBaja<Usuario>, CUBajaUsuario>();
builder.Services.AddScoped<ICUBuscarPorId<Usuario>, CUBuscarUsuarioPorId>();
builder.Services.AddScoped<ICUActualizar<Usuario>, CUActualizarUsuario>();
builder.Services.AddScoped<ICUListar<Usuario>, CUListarUsuarios>();
builder.Services.AddScoped<ICULogin<Usuario>, CULoginUsuario>();
builder.Services.AddScoped<ICUBuscarConMail<Usuario>, CUBuscarUsuarioConMail>();
//CARGAR IVA
builder.Services.AddScoped<ICUCargarImpuestos<Impuesto>, CUCargarImpuestos>();

//Cliente
builder.Services.AddScoped<ICUAlta<Cliente>, CUAltaCliente>();
builder.Services.AddScoped<ICUBaja<Cliente>, CUBajaCliente>();
builder.Services.AddScoped<ICUBuscarPorTexto<Cliente>, CUBuscarClientePorTexto>();
builder.Services.AddScoped<ICUActualizar<Cliente>, CUActualizarCliente>();
builder.Services.AddScoped<ICUListar<DTOCliente>, CUListarClientes>();
builder.Services.AddScoped<ICUBuscarPorId<Cliente>, CUBuscarClientePorId>();
builder.Services.AddScoped<ICUBuscarClientePorMonto<Cliente>, CUBuscarClientePorMonto>();

//Pedidos
builder.Services.AddScoped<ICUAlta<DTOPedidoAlta>, CUAltaPedido>();
builder.Services.AddScoped<ICUBuscarPorId<Pedido>, CUBuscarPedidoPorId>();
builder.Services.AddScoped<ICUActualizar<DTOPedidoEditar>, CUActualizarPedido>();
builder.Services.AddScoped<ICUAnularPedido<Pedido>, CUAnularPedido>();
builder.Services.AddScoped<ICUListar<DTOPedidoListar>, CUListarPedidos>();
builder.Services.AddScoped<ICUBuscarPedidoPorFecha<DTOPedidoListar>, CUBuscarPedidoPorFecha>();
builder.Services.AddScoped<ICUListarAnulados<DTOPedidoListar>, CUListarPedidosAnulados>();


//Repositorios
builder.Services.AddScoped<IRepositorioArticulos, RepositorioArticulosEF>();
builder.Services.AddScoped<IRepositorioUsuarios, RepositorioUsuariosEF>();
builder.Services.AddScoped<IRepositorioClientes, RepositorioClientesEF>();
builder.Services.AddScoped<IRepositorioImpuestos, RepositorioImpuestosEF>();
builder.Services.AddScoped<IRepositorioPedidos, RepositorioPedidosEF>();

//Libreria context
builder.Services.AddDbContext<LibreriaContext>();


//CONEXION A BD USANDO APPSETTINGS
string strCon = builder.Configuration.GetConnectionString("conekt");
builder.Services.AddDbContext<LibreriaContext>(options => options.UseSqlServer(strCon));


builder.Services.AddSession();

var app = builder.Build();

// Add services to the container.
//builder.Services.AddControllersWithViews();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuarios}/{action=Login}");

app.UseSession();

app.Run();
