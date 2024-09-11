using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FacturacionTarea1.Utils
{
    public class DataHelper
    {
        private static DataHelper _instancia;
        private SqlConnection _connection;
        private DataHelper() 
        {
            _connection = new SqlConnection("Data Source=localhost;Initial Catalog=FacturacionDB;Integrated Security=True;");
        }
        public static DataHelper GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new DataHelper();
            }
            return _instancia;
        }
        public SqlConnection OpenConnection()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
            return _connection;
        }
        public void CloseConnection()
        {
            _connection?.Close();
        }
        public void CommitTransaction(SqlTransaction transaction)
        {
            transaction?.Commit();
        }

        public void RollbackTransaction(SqlTransaction transaction)
        {
            transaction?.Rollback();
        }
        public SqlTransaction beginTransaction()
        {
            return OpenConnection().BeginTransaction();
        }

        public DataTable ExecuteSPGet(string sp, ParameterSQL parametros)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(parametros.Name, parametros.Value);
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception exc)
            {
                dt = null;
                throw exc;
            }

            return dt;
        }

        public DataTable ExecuteSPGet(string sp)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                dt.Load(cmd.ExecuteReader());

            }
            catch (Exception exc)
            {
                dt = null;
                throw exc;
            }

            return dt;
        }

        public int ExecuteSPwParams(string sp, List<ParameterSQL> parametros, SqlTransaction t)
        {
            int idFactura = 0;
            int rows = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(sp, _connection, t);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (ParameterSQL param in parametros)
                {
                    cmd.Parameters.AddWithValue(param.Name, param.Value);
                }

                if (sp == "SP_INSERTAR_MAESTRO")
                {
                    SqlParameter paramOutput = new SqlParameter("@id_factura", SqlDbType.Int);
                    paramOutput.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(paramOutput);

                    cmd.ExecuteNonQuery();

                    idFactura = (int)paramOutput.Value;
                    cmd.Parameters.Clear();
                }
                else
                {
                    rows = cmd.ExecuteNonQuery();
                }


            }
            catch (Exception exc)
            {
                throw exc;
            }
            if (sp == "SP_INSERTAR_MAESTRO")
            {
                return idFactura;
            }
            else
            {
                return rows;
            }
        }
        public int ExecuteSPwParams(string sp, ParameterSQL parametros, SqlTransaction t)
        {
            int rows = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(sp, _connection, t);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(parametros.Name, parametros.Value);

                rows = cmd.ExecuteNonQuery();



            }
            catch (Exception exc)
            {
                throw exc;
            }
            return rows;
        }
    }
}
