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
    [Index(nameof(Email),IsUnique = true)]
    public class Usuario: IValidable
    {

        public enum TipoDeUsuario
        {
            Administrador,
            Encargado

        }

        public int Id { get; set; }

        [Required(ErrorMessage = "El mail es obligatorio")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Se debe ingresar un mail valido.")]     
        public string Email { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        //[a-zA-Z]+ Letra may o min del alfabeto, ['\s\-] comillas simples, espacio en blanco o guion, [a-zA-Z](Lo mismo en caso de nombre ej. Anna Lia, etc., * Ninguna o mas de una repeticion de lo anterior.
        [RegularExpression(@"^(?:[a-zA-Z]+(?:['\s\-][a-zA-Z]+)*)$", ErrorMessage = "El Nombre debe contener solo caracteres alfabéticos, espacios, apóstrofes o guiones, sin caracteres no alfabéticos al principio o al final.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Apellido es obligatorio")]
        //[a-zA-Z]+ Letra may o min del alfabeto, ['\s\-] comillas simples, espacio en blanco o guion, [a-zA-Z](Lo mismo en caso de nombre ej. Anna Lia, etc., * Ninguna o mas de una repeticion de lo anterior.
        [RegularExpression(@"^(?:[a-zA-Z]+(?:['\s\-][a-zA-Z]+)*)$", ErrorMessage = "El apelllido debe contener solo caracteres alfabéticos, espacios, apóstrofes o guiones, sin caracteres no alfabéticos al principio o al final.")]
        public string Apellido { get; set; }

        //public Tipo UsuarioTipo { get; set; } 

        //[Required(ErrorMessage = "El Password es obligatorio.")]
        //(?=.*[A-Z]) Al menos una letra may, (?=.*[a-z]) al menos una letra min, (?=.*\d) al menos un digito, [\.;,!A-Za-z\d] Limita todo a letras may,min, digito o ".", ";", "!", o ",".
        //[MinLength(6, ErrorMessage = "La contraseña debe tener un mínimo de 6 caracteres.")]
        [Required(ErrorMessage = "La contraseña no puede ser vacia"), MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres"), DataType(DataType.Password), RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[.,;!]).{6,}$", ErrorMessage = "La contraseña debe tener al menos una letra mayúscula, una minúscula, un dígito y un carácter de puntuación: punto, punto y\r\ncoma, coma, signo de admiración de cierre")]
        public string Password { get; set; }

        //Metodo hash de enctriptamiento
        public String PasswordEnctriptado { get; set; }

        [Required(ErrorMessage = "El campo tipo de usuario es obligatorio.")]
        public TipoDeUsuario? TipoUsuario {  get; set; }


        public void Validar()
        {

            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Apellido) || string.IsNullOrEmpty(Password) || !TipoUsuario.HasValue) {

                throw new ExcepcionCustomException("Todos los campos son obligatorios.");

            }

            ValidarTexto(Nombre);

            ValidarTexto(Apellido);

            ValidarPassword(Password);

        }
        //public enum Tipo { Autenticado, SinAutenticar }

        private void ValidarPassword(string password)
        {
            //Variables de validacion
            bool cMayucula = false;
            bool cMinuscula = false;
            bool cNumerico = false;
            bool cPuntuacion = false;
            int longitud = password.Length;

            foreach (char caracter in password)
            {
                if (char.IsUpper(caracter))
                {
                    cMayucula = true;
                }
                if (char.IsLower(caracter))
                {
                    cMinuscula = true;
                }
                if (char.IsDigit(caracter))
                {
                    cNumerico = true;
                }
                if (caracter == '.' || caracter == ';' || caracter == ',' || caracter == '!')
                {
                    cPuntuacion = true;
                }
            }

            if (!cMayucula || !cMinuscula || !cNumerico || !cPuntuacion || longitud < 6)
            {
                throw new ExcepcionCustomException("Debe ingresar una contraseña que tenga un largo mínimo de 6 caracteres, al menos una letra mayúscula, una minúscula, un dígito y un carácter de puntuación: punto, punto y coma, coma, signo de admiración de cierre");
            }
        }

        private void ValidarTexto(string texto)
        {
            // Verificar si el texto es nulo o vacío
            if (string.IsNullOrEmpty(texto))
            {
                throw new ExcepcionCustomException("El texto no puede estar vacío.");
            }

            // Verificar el primer y último carácter
            if (!char.IsLetter(texto[0]) || texto[0] == '\'' || texto[0] == '-' || texto[0] == ' ')
            {
                throw new ExcepcionCustomException("Inicio y final no pueden ser ',- ni espacio en blanco, debe ser alfabético!");
            }

            if (!char.IsLetter(texto[texto.Length - 1]) || texto[texto.Length - 1] == '\'' || texto[texto.Length - 1] == '-' || texto[texto.Length - 1] == ' ')
            {
                throw new ExcepcionCustomException("Inicio y final no pueden ser ',- ni espacio en blanco, debe ser alfabético!");
            }

            // Verificar todos los caracteres intermedios
            for (int i = 1; i < texto.Length - 1; i++)
            {
                char letra = texto[i];
                if (!char.IsLetter(letra) && letra != '\'' && letra != '-' && letra != ' ')
                {
                    throw new ExcepcionCustomException("Ningún carácter puede ser distinto a alfabético, ', - o espacio en blanco!");
                }
            }


        }


    }
}
