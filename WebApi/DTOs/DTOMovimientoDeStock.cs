using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LogicaNegocio.Dominio.MovimientoDeStock;

namespace DTOs
{
    public class DTOMovimientoDeStock
    {
        public int Id { get; set; }

        public DateTime FechaYHora { get; set; }

        public int ArticuloDeMovimiento { get; set; }

        public int CantidadArticulo { get; set; }

        public int UsuarioDeMovimiento { get; set; }

        public int Tipo { get; set; }

    }
}
