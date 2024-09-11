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
    public class FacturaService
    {
        private IFacturaRepository _repository;
        public FacturaService()
        {
            _repository = new FacturaRepository();
        }
        public bool GuardarFactura(Factura factura)
        {
            return _repository.Add(factura);
        }
        public bool ActualizarFactura(Factura factura, int id)
        {
            return _repository.Update(factura, id);
        }
        public bool EliminarFactura(int id)
        {
            return _repository.Delete(id);
        }
        public List<Factura> ObtenerFacturas()
        {
            return _repository.GetList();
        }
    }
}
