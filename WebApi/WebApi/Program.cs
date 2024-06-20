
using DTOs;
using LogicaAplicacion.CasosDeUso.CasosDeUsoArticulo;
using LogicaAplicacion.CasosDeUso.CasosDeUsoMovimientosDeStock;
using LogicaAplicacion.CasosDeUso.CasosDeUsoTipoDeMovimiento;
using LogicaAplicacion.CasosDeUso.CasosDeUsoUsuario;
using LogicaAplicacion.CasosDeUso.CasosDeUsoValoresFijos;
using LogicaAplicacion.InterfacesCasosDeUso.Genericas;
using LogicaAplicacion.InterfacesCasosDeUso.Impuesto;
using LogicaAplicacion.InterfacesCasosDeUso.MovimientoDeStock;
using LogicaAplicacion.InterfacesCasosDeUso.Usuario;
using LogicaDatos.Repositorios;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //
            builder.Services.AddScoped<ICUObtenerTotalPaginas, CUObtenerTotalPaginado>();
            //Usuarios
            builder.Services.AddScoped<ICULogin<DTOUsuario>, CULoginUsuario>();
            builder.Services.AddScoped<ICUBuscarConMail<DTOUsuario>, CUBuscarUsuarioConMail>();           
            //Articulos
            builder.Services.AddScoped<ICUListar<DTOListarArticulos>, CUListarArticulos>();
            //Movimiento de Stock
            builder.Services.AddScoped<ICUAltaMovimientoDeStock<DTOMovimientoDeStock>, CUAltaMovimientoDeStock>();
            builder.Services.AddScoped<ICUBuscarMovimientoPorId<DTOMovimientoDeStock>, CUBuscarMovimientoDeStockPorId>();
            builder.Services.AddScoped<ICUListarMovimientosDeStock<DTOMovimientoDeStock>, CUListarMovimientosDeStock>();
            builder.Services.AddScoped<ICUListarMovimientosYTipos<DTOMovimientoStockYTipoPaginado>, CUListarMovimientosDeStockYTipos>();
            builder.Services.AddScoped<ICUListarArticulosEnMovimientosEntreFechas<DTOArticulosPaginados>, CUListarArticulosEnMovimientosEntreFechas>();
            builder.Services.AddScoped<ICUCantidadMovimientosPorTipoYFecha<DTOCantidad>, CUCantidadMovimientosPorTipoYFecha>();
            builder.Services.AddScoped<ICUCantidadTotalMovimientos, CUCantidadTotalMovimientos>();
            
            builder.Services.AddScoped<ICUListarSimpleMOvimientoDeStockYTipo<DTOMovimientoStockYTipo>, CUListarSimpleMovimientoDeStockYTipo>();

            //Tipo de movimiento de Stock
            builder.Services.AddScoped<ICUAlta<DTOTipoDeMovimiento>, CUAltaTipoDeMovimiento>();
            builder.Services.AddScoped<ICUActualizar<DTOTipoDeMovimiento>, CUActualizarTipoDeMovimiento>();
            builder.Services.AddScoped<ICUBaja<DTOTipoDeMovimiento>, CUBajaTipoDeMovimiento>();
            builder.Services.AddScoped<ICUBuscarPorId<DTOTipoDeMovimiento>, CUBuscarTipoDeMovimientoPorId>();
            builder.Services.AddScoped<ICUListar<DTOTipoDeMovimiento>, CUListarTiposDeMovimiento>();
  

            //Repositorios
            builder.Services.AddScoped<IRepositorioArticulos, RepositorioArticulosEF>();
            builder.Services.AddScoped<IRepositorioUsuarios, RepositorioUsuariosEF>();
            builder.Services.AddScoped<IRepositorioMovimientoDeStock, RepositorioMovimientosDeStockEF>();
            builder.Services.AddScoped<IRepositorioTipoDeMovimiento, RepositorioTipoDeMovimiento>();
            builder.Services.AddScoped<IRepositorioValoresFijos, RepositorioValoresFijosEF>();

            //conexion BD
            string strCon = builder.Configuration.GetConnectionString("conekt");
            builder.Services.AddDbContext<LibreriaContext>(options => options.UseSqlServer(strCon));
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            //Lineas de JWT

            var claveSecreta = "ZWRpw6fDo28gZW0gY29tcHV0YWRvcmE=";
            builder.Services.AddAuthentication(aut => {
                aut.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                aut.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(aut => {
                aut.RequireHttpsMetadata = false;
                aut.SaveToken = true;
                aut.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(claveSecreta)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


            //FIN de Lineas JWT



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
