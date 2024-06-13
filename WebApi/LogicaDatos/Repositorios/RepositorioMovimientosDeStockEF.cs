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
                            .FirstOrDefault(m => m.Id == id);
        }

        public void Remove(int id)
        {
            MovimientoDeStock movimiento = GetById(id);
            if (movimiento != null)
            {
                Contexto.MovimientosDeStock.Remove(movimiento);
                Contexto.SaveChanges();
            }
            else
            {
                throw new ExcepcionCustomException("El Movimiento con id" + id + " no existe");
            }
        }

        public void Update(MovimientoDeStock obj)
        {
            //Se ejecutan las validaciones del dominio
            obj.Validar();
            //Actualizar y guardar
            Contexto.MovimientosDeStock.Update(obj);
            //Se efectuan los cambios
            Contexto.SaveChanges();
        }

        public List<MovimientoDeStock> BuscarElementosPorIdYTipo(int id, MovimientoDeStock.TipoDeMovimiento tipo)
        {
            List<MovimientoDeStock> movimientosEncontrados = new List<MovimientoDeStock>();
            //Buscamos movimientos segun tipo y el id de articulo
            movimientosEncontrados = Contexto.MovimientosDeStock
                                  .Include(m => m.ArticuloDeMovimiento)
                                  .Include(m => m.UsuarioDeMovimiento)
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

        public int CantidadesPorTipoYFecha(int anio, MovimientoDeStock.TipoDeMovimiento tipo)
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
