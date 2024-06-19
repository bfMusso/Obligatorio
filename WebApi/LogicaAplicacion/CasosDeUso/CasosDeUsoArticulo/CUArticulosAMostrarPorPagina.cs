using DTOs;
using LogicaAplicacion.InterfacesCasosDeUso.Articulo;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.CasosDeUsoArticulo
{
    public class CUArticulosAMostrarPorPagina : ICUArticulosAMostrarPorPagina
    {
        public IRepositorioArticulos Repo { get; set; }


        public CUArticulosAMostrarPorPagina(IRepositorioArticulos repo)
        {
            Repo = repo;
        }

        public List<DTOListarArticulos> ArticulosPorPagina(int pagina)
        {
            List<Articulo> articulos = Repo.ArticulosAMostrarPorPagina(pagina);
            return MapperArticulos.ToDTOListarArticulos(articulos);
        }
    }
}
