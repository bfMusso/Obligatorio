using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class DTOMovimientoStockYTipoPaginado
    {
        public List<DTOMovimientoStockYTipo> MovimientosStockYTipo { get; set;}

        public int PaginasTotales {  get; set;}      
    }
}
