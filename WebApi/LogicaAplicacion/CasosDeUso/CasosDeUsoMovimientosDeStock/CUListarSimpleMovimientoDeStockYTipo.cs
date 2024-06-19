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
    public class CUListarSimpleMovimientoDeStockYTipo : ICUListarSimpleMOvimientoDeStockYTipo<DTOMovimientoStockYTipo>
    {
        public IRepositorioMovimientoDeStock Repo { get; set; }

        public CUListarSimpleMovimientoDeStockYTipo(IRepositorioMovimientoDeStock repo)
        {

            Repo = repo;
        }

        public List<DTOMovimientoStockYTipo> ListarMovimientosDeStockYTipo()
        {
            List<MovimientoDeStock> movimientos = new List<MovimientoDeStock>();
            List<DTOMovimientoStockYTipo> DtosMovimientosStockYTipo = new List<DTOMovimientoStockYTipo>();

            movimientos = Repo.ListarMovimientosDeStockYTipo();
            if (movimientos.Count > 0)
            {
                DtosMovimientosStockYTipo = MapperMovimientoDeStock.ToDTOsMovimientosDeStockYTipos(movimientos);
            }
            return DtosMovimientosStockYTipo;
        }
    }

}
