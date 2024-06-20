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

        public IRepositorioUsuarios RepoUsuario { get; set; }

        public IRepositorioArticulos RepoArticulo { get; set; }

        public IRepositorioTipoDeMovimiento RepoTipoDeMovimiento { get; set; }
        public IRepositorioValoresFijos RepoValoresFijos { get; set; }


        public CUAltaMovimientoDeStock(IRepositorioMovimientoDeStock repoMovimiento, IRepositorioUsuarios repoUsuario, IRepositorioArticulos repoArticulo, IRepositorioTipoDeMovimiento repoTipoDeMovimiento, IRepositorioValoresFijos repoValoresFijos) 
        {
            RepoMovimiento = repoMovimiento;
            RepoUsuario = repoUsuario;
            RepoArticulo = repoArticulo;
            RepoTipoDeMovimiento = repoTipoDeMovimiento;
            RepoValoresFijos = repoValoresFijos;
        }

        public void Alta(DTOMovimientoDeStock dto)
        {
            //Mapeamos a Objeto MovimientoDeStock desde DTO
            MovimientoDeStock movimientoDeStock = MapperMovimientoDeStock.ToMovimientoDeStock(dto);
            Articulo art = RepoArticulo.GetById(dto.ArticuloDeMovimientoId);
            Usuario usu = RepoUsuario.GetById(dto.UsuarioDeMovimiento);
            TipoDeMovimiento tipo = RepoTipoDeMovimiento.GetById(dto.TipoDeMovimientoId);

            //IMPLEMENTAR IF DTO.CANTIDAD > VALOR FIJO se va por la excepcion.
            ValorFijo tope = RepoValoresFijos.CargarValores("Tope");

            if (dto.CantidadArticulo > tope.Valor)
            {
                throw new ExcepcionCustomException("Se sobrepasó el tope permitido.");
            }

            //Si no es null hacemos el Add al repo
            if (movimientoDeStock != null)
            {
                movimientoDeStock.ArticuloDeMovimiento = art;
                movimientoDeStock.UsuarioDeMovimiento = usu;
                movimientoDeStock.Tipo = tipo;
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
