using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class DTOMovimientoDeStock
    {
        public int Id { get; set; }

        public DateTime FechaYHora { get; set; }

        public int CantidadArticulo { get; set; }

        public int ArticuloDeMovimientoId { get; set; }

        public int UsuarioDeMovimiento { get; set; }

        public int TipoDeMovimientoId { get; set; }

        public List<DTOArticulos> Articulos { get; set; } = new List<DTOArticulos> { }; 
        public List<DTOTipoDeMovimiento> TiposDeMovimiento { get; set; } = new List<DTOTipoDeMovimiento> { };

    }
}
