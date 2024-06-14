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
            //Agregamos a BD
            Contexto.Entry(obj.ArticuloDeMovimiento).State = EntityState.Unchanged;
            Contexto.Entry(obj.UsuarioDeMovimiento).State = EntityState.Unchanged;
            Contexto.Entry(obj.Tipo).State = EntityState.Unchanged;
            Contexto.MovimientosDeStock.Add(obj);
            Contexto.SaveChanges();
        }

        public List<MovimientoDeStock> GetAll()
        {
            return Contexto.MovimientosDeStock
                           .Include(m => m.ArticuloDeMovimiento)
                           .Include(m => m.UsuarioDeMovimiento)
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
            List<MovimientoDeStock> movimientosEncontrados = new List<MovimientoDeStock>();
            //Buscamos movimientos segun tipo y el id de articulo
            movimientosEncontrados = Contexto.MovimientosDeStock
                                  .Include(m => m.ArticuloDeMovimiento)
                                  .Include(m => m.UsuarioDeMovimiento)
                                  .Include(m => m.Tipo)
                                  .Where(m => m.ArticuloDeMovimiento.Id == id && m.Tipo == tipo)
                                  .OrderByDescending(m => m.FechaYHora)
                                  .ThenBy(m => m.CantidadArticulo)
                                  .ToList();
            //Retornamos elementos encontrados
            return movimientosEncontrados;
        }

        public List<Articulo> BuscarArtDeMovEnRangoDeFechas(DateTime inicial, DateTime final)
        {
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

        public int CantidadesPorTipoYFecha(int anio, TipoDeMovimiento tipo)
        {
            int cantidadesMovidas = 0;
            //Buscamos las cantidades segun tipo y año
            cantidadesMovidas = Contexto.MovimientosDeStock
                                        .Where(m => m.FechaYHora.Year == anio && m.Tipo == tipo)
                                        .Sum(m => m.CantidadArticulo);
            //Retornamos resultados
            return cantidadesMovidas;
        }
    }
}
