using LogicaNegocio.Excepciones;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LogicaNegocio.OtrasClases
{
  
    public class Direccion
    {
        
        [Required(ErrorMessage = "Calle de direccion requerida.")]
        public string Calle { get; init; }

        [Required(ErrorMessage = "Numero es requerido.")]
        [Range(0, int.MaxValue, ErrorMessage = "Numero no puede ser menor a 0")]
        public int Numero { get; init; }

        [Required(ErrorMessage = "Ciudad requerida.")]
        public string Ciudad { get; init; }

        public Direccion(string calle, int numero, string ciudad)
        {
            Calle = calle;
            Numero = numero;
            Ciudad = ciudad;

            //VALIDACION
            Validar();
        }

        private void Validar() {
            if (string.IsNullOrEmpty(Calle))
                throw new ExcepcionCustomException("Calle es un parametro obligatorio.");

            if ( Numero < 0)
                throw new ExcepcionCustomException("Numero es un dato obligatorio.");

            if (string.IsNullOrEmpty(Ciudad))
                throw new ExcepcionCustomException("Ciudad es un parametro obligatorio.");
        }

        public override bool Equals(object? obj)
        {
            Direccion? otro = obj as Direccion;

            if (otro != null)
            {
                if (otro.Calle == Calle && otro.Numero == Numero && otro.Ciudad == Ciudad)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
