
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class DTOListaResumen
    {
        public int Anio { get; set; }
        public List<DTOResumen> CantidadesPorTipo { get; set; }
    }
}
