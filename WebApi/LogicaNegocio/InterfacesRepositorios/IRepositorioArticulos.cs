using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioArticulos : IRepositorio<Articulo>
    {
        void NombreEsUnico(string nombre);

        void CodigoUnico(string codigo);

    }
}
