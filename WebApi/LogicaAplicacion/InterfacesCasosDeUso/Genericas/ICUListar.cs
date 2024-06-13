using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosDeUso.Genericas
{
    public interface ICUListar<T>
    {
        List<T> ObtenerListado();
    }
}
