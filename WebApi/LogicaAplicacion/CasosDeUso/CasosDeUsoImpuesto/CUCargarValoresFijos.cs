using LogicaAplicacion.InterfacesCasosDeUso.Impuesto;
using LogicaNegocio.Dominio;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.CasosDeUsoImpuesto
{
    public class CUCargarValoresFijos : ICUCargarValoresFijos<ValorFijo>
    {
       

        public IRepositorioValoresFijos Repo { get; set; }

        public CUCargarValoresFijos(IRepositorioValoresFijos repo)
        {
            Repo = repo;
        }


        public void Cargar(string valor)
        {
            /*
             switch (impuesto)
            {
                case "IVA":
                    Pedido.TasaIVA = Repo.CargarImpuestos(impuesto).Valor;
                    break;

                case "Plazo maximo":
                    // Se parsea el impuesto decimal a int
                    Express.PlazoMaximo = (int)Repo.CargarImpuestos(impuesto).Valor;
                    break;

                case "RecargoMinimo":
                    // Se parsea el impuesto decimal a int
                    Express.RecargoMinimo = (int)Repo.CargarImpuestos(impuesto).Valor;
                    break;

                case "RecargoMaximo":
                    // Se parsea el impuesto decimal a int
                    Express.RecargoMaximo = (int)Repo.CargarImpuestos(impuesto).Valor;
                    break;

                case "RecargoComun":
                    // Se parsea el impuesto decimal a int
                    Comun.RecargoComun = (int)Repo.CargarImpuestos(impuesto).Valor;
                    break;

                default:
                    throw new ExcepcionCustomException("Error inesperado");
                                              
            }
             */
        }
    }
}
