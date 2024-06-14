using LogicaNegocio.Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDatos.Repositorios
{
    public class LibreriaContext:DbContext
    {
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<MovimientoDeStock> MovimientosDeStock { get; set; }
        public DbSet<TipoDeMovimiento> TipoDeMovimientos { get; set; }
        public DbSet<Impuesto> Impuestos { get; set; }


        public LibreriaContext(DbContextOptions<LibreriaContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //FLUENT API
            //modelBuilder.Entity<Pedido>().UseTpcMappingStrategy();
            //Direccion (Cliente) Le decimos que una entidad es dueña de la otra
            //modelBuilder.Entity<Cliente>().OwnsOne(cli => cli.Direccion);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               // optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLOCALDB; Initial Catalog = Oblg_Insumos_2024; Integrated Security =SSPI;");
            }

        }


    }
}
