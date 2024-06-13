using DTOs;
using LogicaNegocio.Dominio;

namespace MVC_Insumos.Models
{
    public class PedidoNuevaLineaViewModel
    {

        public int Id { get; set; }

        public int ArticuloId { get; set; }

        public int ArticuloCantidad { get; set; }

        public List<DTOListarArticulos>? Articulos { get; set; }


    }
}
