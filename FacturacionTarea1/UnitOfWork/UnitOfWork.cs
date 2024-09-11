using FacturacionTarea1.Repositories.Contracts;
using FacturacionTarea1.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionTarea1.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private SqlTransaction _transaction;
        private SqlConnection _connection;
        private IArticuloRepository articuloRepository;
        private IFacturaRepository facturaRepository;
        private IFormaPago formaPago;
        public UnitOfWork()
        {
            _connection = DataHelper.GetInstance().getConnection();
        }
    }
}
