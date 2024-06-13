using DTOs;
using LogicaNegocio.Dominio;

namespace MVC_Insumos.Models
{
    public class PedidoAltaViewModel
    {

        public DateTime FechaDeEntrega { get; set; }

        public int ArticuloId { get; set; }

        public int ArticuloCantidad { get; set; }

        public List<DTOListarArticulos>? Articulos { get; set; }

        public string Tipo { get; set; }

        public int ClienteId { get; set; }

        public List<DTOCliente>? Clientes { get; set; }

    }
}
