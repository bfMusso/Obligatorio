using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class DTOMovimientoStockYTipo
    {
        public int Id { get; set; }

        public DateTime FechaYHora { get; set; }

        public int ArticuloDeMovimiento { get; set; }

        public int CantidadArticulo { get; set; }

        public int UsuarioDeMovimiento { get; set; }

        public int Tipo { get; set; }

        public string Nombre { get; set; }

        public bool tipoDeCambioEnStock { get; set; }

        public int CantidadItemsTotales { get; set; }

    }
}
