using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Excepciones
{
    public class ExcepcionCustomException:Exception
    {
        
        public ExcepcionCustomException() { }

        public ExcepcionCustomException(string mensaje) : base(mensaje)
        {
        }

        public ExcepcionCustomException(string mensaje, Exception interna) : base(mensaje, interna)
        {
        }
   
    }
}
