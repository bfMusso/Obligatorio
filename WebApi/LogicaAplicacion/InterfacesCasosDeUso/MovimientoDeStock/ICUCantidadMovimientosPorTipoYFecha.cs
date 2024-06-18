using DTOs;
using LogicaDatos.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosDeUso.MovimientoDeStock
{
    public interface ICUCantidadMovimientosPorTipoYFecha <T>
    {
        T cantidadMovPorTipoyFecha(int anio, int tipo);
    }
}
