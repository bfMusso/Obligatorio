using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LogicaNegocio.Dominio.MovimientoDeStock;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioMovimientoDeStock : IRepositorio<MovimientoDeStock>
    {
        public List<MovimientoDeStock> BuscarElementosPorIdYTipo(int id, TipoDeMovimiento tipo);

        public List<Articulo> BuscarArtDeMovEnRangoDeFechas(DateTime inicial, DateTime final);

        public int CantidadesPorTipoYFecha(int anio, TipoDeMovimiento tipo);
    }
}
