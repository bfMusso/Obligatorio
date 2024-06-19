using DTOs;
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
    public class CUMovimientosAMostrarPorPagina : ICUMovimientosAMostrarPorPagina
    {
        public IRepositorioMovimientoDeStock Repo { get; set; }


        public CUMovimientosAMostrarPorPagina(IRepositorioMovimientoDeStock repo)
        {
            Repo = repo;
        }

        public List<DTOMovimientoDeStock> MovimientosPorPagina(int pagina)
        {
            List<MovimientoDeStock> movimientos = Repo.MovimientosAMostrarPorPagina(pagina);
            List<DTOMovimientoDeStock> DTOmovimientios = MapperMovimientoDeStock.ToDTOSMovimientosDeStock(movimientos);
            return DTOmovimientios;
        }
    }
}
