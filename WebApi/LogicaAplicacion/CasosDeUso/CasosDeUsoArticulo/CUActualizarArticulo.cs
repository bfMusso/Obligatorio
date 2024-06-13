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
    public class CUActualizarArticulo : ICUActualizar<Articulo>
    {
        public IRepositorioArticulos Repo { get; set; }

        public CUActualizarArticulo(IRepositorioArticulos repo)
        {
            Repo = repo;
        }

        public void Actualizar(Articulo obj)
        {
            //Actualizar
            Repo.Update(obj);
        }
    }
}
