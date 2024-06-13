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
    public class CUBuscarArticuloPorId : ICUBuscarPorId<Articulo>
    {
        IRepositorioArticulos Repo { get; set; }

        public CUBuscarArticuloPorId(IRepositorioArticulos repo)
        {
            Repo = repo;
        }

        public Articulo Buscar(int id)
        {
            return Repo.GetById(id);
        }
    }
}
