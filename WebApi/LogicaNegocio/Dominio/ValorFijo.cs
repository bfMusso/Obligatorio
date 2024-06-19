using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Dominio
{
    public class ValorFijo:IValidable
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Valor es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El valor no puede ser menor a 0")]
        public decimal Valor { get; set; }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Nombre))
                throw new ExcepcionCustomException("El nombre es obligatorio.");  

            if (Valor < 0)
                throw new ExcepcionCustomException("El valor no puede ser negativo");
            
        }
    }
}
