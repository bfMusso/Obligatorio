﻿using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class MapperUsuarios
    {
        public static DTOUsuario ToDTOUsuario(Usuario usuario)
        {
            return new DTOUsuario()
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Password = usuario.Password,
                Rol = usuario.Rol
            };
        }


        public static List<DTOUsuario> ToListarDTOUsuarios(List<Usuario> usuarios)
        {
            return usuarios.Select(usuario => new DTOUsuario
            {
                Email = usuario.Email,
                Password = usuario.Password,
                Rol = usuario.Rol

            }).ToList();

        }

        public static List<DTORoles> ToListarDTORoles(List<Usuario> usuarios)
        {
            return usuarios.Select(usuario => new DTORoles
            {
                Rol = usuario.Rol

            }).ToList();

        }

    }
}
