
using DTOs;
using LogicaAplicacion.CasosDeUso.CasosDeUsoArticulo;
using LogicaAplicacion.CasosDeUso.CasosDeUsoMovimientosDeStock;
using LogicaAplicacion.InterfacesCasosDeUso.Genericas;
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
            builder.Services.AddScoped<ICUAlta<DTOMovimientoDeStock>, CUAltaMovimientoDeStock>();
            builder.Services.AddScoped<ICUBuscarPorId<DTOMovimientoDeStock>, CUBuscarMovimientoDeStockPorId>();

            //Repositorios
            builder.Services.AddScoped<IRepositorioArticulos, RepositorioArticulosEF>();
            builder.Services.AddScoped<IRepositorioMovimientoDeStock, RepositorioMovimientosDeStockEF>();

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
