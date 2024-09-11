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
    public class FormaPgoRepository : IFormaPago
    {
        public FormaPago ObtenerFormaPago(string nombre)
        {
            DataTable dt = null;
            ParameterSQL paramsql = null;
            FormaPago formaPago = new FormaPago();

            try
            {
                DataHelper.GetInstance().OpenConnection();
                paramsql = new ParameterSQL("@forma_pago", nombre);
                dt = DataHelper.GetInstance().ExecuteSPGet("SP_OBTENER_FORMA_PAGO", paramsql);
                foreach (DataRow dr in dt.Rows)
                {
                    formaPago.Codigo = (int)dr["id_forma_pago"];
                    formaPago.Nombre = (string)dr["forma_pago"];
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
            return formaPago;
        }
    }
}
