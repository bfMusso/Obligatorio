using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class MapperTipoDeMovimiento
    {

        public static TipoDeMovimiento ToTipoDeMovimiento(DTOTipoDeMovimiento dto)
        {
            TipoDeMovimiento tipoDeMovimiento = new TipoDeMovimiento()
            { 
            
                Id = dto.Id,

                Nombre = dto.Nombre,

                tipoDeCambioEnStock = dto.tipoDeCambioEnStock

            };

            return tipoDeMovimiento; 
        }


        public static DTOTipoDeMovimiento ToDTOTipoDeMovimiento(TipoDeMovimiento obj)
        {
            DTOTipoDeMovimiento dto = new DTOTipoDeMovimiento() {

                Id = obj.Id,

                Nombre = obj.Nombre,

                tipoDeCambioEnStock = obj.tipoDeCambioEnStock

            };

            return dto; 
        }

        public static List<DTOTipoDeMovimiento> ToListarDTOTipoDeMovimientos(List<TipoDeMovimiento> tipoDeMovimientos)
        { 
            return tipoDeMovimientos.Select(tipo => new DTOTipoDeMovimiento
            { 
                    Id = tipo.Id,

                    Nombre= tipo.Nombre,

                    tipoDeCambioEnStock = tipo.tipoDeCambioEnStock
            }).ToList();
        
        }


        public static List<TipoDeMovimiento> ToListarTipoDeMovimientos(List<DTOTipoDeMovimiento> DTOs)
        {
            return DTOs.Select(dto => new TipoDeMovimiento
            {
                Id = dto.Id,

                Nombre = dto.Nombre,

                tipoDeCambioEnStock = dto.tipoDeCambioEnStock
                
            }).ToList();
        
        }



    }
}
