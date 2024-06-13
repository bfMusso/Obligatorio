﻿using LogicaAplicacion.InterfacesCasosDeUso.Genericas;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.CasosDeUsoUsuario
{
    public class CUActualizarUsuario : ICUActualizar<Usuario>
    {
        public IRepositorioUsuarios Repo { get; set; }

        public CUActualizarUsuario(IRepositorioUsuarios repo)
        {
            Repo = repo;
        }

        public void Actualizar(Usuario obj)
        {
            obj.PasswordEnctriptado = Repo.EncriptarPassword(obj.Password);
            Repo.Update(obj);
        }
    }
}
