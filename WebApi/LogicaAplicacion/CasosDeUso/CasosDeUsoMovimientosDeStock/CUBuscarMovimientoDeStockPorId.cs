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
    public class CUBuscarMovimientoDeStockPorId : ICUBuscarMovimientoPorId<DTOMovimientoDeStock>
    {
        public IRepositorioMovimientoDeStock Repo { get; set; }


        public CUBuscarMovimientoDeStockPorId(IRepositorioMovimientoDeStock repo)
        {
            Repo = repo;
        }

        public DTOMovimientoDeStock Buscar(int id)
        {
            MovimientoDeStock movimientoEncontrado = Repo.GetById(id);

            if (movimientoEncontrado != null)
            {
                return MapperMovimientoDeStock.ToDTOMovimientoDeStock(movimientoEncontrado);

            }
            else
            {
                throw new ExcepcionCustomException("No se encontro el movimiento de stock correspondiente al Id: " + id);
            }
           
        }
    }
}
