using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosDeUso.Genericas
{
    public interface ICUBuscarPorId<T>
    {
        T Buscar(int id);
    }
}
