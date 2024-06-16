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
    public class CUListarArticulosEnMovimientosEntreFechas : ICUListarArticulosEnMovimientosEntreFechas<DTOListarArticulos>
    {
        public IRepositorioMovimientoDeStock Repo { get; set; }

        public CUListarArticulosEnMovimientosEntreFechas(IRepositorioMovimientoDeStock repo)
        {
            Repo = repo;
        }

        public List<DTOListarArticulos> ListarArticulosEntreFechas(DateTime fecha1, DateTime fecha2)
        {
            List<Articulo> articulos = Repo.BuscarArtDeMovEnRangoDeFechas(fecha1, fecha2);

            return MapperArticulos.ToDTOListarArticulos(articulos);
        }
    }
}
