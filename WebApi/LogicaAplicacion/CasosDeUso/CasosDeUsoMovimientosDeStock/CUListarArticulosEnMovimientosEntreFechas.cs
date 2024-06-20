using DTOs;
using LogicaAplicacion.InterfacesCasosDeUso.Impuesto;
using LogicaAplicacion.InterfacesCasosDeUso.MovimientoDeStock;
using LogicaDatos.Migrations;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.CasosDeUsoMovimientosDeStock
{
    public class CUListarArticulosEnMovimientosEntreFechas : ICUListarArticulosEnMovimientosEntreFechas<DTOArticulosPaginados>
    {
        public IRepositorioMovimientoDeStock Repo { get; set; }
        public ICUObtenerTotalPaginas ObtenerTopePaginas { get; set; }

        public CUListarArticulosEnMovimientosEntreFechas(IRepositorioMovimientoDeStock repo, ICUObtenerTotalPaginas obtenerTopePaginas)
        {
            Repo = repo;
            ObtenerTopePaginas = obtenerTopePaginas;
        }

        public DTOArticulosPaginados ListarArticulosEntreFechas(DateTime fecha1, DateTime fecha2, int pagina)
        {
            List<Articulo> articulos = Repo.BuscarArtDeMovEnRangoDeFechas(fecha1, fecha2);

            int totalElementos = articulos.Count();
            int TopeDePagina = ObtenerTopePaginas.ObtenerPaginado();
            int paginasTotales = (int)Math.Ceiling((double)totalElementos / TopeDePagina);

            articulos = articulos.Skip((pagina - 1) * TopeDePagina).Take(TopeDePagina).ToList();

           List<DTOListarArticulos> articulosMapeados = MapperArticulos.ToDTOListarArticulos(articulos);

            return new DTOArticulosPaginados()
            {
                Articulos = articulosMapeados,
                PaginasTotales = paginasTotales               
            };
        }
    }
}
