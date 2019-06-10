using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace ConversorToByte.DALL
{
    public  class conn
    {
        string strConn =  string.Empty;
        SqlCommand command = null;
        public conn() 
        {
            strConn = ConfigurationManager.ConnectionStrings[0].ToString();
            command = new SqlCommand();
        }


        protected internal SqlCommand Parametriza(Procedures _proc)
        {
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = _proc.ToString();
            command.CommandTimeout = 0;
            command.Parameters.Clear();
            command.Connection = new SqlConnection(strConn);

            return command;
        }

        protected internal void FecharConexao(SqlCommand _command)
        {
            _command.Dispose();
            _command.Clone();
        }







        public byte[] Pesquisars(string prop)
        {
           
            SqlCommand command = null;

            using (SqlConnection connection = new SqlConnection("Password=presto123;Persist Security Info=True;User ID=presto_user;Initial Catalog=DB_Santander_Presto;Data Source=172.19.11.155"))
            {

                byte[] ret = null;
                try
                {

                    command = new SqlCommand();
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.CommandText = "sp_ArmazenarArquivoProposta";
                    command.CommandTimeout = 0;

                    command.Parameters.Add(new SqlParameter("@Proporsal", prop));
                    command.Parameters.Add(new SqlParameter("@tipo", 1));

                    connection.Open();
                    SqlDataReader dr = command.ExecuteReader();
                    while(dr.Read())
                    {
                        ret = (byte[])dr[0];
                    }

                    command.Dispose();
                    command.Clone();
                    connection.Close();
                  

                }
                catch (SqlException ex)
                {
                    command.Dispose();
                    command.Clone();
                    connection.Close();
                    throw ex; ;
                }

                return ret;
            }
        }
        
    }
}