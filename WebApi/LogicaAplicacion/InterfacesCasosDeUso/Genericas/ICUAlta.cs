using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosDeUso.Genericas
{
    public interface ICUAlta<T>
    {
        void Alta(T obj);
    }
}
