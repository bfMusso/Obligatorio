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
    public class CUAltaUsuario : ICUAlta<Usuario>
    {
        public IRepositorioUsuarios Repo { get; set; }

        public CUAltaUsuario(IRepositorioUsuarios repo) {
            Repo = repo;
        }

        public void Alta(Usuario obj)
        {
            obj.PasswordEnctriptado = Repo.EncriptarPassword(obj.Password);
            Repo.Add(obj);
        }
    }
}
