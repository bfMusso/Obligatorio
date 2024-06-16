using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class DTOUsuarioLogueado
    {
        public int Id { get; set; }
        public string Rol { get; set; }

        public string Token { get; set; }

        public string Email { get; set; }

    }
}
