using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LogicaNegocio.Dominio.MovimientoDeStock;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioMovimientoDeStock
    {
        List<MovimientoDeStock> GetAll();

        MovimientoDeStock GetById(int id);

        void Add(MovimientoDeStock obj);

        public List<MovimientoDeStock> BuscarElementosPorIdYTipo(int id, TipoDeMovimiento tipo);

        public List<Articulo> BuscarArtDeMovEnRangoDeFechas(DateTime inicial, DateTime final);

        public List<(int,string,int)> CantidadesPorTipoYFecha();

        public void ModificarStockArticulos(int cantidad, int IdArticulo, bool tipoCambio);

        public bool ControlarSiTipoExiste(int id);

        public bool ControlarSiUsuarioExiste(int id);

        public bool ControlarSiArticuloExiste(int id);
    }
}
