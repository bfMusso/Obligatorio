using DTOs;
using LogicaAplicacion.InterfacesCasosDeUso.Genericas;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.CasosDeUsoUsuario
{
    public class CUListarUsuarios : ICUListar<DTORoles>
    {
        public IRepositorioUsuarios Repo { get; set; }

        public CUListarUsuarios(IRepositorioUsuarios repo)
        {
            Repo = repo;
        }

        public List<DTORoles> ObtenerListado()
        {
            List<Usuario> usuarios = Repo.GetDistinctAll();
            return MapperUsuarios.ToListarDTORoles(usuarios);
        }
    }
}
