using DTOs;
using LogicaAplicacion.InterfacesCasosDeUso.Genericas;
using LogicaDatos.Repositorios;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.CasosDeUsoMovimientosDeStock
{
    public class CUBuscarMovimientoDeStockPorId : ICUBuscarPorId<DTOMovimientoDeStock>
    {
        public IRepositorioMovimientoDeStock Repo { get; set; }


        public CUBuscarMovimientoDeStockPorId(IRepositorioMovimientoDeStock repoBuscarPorId)
        {
            Repo = repoBuscarPorId;
        }

        public DTOMovimientoDeStock Buscar(int id)
        {
            MovimientoDeStock movimientoEncontrado = Repo.GetById(id);
            return MapperMovimientoDeStock.ToDTOMovimientoDeStock(movimientoEncontrado);
        }
    }
}
