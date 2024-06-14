using DTOs;
using LogicaAplicacion.InterfacesCasosDeUso.Genericas;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.CasosDeUsoTipoDeMovimiento
{
    public class CUListarTiposDeMovimiento : ICUListar<DTOTipoDeMovimiento>
    {
        public IRepositorioTipoDeMovimiento Repo { get; set; }

        public CUListarTiposDeMovimiento(IRepositorioTipoDeMovimiento repo)
        {
            Repo = repo;
        }

        public List<DTOTipoDeMovimiento> ObtenerListado()
        {
            List<TipoDeMovimiento> tiposDeMovimiento = Repo.GetAll();
            return MapperTipoDeMovimiento.ToListarDTOTipoDeMovimientos(tiposDeMovimiento);

        }
    }
}
