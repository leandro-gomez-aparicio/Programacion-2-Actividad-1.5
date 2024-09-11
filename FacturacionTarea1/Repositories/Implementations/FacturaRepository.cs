using FacturacionTarea1.Entities;
using FacturacionTarea1.Repositories.Contracts;
using FacturacionTarea1.Services;
using FacturacionTarea1.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionTarea1.Repositories.Implementations
{
    public class FacturaRepository : IFacturaRepository
    {
        public bool Update(Factura factura, int id)
        {
            bool result = true;
            SqlTransaction t = null;
            int rows = 0;
            int rowsDetalle = 0;

            try
            {
                t = DataHelper.GetInstance().beginTransaction();

                ParameterSQL idFactura = new ParameterSQL("@id_factura", id);
                rows = DataHelper.GetInstance().ExecuteSPwParams("SP_ACTUALIZAR_FACTURA", idFactura, t);
                List<ParameterSQL> lstDetalle = new List<ParameterSQL>();
                int conteoId = 1;

                foreach(DetalleFactura df in factura.Detalle) 
                {
                    ParameterSQL idDetalle = new ParameterSQL("@id_detalle", conteoId);
                    ParameterSQL idFac = new ParameterSQL("@id_factura", id);
                    ParameterSQL idArt = new ParameterSQL("@id_articulo", df.Articulo.Codigo);
                    ParameterSQL cantidad = new ParameterSQL("@cantidad", df.Cantidad);

                    lstDetalle.Add(idDetalle);
                    lstDetalle.Add(idFac);
                    lstDetalle.Add(idArt);
                    lstDetalle.Add(cantidad);
                    rowsDetalle = DataHelper.GetInstance().ExecuteSPwParams("SP_INSERTAR_DETALLE", lstDetalle, t);
                    lstDetalle.Clear();
                    conteoId++;
                }
                DataHelper.GetInstance().CommitTransaction(t);
            }
            catch (Exception exc)
            {
                DataHelper.GetInstance().RollbackTransaction(t);
                throw exc;
            }
            finally 
            {
                DataHelper.GetInstance().CloseConnection();
            }
            if (rows > 0 && rowsDetalle > 0) 
            {
                return result;
            }
            else
            {
                return result = false;
            }
        }

        public bool Delete(int id)
        {
            bool result = true;
            int rows = 0;
            SqlTransaction t = null;

            try
            {
                t = DataHelper.GetInstance().beginTransaction();
                ParameterSQL idFactura = new ParameterSQL("@id_factura", id);
                rows = DataHelper.GetInstance().ExecuteSPwParams("SP_ELIMINAR_FACTURA", idFactura, t);
                DataHelper.GetInstance().CommitTransaction(t);
            }
            catch (Exception exc)
            {
                DataHelper.GetInstance().RollbackTransaction(t);
                throw exc;
            }
            finally
            {
                DataHelper.GetInstance().CloseConnection();
            }

            if (rows > 0)
            {
                return result;
            }
            else
            {
                return result = false;
            }
        }

        public List<Factura> GetList()
        {
            FormaPagoService formaPagoService = new FormaPagoService();
            ArticuloService articuloManager = new ArticuloService();
            List<Factura> Facturas = new List<Factura>();
            DataTable dataTableFact = null;
            DataTable dataTableDet = null;

            try
            {
                DataHelper.GetInstance().OpenConnection();
                dataTableFact = DataHelper.GetInstance().ExecuteSPGet("SP_OBTENER_FACTURAS");

                foreach (DataRow r in dataTableFact.Rows)
                {
                    Factura oFactura = new Factura();
                    oFactura.NroFactura = (int)r["id_factura"];
                    oFactura.Fecha = (DateTime)r["fecha"];
                    oFactura.Cliente = (string)r["cliente"];
                    oFactura.FormaPago = formaPagoService.ObtenerFormaPago((string)r["forma_pago"]);

                    Facturas.Add(oFactura);
                }
                foreach (Factura f in Facturas)
                {
                    DataHelper.GetInstance().OpenConnection();
                    ParameterSQL p = new ParameterSQL("@id_factura", f.NroFactura);
                    dataTableDet = DataHelper.GetInstance().ExecuteSPGet("SP_OBTENER_DETALLES", p);

                    foreach (DataRow r in dataTableDet.Rows)
                    {
                        DetalleFactura df = new DetalleFactura();

                        df.Articulo = articuloManager.ObtenerProducto((string)r["articulo"]);
                        df.Cantidad = (int)r["cantidad"];
                        f.AgregarDetalle(df);
                    }

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
            return Facturas;
        }

        public bool Add(Factura factura)
        {
            bool result = true;
            SqlTransaction t = null;

            try
            {
                t = DataHelper.GetInstance().beginTransaction();
                List<ParameterSQL> lstMaestro = new List<ParameterSQL>();
                ParameterSQL cliente = new ParameterSQL("@cliente", factura.Cliente);
                ParameterSQL tipoPago = new ParameterSQL("@formaPago", factura.FormaPago.Codigo);
                lstMaestro.Add(cliente);
                lstMaestro.Add(tipoPago);
                int idFactura = DataHelper.GetInstance().ExecuteSPwParams("SP_INSERTAR_MAESTRO", lstMaestro, t);

                List<ParameterSQL> lstDetalle = new List<ParameterSQL>();
                int conteoId = 1;
                int rows;

                foreach (DetalleFactura df in factura.Detalle) 
                {
                    ParameterSQL idDetalle = new ParameterSQL("@id_detalle", conteoId);
                    ParameterSQL idFac = new ParameterSQL("@id_factura", idFactura);
                    ParameterSQL idArt = new ParameterSQL("@id_articulo", df.Articulo.Codigo);
                    ParameterSQL cantidad = new ParameterSQL("@cantidad", df.Cantidad);

                    lstDetalle.Add(idDetalle);
                    lstDetalle.Add(idFac);
                    lstDetalle.Add(idArt);
                    lstDetalle.Add(cantidad);
                    rows = DataHelper.GetInstance().ExecuteSPwParams("SP_INSERTAR_DETALLE", lstDetalle, t);
                    lstDetalle.Clear();
                    conteoId++;
                }
                DataHelper.GetInstance().CommitTransaction(t);
            }
            catch (Exception)
            {
                result = false;
                DataHelper.GetInstance().RollbackTransaction(t);
            }
            finally
            {
                DataHelper.GetInstance().CloseConnection();
            }
            return result;
        }
    }
}
