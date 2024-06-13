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

    public class Articulo : IValidable
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripcion es obligatoria")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "La descripcion debe tener un mínimo de 5 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Codigo de proveedor es obligatorio")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "El Código de proveedor debe tener 13 dígitos.")]
        public string CodigoProveedor { get; set; }

        [Required(ErrorMessage = "Stock es obligatorio")]
        [MaxLength(50, ErrorMessage = "La longitud máxima es de 50 caracteres.")]
        [Range(0, int.MaxValue, ErrorMessage = "El valor de stock no puede ser menor a 0")]
        public decimal Stock { get; set; }

        [Required(ErrorMessage = "Precio de venta es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El valor de precio de venta no puede ser menor a 0")]
        public decimal PrecioVenta { get; set; }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Nombre) || Nombre.Length < 10 || Nombre.Length > 200)
                throw new ExcepcionCustomException("El nombre es obligatorio y su largo debe ser mayor a 10 y menor a 200 caracteres.");

            if (string.IsNullOrEmpty(Descripcion) || Descripcion.Length < 5)
                throw new ExcepcionCustomException("La Descripcion es obligatoria y su largo debe ser mayor a 5 caracteres.");

            if (string.IsNullOrEmpty(CodigoProveedor))
                throw new ExcepcionCustomException("Codigo de proveedor es obligatorio");

            if (Stock <0)
                throw new ExcepcionCustomException("Se debe indicar Stock, y debe ser como minimo 0.");

            if (PrecioVenta <= 0)
                throw new ExcepcionCustomException("Se debe indicar precio de Venta, y debe ser un valor mayor a 0.");
            
            //Controlar codigo de proveedor
            CodigoValido(CodigoProveedor);

        }

        private void CodigoValido(string codigoProveedor)
        {
            if (codigoProveedor.Length < 13)
                throw new ExcepcionCustomException("El codigo de proveedor debe tener un minimo de 13 numeros.");

            foreach (char letra in codigoProveedor) {
                //Se quita filtro de texto y se deja solo numericos
                if (!char.IsDigit(letra)) {
                    throw new ExcepcionCustomException("Solo se admiten numeros del 0 al 9.");
                }
            }

        }

    }
}
