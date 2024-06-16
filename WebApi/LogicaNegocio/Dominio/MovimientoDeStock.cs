using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesDominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LogicaNegocio.Dominio
{
    public class MovimientoDeStock : IValidable
    {

        //PREGUNTAR DPOR TIPOS QUE NOD EBERIAN USARSE
        public int Id { get; set; }

        [Required(ErrorMessage = "Fecha y hora son obligatorios.")]
        public DateTime FechaYHora { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Debe seleccionarse un Articulo.")]
        public Articulo ArticuloDeMovimiento { get; set; }

        [Required(ErrorMessage = "Debe ingresar la cantidad de articulos.")]
        [Range(1, int.MaxValue, ErrorMessage = "Cantidad de articulos no puede ser menor ni igual a 0")]
        public int CantidadArticulo { get; set; }

        [Required(ErrorMessage = "Usuario es campo obligatorio")] //No deberia ser posible que exista movimiento sin usuario, ya que se saca de localstorage
        public Usuario UsuarioDeMovimiento { get; set; }

        [Required(ErrorMessage = "Debe seleccionarse un tipo de movimiento.")]
        public TipoDeMovimiento Tipo { get; set; }


        //To string para mostrar info de manera predefinida
        public override string ToString()
        {
            return $"Id: {Id}, Fecha y Hora: {FechaYHora}, Articulo: {ArticuloDeMovimiento}, Cantidad de articulos: {CantidadArticulo}, Usuario: {UsuarioDeMovimiento}, Tipo de Movimiento {Tipo}";
        }

        //Validaciones de la entidad para el repo
        public void Validar() {

            if (FechaYHora == null || FechaYHora < DateTime.Now)
                throw new ExcepcionCustomException("Debe existir fecha y hora para el movimiento, la misma debe ser posterior o igual a la fecha y hora actuales.");

            if (CantidadArticulo <= 0)
                throw new ExcepcionCustomException("No puede realizarse pedidos con 0 o menos Articulos.");

            if (Tipo == null)
                throw new ExcepcionCustomException("Solo se permite Compra y Venta.");

        }


    }
}
