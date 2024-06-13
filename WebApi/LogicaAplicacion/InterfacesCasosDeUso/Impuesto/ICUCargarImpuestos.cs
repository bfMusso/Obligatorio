using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosDeUso.Impuesto
{
    public interface ICUCargarImpuestos<T>
    {
        void Cargar(string id);
    }
}
