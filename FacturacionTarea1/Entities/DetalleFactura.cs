using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionTarea1.Entities
{
    public class DetalleFactura
    {
        public Articulo Articulo { get; set; }
        public int Cantidad { get; set; }
        public double Subtotal() 
        {
            return Articulo.PrecioUnitario * Cantidad;
        }
    }
}
