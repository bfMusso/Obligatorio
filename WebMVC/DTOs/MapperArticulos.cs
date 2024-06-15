using LogicaNegocio.Dominio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DTOs
{
    public class MapperArticulos
    {
       
        //De un DTO a un Articulo
        public static Articulo ToArticulo(DTOListarArticulos dto)
        {
            Articulo articulo = new Articulo()
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                CodigoProveedor = dto.CodigoProveedor,
                Stock = dto.Stock,
                PrecioVenta = dto.PrecioVenta
            };
            return articulo;
        }

        //De un articulo  a un DTO
        public static DTOListarArticulos ToDTOListarArticulos(Articulo dto)
        {
            DTOListarArticulos dtoMapeado = new DTOListarArticulos()
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                CodigoProveedor = dto.CodigoProveedor,
                Stock = dto.Stock,
                PrecioVenta = dto.PrecioVenta
            };
            return dtoMapeado;
        }

        //De una lista de Articulos a una lista de DTOs
        public static List<DTOListarArticulos> ToDTOListarArticulos(List<Articulo> articulos) 
        {
            //Transformar la lista de Articulos a DTOListarArticulos
            return articulos.Select(art => new DTOListarArticulos
            {
                Id = art.Id,
                Nombre = art.Nombre,
                Descripcion = art.Descripcion,
                CodigoProveedor = art.CodigoProveedor,
                Stock = art.Stock,
                PrecioVenta = art.PrecioVenta

            }).ToList();
        }

        //De una lista de DTOs a una lista de Articulos
        public static List<Articulo> ToArticulosListarDTO(List<DTOListarArticulos> dtos)
        {
            //Transformar la lista de Articulos a DTOListarArticulos
            return dtos.Select(dto => new Articulo
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                CodigoProveedor = dto.CodigoProveedor,
                Stock = dto.Stock,
                PrecioVenta = dto.PrecioVenta

            }).ToList();
        }

    }
}
