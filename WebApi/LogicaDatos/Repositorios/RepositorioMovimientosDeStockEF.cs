using LogicaNegocio.Dominio;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDatos.Repositorios
{
    public class RepositorioMovimientosDeStockEF : IRepositorioMovimientoDeStock
    {
        public LibreriaContext Contexto { get; set; }

        public RepositorioMovimientosDeStockEF(LibreriaContext ctx) {
            Contexto = ctx;
        }


        public void Add(MovimientoDeStock obj)
        {
            //Aplicamos validaciones
            obj.Validar();

            // Controles de Articulo, usuario y Tipo
            if (!ControlarSiTipoExiste(obj.Tipo.Id))
            {
                throw new ExcepcionCustomException("El tipo de movimiento no existe.");
            }
            if (!ControlarSiArticuloExiste(obj.ArticuloDeMovimiento.Id))
            {
                throw new ExcepcionCustomException("El artículo no existe.");
            }
            if (!ControlarSiUsuarioExiste(obj.UsuarioDeMovimiento.Id))
            {
                throw new ExcepcionCustomException("Problemas para validar alta debido al usuario.");
            }

            //Hacemos el cambio en el stock de articulos
            ModificarStockArticulos(obj.CantidadArticulo, obj.ArticuloDeMovimiento.Id, obj.Tipo.tipoDeCambioEnStock);

            //Agregamos a BD
            Contexto.Entry(obj.ArticuloDeMovimiento).State = EntityState.Unchanged;
            Contexto.Entry(obj.UsuarioDeMovimiento).State = EntityState.Unchanged;
            Contexto.Entry(obj.Tipo).State = EntityState.Unchanged;
            Contexto.MovimientosDeStock.Add(obj);

            //Guardamos cambios
            Contexto.SaveChanges();
        }

        public List<MovimientoDeStock> GetAll()
        {
            return Contexto.MovimientosDeStock
                           .Include(m => m.ArticuloDeMovimiento)
                           .Include(m => m.UsuarioDeMovimiento)
                           .Include(m => m.Tipo)
                           .ToList();
        }

        public MovimientoDeStock GetById(int id)
        {
            return Contexto.MovimientosDeStock
                            .Include(m => m.ArticuloDeMovimiento)
                            .Include(m => m.UsuarioDeMovimiento)
                            .Include(m => m.Tipo)
                            .FirstOrDefault(m => m.Id == id);
        }

        
       public List<MovimientoDeStock> BuscarElementosPorIdYTipo(int id, TipoDeMovimiento tipo)
       {
            int topePagina = ObtenerTopeDePaginados();


             List<MovimientoDeStock> movimientosEncontrados = new List<MovimientoDeStock>();
                //Buscamos movimientos segun tipo y el id de articulo
             movimientosEncontrados = Contexto.MovimientosDeStock
                                 .Include(m => m.ArticuloDeMovimiento)
                                 .Include(m => m.UsuarioDeMovimiento)
                                 .Include(m => m.Tipo)
                                 .Where(m => m.ArticuloDeMovimiento.Id == id && m.Tipo.Id == tipo.Id)
                                 .OrderByDescending(m => m.FechaYHora)
                                 .ThenBy(m => m.CantidadArticulo)                                 
                                 .ToList();
            //Retornamos elementos encontrados
            return movimientosEncontrados;
       }

        public List<MovimientoDeStock> ListarMovimientosDeStockYTipo(int pagina)
        {
            int topePagina = ObtenerTopeDePaginados();
            


            List<MovimientoDeStock> movimientosEncontrados = new List<MovimientoDeStock>();
            //Buscamos movimientos segun tipo y el id de articulo
            movimientosEncontrados = Contexto.MovimientosDeStock
                                  .Include(m => m.ArticuloDeMovimiento)
                                  .Include(m => m.UsuarioDeMovimiento)
                                  .Include(m => m.Tipo)
                                  .OrderByDescending(m => m.FechaYHora)
                                  .ThenBy(m => m.CantidadArticulo)
                                  .Skip((pagina - 1) * topePagina)
                                  .Take(topePagina)
                                  .ToList();
            //Retornamos elementos encontrados
            return movimientosEncontrados;
        }


        public List<Articulo> BuscarArtDeMovEnRangoDeFechas(DateTime inicial, DateTime final)
        {
            int topePagina = ObtenerTopeDePaginados();

            List<Articulo> articulosEncontrados = new List<Articulo>();
            //Buscamos los articulos en rango de fechas
            articulosEncontrados = Contexto.MovimientosDeStock
                                            .Include(m => m.ArticuloDeMovimiento)
                                            .Where(m => m.FechaYHora >= inicial && m.FechaYHora <= final)
                                            .Select(m => m.ArticuloDeMovimiento)
                                            .Distinct()                                           
                                            .ToList();
            //Retornamos elementos encontrados
            return articulosEncontrados;
        }
       
       

        public List<(int, string, int)> CantidadesPorTipoYFecha()
        {

            var movimientosFiltrados = Contexto.MovimientosDeStock
                .Include(m => m.Tipo)
                .GroupBy(m => new { Anio = m.FechaYHora.Year, Tipo = m.Tipo.Nombre })
                .Select(grupo => new
                {
                    Anio = grupo.Key.Anio,
                    Tipo = grupo.Key.Tipo,
                    Cantidad = grupo.Count()
                })
                .OrderByDescending(grupo => grupo.Anio)
                .ToList();

            List<(int, string, int)> movimientosARetornar = new List<(int, string, int)>();
            foreach (var mf in movimientosFiltrados)
            {
                movimientosARetornar.Add(new(mf.Anio, mf.Tipo, mf.Cantidad));
            }

            return movimientosARetornar;

        }
            

        public void ModificarStockArticulos(int cantidad, int IdArticulo, bool tipoCambio) {
            //Traemos el articulo
            Articulo? articulo = Contexto.Articulos
                                        .Where(a => a.Id == IdArticulo)
                                        .SingleOrDefault();
            //Control existencia de articulo
            if(articulo == null) {
                throw new ExcepcionCustomException("No se encontro el articulo para el movimiento de stock.");
            }

            //Control de tipo de cambio
            if (tipoCambio)
            {
                articulo.Stock = articulo.Stock + cantidad;
            }
            else
            {
                articulo.Stock = articulo.Stock - cantidad;
            }

            //Actualizamos el stock
            if (articulo.Stock < 0)
            {
                throw new ExcepcionCustomException("Cantidad de stock insuficiente para realizar la operacion.");
            }

            try
            {
                Contexto.Articulos.Update(articulo);
                Contexto.SaveChanges();
            }
            catch
            {
                throw new ExcepcionCustomException("Error al actualizar el stock en la base de datos.");
            }
        }


        public bool  ControlarSiArticuloExiste(int id)
        {
            try
            {
                return Contexto.Articulos
                            .Any(a => a.Id == id);
            }
            catch
            {
                throw new ExcepcionCustomException("Error en control de articulos de existentes.");
            }
            
        }

        public bool ControlarSiUsuarioExiste(int id)
        {
            try
            {

                bool retorno = false;

                Usuario? usu = Contexto.Usuarios
                              .SingleOrDefault(u => u.Id == id);

                if (usu != null)
                {
                    if (usu.Rol == "Encargado")
                    {
                        retorno = true;
                    }
                }

                return retorno;

            }
            catch 
            {
                throw new ExcepcionCustomException("Error en control de usuarios existentes.");
            } 
        }

        public bool ControlarSiTipoExiste(int id)
        {
            try
            {
                bool retorno = false;

                TipoDeMovimiento? tipo = Contexto.TipoDeMovimientos
                              .SingleOrDefault(t => t.Id == id);

                if (tipo != null)
                {
                    if (tipo.tipoDeCambioEnStock == true || tipo.tipoDeCambioEnStock == false)
                    {
                        retorno = true;
                    }
                }
                return retorno;
            }
            catch 
            {
                throw new ExcepcionCustomException("Error en control tipos de movimientos existentes.");
            }
        }

    
        public int CantidadTotalDeMovimientos()
        {
           return Contexto.MovimientosDeStock.Count();
        }


        
          public int ObtenerTopeDePaginados()
        {
            int topePagina = Contexto.ValoresFijos
                                .Where(v => v.Nombre == "Paginado")
                                .Select(v => v.Valor)
                                .SingleOrDefault();

            return topePagina;
        }

        public List<MovimientoDeStock> MovimientosAMostrarPorPagina(int pagina)
        {
            return Contexto.MovimientosDeStock
                                              .Include(m => m.ArticuloDeMovimiento)
                                              .Include(m => m.UsuarioDeMovimiento)
                                              .Include(m => m.Tipo)
                                              .Skip((pagina - 1) * 5)
                                              .Take(5)
                                              .ToList();
        }
    }
}
