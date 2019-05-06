using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MalhaToByte.DAL
{
    public class Cnn
    {

       public static string strConn = "Password=#$01Sucesso;Persist Security Info=True;User ID=sa;Initial Catalog=DBFileSafe;Data Source=HUISPVWDV2939";
            

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

 


        public static byte[] DownloadFile(string p)
        {

            using (SqlConnection connection = new SqlConnection(strConn))
            {

                byte[] ret = null;
                try
                {

                    Parametriza("sp_Post_File_safe");
                    command.Connection = connection;
                    command.Parameters.Add(new SqlParameter("@Proporsal", p));
                    command.Parameters.Add(new SqlParameter("@tipo", 1));

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





        public static int FileStores(List<FileCompress> ByteFile)
        {

            using (SqlConnection connection = new SqlConnection(strConn))
            {

                int ret = 0;
                try
                {
                    Parametriza("sp_Post_File_safe");
                    command.Connection = connection;
                    connection.Open();

                    ByteFile.ForEach(x =>
                    {
                        command.Parameters.Add(new SqlParameter("@IdCompany", x.IdCompany));
                        command.Parameters.Add(new SqlParameter("@ContractName", x.ContractName));
                        command.Parameters.Add(new SqlParameter("@CpfName", x.CpfName));
                        command.Parameters.Add(new SqlParameter("@FileEncryption", x.FileEncryption));
                        command.Parameters.Add(new SqlParameter("@DateInput", x.DateInput));
                        ret += command.ExecuteNonQuery();
                        command.Parameters.Clear();
                        Thread.Sleep(300);
                    });


                    FecharConexao();

                }
                catch (SqlException ex)
                {
                    FecharConexao();
                    connection.Close();
                    throw ex;
                }

                return ret;
            }

        }


        public static List<PathFiles> GetPathFileCompany()
        {
            List<PathFiles> lst = new List<PathFiles>();
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                Parametriza("SP_Get_Path_Company");
                command.Connection = connection;
                connection.Open();
                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    lst.Add(new PathFiles() { IdCompany = Convert.ToInt32(dr[0].ToString()), PathFileCompany = dr[1].ToString() });
                }
            }

            return lst;
        }
    }
}
