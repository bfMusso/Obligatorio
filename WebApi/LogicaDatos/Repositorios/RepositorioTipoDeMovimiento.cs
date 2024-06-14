using LogicaNegocio.Dominio;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDatos.Repositorios
{
    public class RepositorioTipoDeMovimiento : IRepositorioTipoDeMovimiento
    {
        public LibreriaContext Contexto { get; set; }

        public RepositorioTipoDeMovimiento(LibreriaContext ctx)
        {
            Contexto = ctx;
        }

        public void Add(TipoDeMovimiento obj)
        {
            //Validar objeto
            obj.Validar();
            //Agregar a BD
            Contexto.TipoDeMovimientos.Add(obj);
            //Guardar cambios
            Contexto.SaveChanges();
        }

        public List<TipoDeMovimiento> GetAll()
        {
            return Contexto.TipoDeMovimientos.ToList();
        }

        public TipoDeMovimiento? GetById(int id)
        {
            try
            {
                return Contexto.TipoDeMovimientos.Find(id);
            }
            catch 
            {
                throw new ExcepcionCustomException("No se encontro el elemento.");
            
            }
        }

        public void Remove(int id)
        {
            TipoDeMovimiento tipoMovimiento = GetById(id);
            if (tipoMovimiento != null)
            {
                //Controlar si existe
                if (!controlarSiEstaEnUso(id))
                {
                    Contexto.TipoDeMovimientos.Remove(tipoMovimiento);
                    Contexto.SaveChanges();

                }
                else 
                {
                    throw new ExcepcionCustomException("El Movimiento con id" + id + " esta siendo utilizado en un movimiento de stock.");
                }
                
            }
            else
            {
                throw new ExcepcionCustomException("El Movimiento con id" + id + " no existe");
            }
        }

        public void Update(TipoDeMovimiento obj)
        {
            //Se ejecutan las validaciones del dominio
            obj.Validar();
            //Controlar si existe
            if (!controlarSiEstaEnUso(obj.Id))
            {
                //Actualizar y guardar
                Contexto.TipoDeMovimientos.Update(obj);
                //Se efectuan los cambios
                Contexto.SaveChanges();
            }
            else
            {
                throw new ExcepcionCustomException("El Movimiento con id" + obj.Id + " esta siendo utilizado en un movimiento de stock.");
            }

        }

        public bool controlarSiEstaEnUso(int id)
        {
            return  Contexto.MovimientosDeStock
                            .Any(m => m.Tipo.Id == id);
        }
    }
}
