using DTOs;
using LogicaAplicacion.InterfacesCasosDeUso.Genericas;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.CasosDeUsoArticulo
{
    public class CUListarArticulos : ICUListar<DTOListarArticulos>
    {

        public IRepositorioArticulos Repo { get; set; }

        public CUListarArticulos(IRepositorioArticulos repo)
        {

            Repo = repo;
        }

        public List<DTOListarArticulos> ObtenerListado()
        {
            List<DTOListarArticulos> DtosArt = new List<DTOListarArticulos>();
            List<Articulo> articulos = Repo.GetAll();         
            if (articulos.Count > 0)
            {
                DtosArt = MapperArticulos.ToDTOListarArticulos(articulos);               
            }
            return DtosArt;
           // return MapperArticulos.ToDTOListarArticulos(Repo.GetAll());

        }
    }
}
