using LogicaAplicacion.CasosDeUso.CasosDeUsoUsuario;
using LogicaAplicacion.InterfacesCasosDeUso.Impuesto;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.CasosDeUsoValoresFijos
{
    public class CUObtenerTotalPaginado : ICUObtenerTotalPaginas
    {
        IRepositorioValoresFijos Repo { get; set; }

        public CUObtenerTotalPaginado(IRepositorioValoresFijos repo)
        {
            Repo = repo;
        }

        public int ObtenerPaginado()
        {        
            return Repo.ObtenerTopePaginas();
        }
    }
}
