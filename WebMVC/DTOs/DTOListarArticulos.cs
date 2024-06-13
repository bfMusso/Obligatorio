using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class DTOListarArticulos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string CodigoProveedor { get; set; }

        public decimal Stock { get; set; }

        public decimal PrecioVenta { get; set; }

    }
}
