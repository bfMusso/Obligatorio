using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorio<T>
    {
        void Add(T obj);

        void Remove(int id);

        void Update(T obj);

        List<T> GetAll();

        T GetById(int id);

    }
}
