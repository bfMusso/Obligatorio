using LogicaAplicacion.InterfacesCasosDeUso.Articulo;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.CasosDeUsoArticulo
{
    public class CUCantidadTotalArticulos :ICUCantidadTotalArticulos
    {
        public IRepositorioArticulos Repo { get; set; }


        public CUCantidadTotalArticulos(IRepositorioArticulos repo)
        {
            Repo = repo;
        }

        public int CantidadTotalArticulos()
        {
            return Repo.CantidadTotalDeArticulos();
        }
    }
}
