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
    public class FormaPagoService
    {
        private IFormaPago _repository;
        public FormaPagoService()
        {
            _repository = new FormaPgoRepository();
        }
        public FormaPago ObtenerFormaPago(string nombre)
        {
            return _repository.ObtenerFormaPago(nombre);
        }
    }
}
