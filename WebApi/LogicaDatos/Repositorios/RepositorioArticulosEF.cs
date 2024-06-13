using LogicaNegocio.Dominio;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LogicaDatos.Repositorios
{
    public class RepositorioArticulosEF : IRepositorioArticulos
    {
        //Declaramos vacio inicialmente
        public LibreriaContext Contexto { get; set; }

        public RepositorioArticulosEF(LibreriaContext ctx)
        {
            Contexto = ctx;
        }

        public void Add(Articulo obj)
        {
            //Se ejecutan las validaciones del dominio
            obj.Validar();
            //Se valida unicidad de nombre (Consultar si es menor aca o en CU)
            NombreEsUnico(obj.Nombre);
            //Se valida unicidad del codigo de proveedor
            CodigoUnico(obj.CodigoProveedor);
            //Se agrega a Contexto(Bd)
            Contexto.Articulos.Add(obj);
            //Guardamos cambios
            Contexto.SaveChanges();
        }

        public List<Articulo> GetAll()
        {   
            //Se devuelve total de elementos ordenados por Nombre
            return Contexto.Articulos
                           .OrderBy(art => art.Nombre)
                           .ToList();
        }

        public Articulo GetById(int id)
        {
            //Se busca elemento segun id recibido como parametro
            return Contexto.Articulos.Find(id);
        }

        public void Remove(int id)
        {   
            //Buscamos elemento por id
            Articulo aBorrar = GetById(id);
            //Si la respuesta es distinta a null, lo eliminamos, sino se manda excepcion.
            if (aBorrar != null)
            {   
                Contexto.Articulos.Remove(aBorrar);
                Contexto.SaveChanges();
            }
            else
            {
                throw new ExcepcionCustomException("El Articulo con id " + id + " no existe");
            }
        }

        public void Update(Articulo obj)
        {
            //Se ejecutan las validaciones del dominio
            obj.Validar();
            //Actualizo y guardo
            Contexto.Articulos.Update(obj);
            //Se guardan los cambios
            Contexto.SaveChanges();
        }

        public void NombreEsUnico(string nombre)
        {
            bool nombreExistente = Contexto.Articulos.Any(art => art.Nombre == nombre);
            //Si se encuentra articulo con ese nombre, se lanza excepcion(Ya existe ek nombre)
            if (nombreExistente)
            {
                throw new ExcepcionCustomException("El nombre de articulo ya existe.");
            }
        }

        public void CodigoUnico(string codigo)
        {
            bool codigoExistente = Contexto.Articulos.Any(art => art.CodigoProveedor == codigo);
            //Si se encuentra articulo con ese nombre, se lanza excepcion(Ya existe el cod de proveedor)
            if (codigoExistente)
            {
                throw new ExcepcionCustomException("El codigo de proveedor ingresado ya existe.");
            }

            //bool nombre = Contexto.Articulos.Any(art => art.CodigoProveedor == codigo);
        }

    }
}
