using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosDeUso.Usuario
{
    public interface ICUBuscarConMail<Usuario>
    {
        Usuario BuscarUsuarioConMail(string mail);
    }
}
