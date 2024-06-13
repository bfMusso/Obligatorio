using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosDeUso.Usuario
{
    public interface ICULogin<Usuario>
    {
        Usuario Login(string mail, string password);
    }
}
