
using DTOs;
using LogicaAplicacion.CasosDeUso.CasosDeUsoArticulo;
using LogicaAplicacion.CasosDeUso.CasosDeUsoMovimientosDeStock;
using LogicaAplicacion.CasosDeUso.CasosDeUsoTipoDeMovimiento;
using LogicaAplicacion.InterfacesCasosDeUso.Genericas;
using LogicaAplicacion.InterfacesCasosDeUso.MovimientoDeStock;
using LogicaDatos.Repositorios;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Articulos
            builder.Services.AddScoped<ICUListar<DTOListarArticulos>, CUListarArticulos>();
            //Movimiento de Stock
            builder.Services.AddScoped<ICUAltaMovimientoDeStock<DTOMovimientoDeStock>, CUAltaMovimientoDeStock>();
            builder.Services.AddScoped<ICUBuscarMovimientoPorId<DTOMovimientoDeStock>, CUBuscarMovimientoDeStockPorId>();
            //Tipo de movimiento de Stock
            builder.Services.AddScoped<ICUAlta<DTOTipoDeMovimiento>, CUAltaTipoDeMovimiento>();
            builder.Services.AddScoped<ICUActualizar<DTOTipoDeMovimiento>, CUActualizarTipoDeMovimiento>();
            builder.Services.AddScoped<ICUBaja<DTOTipoDeMovimiento>, CUBajaTipoDeMovimiento>();
            builder.Services.AddScoped<ICUBuscarPorId<DTOTipoDeMovimiento>, CUBuscarTipoDeMovimientoPorId>();
            builder.Services.AddScoped<ICUListar<DTOTipoDeMovimiento>, CUListarTiposDeMovimiento>();

            //Repositorios
            builder.Services.AddScoped<IRepositorioArticulos, RepositorioArticulosEF>();
            builder.Services.AddScoped<IRepositorioMovimientoDeStock, RepositorioMovimientosDeStockEF>();
            builder.Services.AddScoped<IRepositorioTipoDeMovimiento, RepositorioTipoDeMovimiento>();

            //conexion BD
            string strCon = builder.Configuration.GetConnectionString("conekt");
            builder.Services.AddDbContext<LibreriaContext>(options => options.UseSqlServer(strCon));
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
