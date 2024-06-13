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
    public class CUAltaArticulo : ICUAlta<Articulo>
    {
        public IRepositorioArticulos Repo { get; set; }

        public CUAltaArticulo(IRepositorioArticulos repo)
        {
            Repo = repo;
        }

        public void Alta(Articulo obj)
        {
            //Se agrega
            Repo.Add(obj);
        }

    }
 }
