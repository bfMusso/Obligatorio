using DTOs;
using LogicaAplicacion.InterfacesCasosDeUso.Usuario;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.CasosDeUsoUsuario
{
    public class CUBuscarUsuarioConMail : ICUBuscarConMail<DTOUsuario>
    {
        public IRepositorioUsuarios Repo { get; set; }

        public CUBuscarUsuarioConMail(IRepositorioUsuarios repo)
        {
            Repo = repo;
        }

        public DTOUsuario BuscarUsuarioConMail(string mail)
        {
            return MapperUsuarios.ToDTOUsuario(Repo.TraerUsuarioConMail(mail));
        }
    }
}
