using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosDeUso.MovimientoDeStock
{
    public interface ICUListarArticulosEnMovimientosEntreFechas<T>
    {
        T ListarArticulosEntreFechas(DateTime fecha1, DateTime fecha2, int pagina);
    }
}
