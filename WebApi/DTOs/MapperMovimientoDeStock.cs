using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class MapperMovimientoDeStock
    {

        public static MovimientoDeStock ToMovimientoDeStock(DTOMovimientoDeStock dto)
        {

            MovimientoDeStock movimiento = new MovimientoDeStock()
            {
                //Id = dto.Id, //EF deberia poder asignar el Id sin este atributo mapeado

                FechaYHora = dto.FechaYHora,

                ArticuloDeMovimiento = new Articulo()
                {
                    Id = dto.ArticuloDeMovimiento
                },

                CantidadArticulo = dto.CantidadArticulo,

                UsuarioDeMovimiento = new Usuario()
                {
                    Id = dto.UsuarioDeMovimiento
                },

                Tipo = new TipoDeMovimiento()
                {
                    Id = dto.Tipo
                }

            };

            return movimiento;
        }


        public static DTOMovimientoDeStock ToDTOMovimientoDeStock(MovimientoDeStock obj)
        {

            DTOMovimientoDeStock DTOmovimiento = new DTOMovimientoDeStock()
            {
                //Id = dto.Id, //EF deberia poder asignar el Id sin este atributo mapeado

                FechaYHora = obj.FechaYHora,

                ArticuloDeMovimiento = obj.ArticuloDeMovimiento.Id,

                CantidadArticulo = obj.CantidadArticulo,

                UsuarioDeMovimiento = obj.UsuarioDeMovimiento.Id,

                Tipo = obj.Tipo.Id

            };

            return DTOmovimiento;
        }


    }
}
