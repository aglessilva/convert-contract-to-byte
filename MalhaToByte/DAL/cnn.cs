using ConvertToByte.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MalhaToByte.DAL
{
    public class Cnn
    {

       public static string strConn = @"Password=01#$Sucesso;Persist Security Info=True;User ID=sa;Initial Catalog=DB_FileSafe;Data Source=NP2110929\SQLEXPRESS";
       
        public static string strConnTombamneto = @"Password=01#$Sucesso;Persist Security Info=True;User ID=sa;Initial Catalog=DB_Tombamento;Data Source=NP2110929\SQLEXPRESS";

        static SqlCommand command = new SqlCommand();

        static void Parametriza(string _proc)
        {
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = _proc;
            command.CommandTimeout = 0;
            command.Parameters.Clear();
        }

        static void FecharConexao()
        {
            command.Dispose();
            command.Clone();
        }



        public static void AddHistoricoParcela(List<HistoricoParcela> historicoParcela)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(strConnTombamneto))
                {
                    Parametriza("SP_GET_FILES");
                    connection.Open();
                    command.Connection = connection;


                };
            }
            catch (Exception exErro)
            {
                string strErro = exErro.Message;
                throw;
            }
        }

        public static byte[] DownloadFile(string p)
        {

            using (SqlConnection connection = new SqlConnection(strConn))
            {

                byte[] ret = null;
                try
                {

                    Parametriza("SP_GET_FILES");
                    command.Connection = connection;
                    command.Parameters.Add(new SqlParameter("@ID", p));

                    connection.Open();
                    SqlDataReader dr = command.ExecuteReader();
                    dr.Read();
                    ret = (byte[])dr[0];

                    FecharConexao();

                }
                catch (SqlException ex)
                {
                    FecharConexao();
                    connection.Close();
                    throw ex; ;
                }

                return ret;
            }
        }





        public static void FileStores(DataTable dataTable)
        {
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                try
                {
                    SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.UseInternalTransaction, null)
                    {
                        DestinationTableName = "FileSafe"
                    };

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    bulkCopy.WriteToServer(dataTable);
                    connection.Close();

                    dataTable.Clear();
                }


            
                catch (SqlException ex)
                {
                    FecharConexao();
                    connection.Close();
                    throw ex;
                }

            }

        }
       
    }
}
