using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosDeUso.MovimientoDeStock
{
    public interface ICUListarMovimientosDeStock<T>
    {
        List<T> Listar();
    }
}
