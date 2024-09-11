using FacturacionTarea1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionTarea1.Repositories.Contracts
{
    public interface IFacturaRepository
    {
        bool Add(Factura factura);
        bool Update(Factura factura, int id);
        List<Factura> GetList();
        bool Delete(int id);
    }
}
