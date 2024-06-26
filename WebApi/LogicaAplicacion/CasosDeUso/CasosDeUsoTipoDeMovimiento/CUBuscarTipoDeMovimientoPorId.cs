﻿using DTOs;
using LogicaAplicacion.InterfacesCasosDeUso.Genericas;
using LogicaNegocio.Dominio;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.CasosDeUsoTipoDeMovimiento
{
    public class CUBuscarTipoDeMovimientoPorId : ICUBuscarPorId<DTOTipoDeMovimiento>
    {
        public IRepositorioTipoDeMovimiento Repo { get; set; }

        public CUBuscarTipoDeMovimientoPorId(IRepositorioTipoDeMovimiento repo)
        {
            Repo = repo;
        }

        public DTOTipoDeMovimiento Buscar(int id)
        {
            TipoDeMovimiento tipoEncontrado = Repo.GetById(id);

            if (tipoEncontrado != null)
            {
                return MapperTipoDeMovimiento.ToDTOTipoDeMovimiento(tipoEncontrado);
            }
            else 
            {
                throw new ExcepcionCustomException("No se encontro el tipo de movimiento correspondiente al Id: " + id);
            }
        }
    }
}
