using FacturacionTarea1.Entities;
using FacturacionTarea1.Repositories.Contracts;
using FacturacionTarea1.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionTarea1.Repositories.Implementations
{
    public class ArticuloRepository : IArticuloRepository
    {
        public Articulo ObtenerArticuloPorNombre(string nombre)
        {
            DataTable dt = null;
            ParameterSQL parameterSQL = null;
            Articulo articulo = new Articulo();

            try
            {
                DataHelper.GetInstance().OpenConnection();
                parameterSQL = new ParameterSQL("@nombre_producto", nombre);

                dt = DataHelper.GetInstance().ExecuteSPGet("SP_OBTENER_PRODUCTO", parameterSQL);
                foreach (DataRow dr in dt.Rows) 
                {
                    articulo.Codigo = (int)dr["id_articulo"];
                    articulo.Nombre = (string)dr["articulo"];
                    articulo.PrecioUnitario = Convert.ToDouble(dr["precioUnitario"]);
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally 
            {
                DataHelper.GetInstance().CloseConnection();
            }
            return articulo;
        }
    }
}
