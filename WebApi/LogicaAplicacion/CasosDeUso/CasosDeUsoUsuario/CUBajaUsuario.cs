using LogicaAplicacion.InterfacesCasosDeUso.Genericas;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.CasosDeUsoUsuario
{
    public class CUBajaUsuario : ICUBaja<Usuario>
    {
        public IRepositorioUsuarios Repo { get; set; }

        public CUBajaUsuario(IRepositorioUsuarios repo)
        {
            Repo = repo;
        }

        public void Baja(int id)
        {
            Repo.Remove(id);
        }
    }
}
