using FacturacionTarea1.Entities;
using FacturacionTarea1.Repositories.Contracts;
using FacturacionTarea1.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionTarea1.Services
{
    public class ArticuloService
    {
        private IArticuloRepository _repository;
        public ArticuloService()
        {
            _repository = new ArticuloRepository();
        }
        public Articulo ObtenerProducto(string nombre)
        {
            return _repository.ObtenerArticuloPorNombre(nombre);
        }

        internal object ObtenerProducto()
        {
            throw new NotImplementedException();
        }
    }
}
