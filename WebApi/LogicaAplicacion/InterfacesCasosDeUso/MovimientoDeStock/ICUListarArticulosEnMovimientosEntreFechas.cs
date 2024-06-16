using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosDeUso.MovimientoDeStock
{
    public interface ICUListarArticulosEnMovimientosEntreFechas<T>
    {
        List<T> ListarArticulosEntreFechas(DateTime fecha1, DateTime fecha2);
    }
}
