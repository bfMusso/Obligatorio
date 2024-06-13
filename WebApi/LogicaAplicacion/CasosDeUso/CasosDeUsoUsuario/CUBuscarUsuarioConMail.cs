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
    public class CUBuscarUsuarioConMail : ICUBuscarConMail<Usuario>
    {
        public IRepositorioUsuarios Repo { get; set; }

        public CUBuscarUsuarioConMail(IRepositorioUsuarios repo)
        {
            Repo = repo;
        }

        public Usuario BuscarUsuarioConMail(string mail)
        {
            return Repo.TraerUsuarioConMail(mail);
        }
    }
}
