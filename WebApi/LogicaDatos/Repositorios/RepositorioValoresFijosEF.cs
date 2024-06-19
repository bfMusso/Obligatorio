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
    public class RepositorioValoresFijosEF : IRepositorioValoresFijos

    {
        public LibreriaContext Contexto { get; set; }

        public RepositorioValoresFijosEF(LibreriaContext ctx)
        {

            Contexto = ctx;
        }
        
        public ValorFijo CargarValores(string valor)
        {
             ValorFijo encontrado = Contexto.ValoresFijos.SingleOrDefault(imp => imp.Nombre == valor);

             if (encontrado == null)
             {
                 throw new ExcepcionCustomException("No se encuentra el impuesto " + valor);
             }

             return encontrado;            
        }
    }
}
