using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvetPdfToLayoutAlta.Models
{
    public class Conn
    {
        string strConn = string.Empty;
        SqlCommand command = null;

        public Conn()
        {
            strConn = ConfigurationManager.ConnectionStrings[0].ToString();
            command = new SqlCommand();
        }

        protected internal SqlCommand Parametriza()
        {
            command.Connection = new SqlConnection(strConn);
            return command;
        }

        protected internal SqlCommand Parametriza(string _procedure)
        {
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = _procedure;
            command.CommandTimeout = 0;
            command.Parameters.Clear();
            command.Connection = new SqlConnection(strConn);

            return command;
        }



        protected internal void FecharConexao(SqlCommand _command)
        {
            if (_command != null)
                _command.Dispose();
        }
    }
}
