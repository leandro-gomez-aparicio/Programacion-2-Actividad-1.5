using FacturacionTarea1.Entities;
using FacturacionTarea1.Repositories.Contracts;
using FacturacionTarea1.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionTarea1.Services
{
    public class FacturacionManager
    {
        private SqlTransaction _transaction;
        private SqlConnection _connection;
        private IArticuloRepository articuloRepository;
        private IFacturaRepository facturaRepository;
        private IFormaPago formaPago;
        public FacturacionManager()
        {
            articuloRepository= new ArticuloRepository();
            facturaRepository= new FacturaRepository();
            formaPago=new FormaPgoRepository();
        }
        public List<Factura> GetFacturas() 
        {
            return facturaRepository.GetList();
        }
        //public List<Articulo> GetArticulos() 
        //{
        //    return articuloRepository.ObtenerArticuloPorNombre 
        //}
    }
}
