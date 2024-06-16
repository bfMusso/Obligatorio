using DTOs;

namespace WebMVC.Models
{
    public class MovimientoDeStockViewModel
    {
        public int Id { get; set; }

        public DateTime FechaYHora { get; set; }

        public int ArticuloId { get; set; }

        public List<DTOArticulos>? Articulos { get; set; }

        public int CantidadArticulo { get; set; }

        public int UsuarioId { get; set; }

        public List<DTOUsuario>? Usuarios { get; set; }

        public int TipoDeMovimientoId { get; set; }

        public List<DTOTipoDeMovimiento>? TiposDeMovimiento { get; set; }
    }
}
