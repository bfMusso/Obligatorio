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

                CantidadArticulo = dto.CantidadArticulo,

            };

            return movimiento;
        }


        public static DTOMovimientoDeStock ToDTOMovimientoDeStock(MovimientoDeStock obj)
        {

            DTOMovimientoDeStock DTOmovimiento = new DTOMovimientoDeStock()
            {
                //Id = dto.Id, //EF deberia poder asignar el Id sin este atributo mapeado

                FechaYHora = obj.FechaYHora,

                ArticuloDeMovimientoId = obj.ArticuloDeMovimiento.Id,

                CantidadArticulo = obj.CantidadArticulo,

                UsuarioDeMovimiento = obj.UsuarioDeMovimiento.Id,

                TipoDeMovimientoId = obj.Tipo.Id

            };

            return DTOmovimiento;
        }




        //Para listar los movimientos
        public static List<MovimientoDeStock> ToMovimientosDeStock(List<DTOMovimientoDeStock> dtos)
        {
            return dtos.Select(dto => new MovimientoDeStock
            {
                //Id = dto.Id, //EF deberia poder asignar el Id sin este atributo mapeado

                FechaYHora = dto.FechaYHora,

                ArticuloDeMovimiento = new Articulo
                {
                    Id = dto.ArticuloDeMovimientoId,
                },

                CantidadArticulo = dto.CantidadArticulo,

                UsuarioDeMovimiento = new Usuario
                {
                    Id = dto.UsuarioDeMovimiento
                },

                Tipo = new TipoDeMovimiento
                {
                    Id = dto.TipoDeMovimientoId
                }

            }).ToList();
        }

        public static List<DTOMovimientoDeStock> ToDTOSMovimientosDeStock(List<MovimientoDeStock> MovimientosDeStock)
        {

            return MovimientosDeStock.Select(obj => new DTOMovimientoDeStock
            {
                //Id = dto.Id, //EF deberia poder asignar el Id sin este atributo mapeado

                FechaYHora = obj.FechaYHora,

                ArticuloDeMovimientoId = obj.ArticuloDeMovimiento.Id,

                CantidadArticulo = obj.CantidadArticulo,

                UsuarioDeMovimiento = obj.UsuarioDeMovimiento.Id,

                TipoDeMovimientoId = obj.Tipo.Id


            }).ToList();

        }
    }
}