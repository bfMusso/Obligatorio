using DTOs;
using LogicaAplicacion.InterfacesCasosDeUso.MovimientoDeStock;
using LogicaDatos.Migrations;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.CasosDeUsoMovimientosDeStock
{
    public class CUListarMovimientosDeStock : ICUListarMovimientosDeStock<DTOMovimientoDeStock>
    {
        public IRepositorioMovimientoDeStock Repo { get; set; }

        public CUListarMovimientosDeStock(IRepositorioMovimientoDeStock repo)
        {

            Repo = repo;
        }

        public List<DTOMovimientoDeStock> Listar()
        {
            List<DTOMovimientoDeStock> DtosMovimientos = new List<DTOMovimientoDeStock>();
            List<MovimientoDeStock> movimientos = Repo.GetAll();   

            if (movimientos.Count > 0)
            {
                /* 
                 foreach (var movimiento in movimientos)
                {
                    DTOMovimientoDeStock DTOmovimiento = MapperMovimientoDeStock.ToDTOMovimientoDeStock(movimiento);
                    DtosMovimientos.Add(DTOmovimiento);
                }
                 */

                 DtosMovimientos = MapperMovimientoDeStock.ToDTOSMovimientosDeStock(movimientos);
            }
            return DtosMovimientos;
            

        }
        
    }
}
