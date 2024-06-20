using DTOs;
using LogicaAplicacion.InterfacesCasosDeUso.Impuesto;
using LogicaAplicacion.InterfacesCasosDeUso.MovimientoDeStock;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.CasosDeUsoMovimientosDeStock
{
    public class CUListarMovimientosDeStockYTipos : ICUListarMovimientosYTipos<DTOMovimientoStockYTipoPaginado>
    {

        public IRepositorioMovimientoDeStock Repo { get; set; }

        public ICUObtenerTotalPaginas ObtenerTopePaginas { get; set; }

        public CUListarMovimientosDeStockYTipos(IRepositorioMovimientoDeStock repo, ICUObtenerTotalPaginas obtenerTopePaginas)
        {
            Repo = repo;
            ObtenerTopePaginas = obtenerTopePaginas;
        }

        public DTOMovimientoStockYTipoPaginado ListarMovimientosYTipos(int idArticulo, int idTipo, int pagina)
        {

            TipoDeMovimiento tipo = new TipoDeMovimiento()
            {
                Id = idTipo,
            };


            List<MovimientoDeStock> movimientos = Repo.BuscarElementosPorIdYTipo(idArticulo, tipo);

            int totalElementos = movimientos.Count();
            int TopeDePagina = ObtenerTopePaginas.ObtenerPaginado();
            int paginasTotales = (int)Math.Ceiling((double)totalElementos / TopeDePagina);

            movimientos = movimientos.Skip((pagina - 1) * TopeDePagina).Take(TopeDePagina).ToList();


            List<DTOMovimientoStockYTipo> movimientosMapeados = MapperMovimientoDeStock.ToDTOsMovimientosDeStockYTipos(movimientos);

            return new DTOMovimientoStockYTipoPaginado()
            {
                MovimientosStockYTipo = movimientosMapeados,
                PaginasTotales = paginasTotales,
                
            };

           

        }
    }
}

