using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosDeUso.MovimientoDeStock
{
    public interface ICUListarMovimientosYTipos<T>
    {
        T ListarMovimientosYTipos(int idArticulo, int idTipo, int pagina);
    }
}

