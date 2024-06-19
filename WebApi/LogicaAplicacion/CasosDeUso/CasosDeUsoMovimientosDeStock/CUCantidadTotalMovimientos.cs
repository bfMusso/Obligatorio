using LogicaAplicacion.InterfacesCasosDeUso.MovimientoDeStock;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.CasosDeUsoMovimientosDeStock
{
    public class CUCantidadTotalMovimientos : ICUCantidadTotalMovimientos
    {

        public IRepositorioMovimientoDeStock Repo { get; set; }


        public CUCantidadTotalMovimientos(IRepositorioMovimientoDeStock repo)
        {
            Repo = repo;
        }

        public int CantidadTotalMovimientos()
        {
            return Repo.CantidadTotalDeMovimientos();
        }
    }
}
