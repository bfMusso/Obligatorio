using DTOs;
using LogicaAplicacion.InterfacesCasosDeUso.Genericas;
using LogicaAplicacion.InterfacesCasosDeUso.MovimientoDeStock;
using LogicaDatos.Repositorios;
using LogicaNegocio.Dominio;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.CasosDeUsoMovimientosDeStock
{
    public class CUAltaMovimientoDeStock : ICUAltaMovimientoDeStock<DTOMovimientoDeStock>
    {

        public IRepositorioMovimientoDeStock RepoMovimiento { get; set; }


        public CUAltaMovimientoDeStock(IRepositorioMovimientoDeStock repoMovimiento) 
        {
            RepoMovimiento = repoMovimiento;
        }

        public void Alta(DTOMovimientoDeStock dto)
        {
            //Mapeamos a Objeto MovimientoDeStock desde DTO
            MovimientoDeStock movimientoDeStock = MapperMovimientoDeStock.ToMovimientoDeStock(dto);
            //Si no es null hacemos el Add al repo
            if (movimientoDeStock != null)
            {
                RepoMovimiento.Add(movimientoDeStock);
            }
            //Si es null tiramos una excepcion
            else
            {
                throw new ExcepcionCustomException("No se pudo realiazar el alta de movimiento de stock.");
            }

        }
    }
}
