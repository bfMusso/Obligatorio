﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class DTOUsuarioLogin
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string? Rol { get; set; }

        public List<DTORoles> Roles { get; set; } = new List<DTORoles> { };

    }
}
