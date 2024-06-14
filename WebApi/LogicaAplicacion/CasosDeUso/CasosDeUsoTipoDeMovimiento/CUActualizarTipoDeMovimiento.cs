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
    public class CUActualizarTipoDeMovimiento : ICUActualizar<DTOTipoDeMovimiento>
    {
        public IRepositorioTipoDeMovimiento Repo { get; set; }

        public CUActualizarTipoDeMovimiento(IRepositorioTipoDeMovimiento repo)
        {
            Repo = repo;
        }

        public void Actualizar(DTOTipoDeMovimiento dto)
        {
            TipoDeMovimiento obj = MapperTipoDeMovimiento.ToTipoDeMovimiento(dto);
            Repo.Update(obj);
        }
    }
}
