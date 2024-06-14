using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesDominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Dominio
{
    [Index(nameof(Nombre), IsUnique = true)]
    public class TipoDeMovimiento : IValidable
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe indicar una de las opciones.")]
        public bool tipoDeCambioEnStock { get; set; }

        public void Validar()
        {

            if (string.IsNullOrEmpty(Nombre))
                throw new ExcepcionCustomException("Debe escribir un Nombre.");

            if (tipoDeCambioEnStock == null)
                throw new ExcepcionCustomException("Debe indicar una de las opciones.");

        }
    }

}
