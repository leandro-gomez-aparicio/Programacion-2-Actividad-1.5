using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionTarea1.Entities
{
    public class Factura
    {
        public int NroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public FormaPago FormaPago { get; set; }
        public string Cliente { get; set; }
        public List<DetalleFactura> Detalle { get; set; }
        public Factura()
        {
            Detalle = new List<DetalleFactura>();
        }
        public void AgregarDetalle(DetalleFactura df)
        {
            bool encontrado = false;
            foreach (DetalleFactura d in Detalle)
            {
                if (df.Articulo.Codigo == d.Articulo.Codigo)
                {
                    d.Cantidad += df.Cantidad;
                    encontrado = true;
                    break;
                }
            }
            if (!encontrado)
            {
                Detalle.Add(df);
            }

        }
        public void EliminarDetalles()
        {
            Detalle.Clear();
        }

    }
}
