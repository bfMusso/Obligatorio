using DTOs;
using LogicaAplicacion.InterfacesCasosDeUso.MovimientoDeStock;
using LogicaDatos.Migrations;
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
    public class CUCantidadMovimientosPorTipoYFecha : ICUCantidadMovimientosPorTipoYFecha<DTOCantidad>
    {
        public IRepositorioMovimientoDeStock Repo { get; set; }


        public CUCantidadMovimientosPorTipoYFecha(IRepositorioMovimientoDeStock repo)
        {
            Repo = repo;
        }

        public List<DTOCantidad> cantidadMovPorTipoyFecha()
        {
            List<DTOCantidad> dTOCantidad = new List<DTOCantidad>();    
            dTOCantidad = MapperCantidad.ToDTOCantidad(Repo.CantidadesPorTipoYFecha());            
            return dTOCantidad;
        }
    }
}
