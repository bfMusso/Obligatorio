﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosDeUso.Genericas
{
    public interface ICUActualizar<T>
    {
        void Actualizar(T obj);
    }
}
