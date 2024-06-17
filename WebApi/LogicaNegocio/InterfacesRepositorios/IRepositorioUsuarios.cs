using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioUsuarios:IRepositorio<Usuario>
    {
        string EncriptarPassword(string password);

        Usuario BuscarPorMail(string mail, string password);

        Usuario TraerUsuarioConMail(string mail);

        bool EmailEsUnico(Usuario obj);
    }
}
