using DTOs;
using LogicaAplicacion.InterfacesCasosDeUso.Genericas;
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
    public class CUAltaMovimientoDeStock : ICUAlta<DTOMovimientoDeStock>
    {

        public IRepositorioMovimientoDeStock RepoMovimiento { get; set; }

        //public RepositorioArticulosEF RepoArticulo { get; set; }

        //public RepositorioUsuariosEF RepoUsuario { get; set; }

        public CUAltaMovimientoDeStock(IRepositorioMovimientoDeStock repoMovimiento) //, RepositorioArticulosEF repoArticulo, RepositorioUsuariosEF repousuario
        {
            RepoMovimiento = repoMovimiento;
            //RepoArticulo = repoArticulo;
            //RepoUsuario = repousuario;
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
