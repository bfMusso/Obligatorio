﻿using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class DTOTipoDeMovimiento
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public bool tipoDeCambioEnStock { get; set; }

    }
}