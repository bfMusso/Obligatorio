using LogicaNegocio.Dominio;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDatos.Repositorios
{
    public class RepositorioImpuestosEF : IRepositorioImpuestos

    {
        public LibreriaContext Contexto { get; set; }

        public RepositorioImpuestosEF(LibreriaContext ctx)
        {

            Contexto = ctx;
        }

        public Impuesto CargarImpuestos(string impuesto)
        {
            Impuesto encontrado = Contexto.Impuestos.SingleOrDefault(imp => imp.Nombre == impuesto);

            if (encontrado == null)
            {
                throw new ExcepcionCustomException("No se encuentra el impuesto " + impuesto);
            }

            return encontrado;
        }
    }
}
