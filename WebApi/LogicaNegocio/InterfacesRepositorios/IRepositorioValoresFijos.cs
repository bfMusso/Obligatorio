using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioValoresFijos
    {
        //metodo traerimpuesto

        public ValorFijo CargarValores(string id);

        public int ObtenerTopePaginas();
    
    }
}
