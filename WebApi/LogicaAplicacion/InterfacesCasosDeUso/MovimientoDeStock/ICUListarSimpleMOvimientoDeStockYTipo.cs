﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosDeUso.MovimientoDeStock
{public interface ICUListarSimpleMOvimientoDeStockYTipo <T>
    {
        List<T> ListarMovimientosDeStockYTipo(int pagina);

    }
}
