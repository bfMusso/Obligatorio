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
    public class CUListarMovimientosDeStockYTipos : ICUListarMovimientosYTipos<DTOMovimientoStockYTipo>
    {

        public IRepositorioMovimientoDeStock Repo { get; set; }

        public CUListarMovimientosDeStockYTipos(IRepositorioMovimientoDeStock repo)
        {
            Repo = repo;
        }

        public List<DTOMovimientoStockYTipo> ListarMovimientosYTipos(int idArticulo, int idTipo)
        {
            TipoDeMovimiento tipo = new TipoDeMovimiento()
            {
                Id = idTipo,
            };

            List<MovimientoDeStock> movimientos = Repo.BuscarElementosPorIdYTipo(idArticulo, tipo);

            return MapperMovimientoDeStock.ToDTOsMovimientosDeStockYTipos(movimientos);

        }
    }
}

