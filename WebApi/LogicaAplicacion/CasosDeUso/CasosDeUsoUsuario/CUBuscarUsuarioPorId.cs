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
    public class CUBuscarUsuarioPorId : ICUBuscarPorId<Usuario>
    {
        IRepositorioUsuarios Repo { get; set; }

        public CUBuscarUsuarioPorId(IRepositorioUsuarios repo)
        {
            Repo = repo;
        }

        public Usuario Buscar(int id)
        {
            return Repo.GetById(id);
        }

    }
}
