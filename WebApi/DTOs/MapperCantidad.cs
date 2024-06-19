using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class MapperCantidad
    {
        public static List<DTOCantidad> ToDTOCantidad(List<(int, string, int)> listaTuplas)
        {
            return listaTuplas.Select(tipo => new DTOCantidad
            {
                Anio = tipo.Item1,

                Tipo = tipo.Item2,

                Cantidad = tipo.Item3
            }).ToList();
        }
    }
}
