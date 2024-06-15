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
    public class CULoginUsuario : ICULogin<DTOUsuario>
    {

        public IRepositorioUsuarios Repo { get; set; }

        public CULoginUsuario(IRepositorioUsuarios repo)
        {

            Repo = repo;
        }

        public DTOUsuario Login(string mail, string password)
        {
            string encriptado = Repo.EncriptarPassword(password);
            Usuario usuario = Repo.BuscarPorMail(mail, encriptado);

           
            return MapperUsuarios.ToDTOUsuario(usuario); 
        }

    }
}
