using FacturacionTarea1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionTarea1.Repositories.Contracts
{
    public interface IArticuloRepository
    {
        Articulo ObtenerArticuloPorNombre(string nombre);
    }
}
