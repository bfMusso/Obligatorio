using DTOs;
using LogicaAplicacion.InterfacesCasosDeUso.Genericas;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.CasosDeUsoTipoDeMovimiento
{
    public class CUBajaTipoDeMovimiento : ICUBaja<DTOTipoDeMovimiento>
    {
        public IRepositorioTipoDeMovimiento Repo { get; set; }

        public CUBajaTipoDeMovimiento(IRepositorioTipoDeMovimiento repo)
        {
            Repo = repo;
        }

        public void Baja(int id)
        {
            Repo.Remove(id);
        }
    }
}
