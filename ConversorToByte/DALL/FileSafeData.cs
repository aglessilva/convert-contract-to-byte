using ConversorToByte.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversorToByte.DALL
{
    public class FileSafeData
    {
        private conn cnx = null;
        private SqlCommand command = null;
        
        public FileSafeData()
        {
            cnx = new conn();
        }
        public List<FileSafe> GetFileSafe(string _contratoCPF = null, string _id = null)
        {
            List<FileSafe> lst = new List<FileSafe>();
            try
            {
                command = cnx.Parametriza(Procedures.SP_GET_FILES);
                command.Connection = command.Connection;
                command.Parameters.Add(new SqlParameter("@CONTRACTNAME", string.IsNullOrWhiteSpace(_contratoCPF) ? null : _contratoCPF));
                command.Parameters.Add(new SqlParameter("@ID", string.IsNullOrWhiteSpace(_id) ? null : _id));

                command.Connection.Open();
                SqlDataReader dr = command.ExecuteReader();

                FileSafe obj = null;
                while (dr.Read())
                {
                    obj = new FileSafe()
                    {
                        Id = Convert.ToInt32(dr[0].ToString()),
                        NameContract = dr[1].ToString(),
                        NameCpf = dr[2].ToString(),
                        FileEncryption = (dr[3] == DBNull.Value) ? default(byte[]) : (byte[])dr[3]  ,
                        FileEncryptionPdf = (dr[4] == DBNull.Value) ? default(byte[]) : (byte[])dr[4] 
                    };

                    lst.Add(obj);
                }

                cnx.FecharConexao(command);

            }
            catch (SqlException ex)
            {
                cnx.FecharConexao(command);
                throw ex; 
            }

            return lst;
        }



        public List<Users> GetUsers(string _userLogin = null)
        {
            List<Users> lst = new List<Users>();
            try
            {
                command = cnx.Parametriza(Procedures.SP_GET_USERS);
                command.Connection = command.Connection;
                command.Parameters.Add(new SqlParameter("@USERNAME", string.IsNullOrWhiteSpace(_userLogin) ? null : _userLogin));
                command.Connection.Open();
                
                SqlDataReader dr = command.ExecuteReader();

                Users obj = null;
                while (dr.Read())
                {
                    obj = new Users()
                    {
                        UserLogin = dr[0].ToString(),
                        UserName = dr[1].ToString(),
                        UserEmail = dr[2].ToString(),
                        IsGestorApp = Convert.ToBoolean(dr[3])
                    };

                    lst.Add(obj);
                }

                cnx.FecharConexao(command);

                return lst;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UdtUser(string _login)
        {
            command = cnx.Parametriza(Procedures.SP_UDT_USERS);
            command.Parameters.Add(new SqlParameter("@USERLOGIN", string.IsNullOrWhiteSpace(_login) ? null : _login));
            command.Connection = command.Connection;
            command.Connection.Open();
            command.ExecuteNonQuery();
            cnx.FecharConexao(command);
        }

        public int AddUser(Users _user)
        {
            int ret = 0;
            try
            {
                command = cnx.Parametriza(Procedures.SP_CHK_USER);
                command.Connection = command.Connection;
                command.Connection.Open();
                command.Parameters.Add(new SqlParameter("@USERLOGIN", _user.UserLogin ));
                
                ret = (int)command.ExecuteScalar();

                cnx.FecharConexao(command);

                if (ret < 1)
                {
                    command = cnx.Parametriza(Procedures.SP_POST_USERS);
                    command.Connection = command.Connection;
                    command.Connection.Open();
                    command.Parameters.Add(new SqlParameter("@USERLOGIN", _user.UserLogin));
                    command.Parameters.Add(new SqlParameter("@USERNAME", _user.UserName));
                    command.Parameters.Add(new SqlParameter("@USEREMAIL", _user.UserEmail));
                    command.Parameters.Add(new SqlParameter("@ISGESTORAPP", false));
                    ret = command.ExecuteNonQuery();
                    cnx.FecharConexao(command);
                }
                else
                    ret = 100;

                return ret;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public Users CheckUser(string _login)
        {
            Users obj = new Users();
            command = cnx.Parametriza(Procedures.SP_CHK_PERMICAO);
            command.Connection = command.Connection;
            command.Connection.Open();
            command.Parameters.Add(new SqlParameter("@USERLOGIN", _login));
            
            SqlDataReader dr = command.ExecuteReader();


            while (dr.Read())
            {
                obj.IsGestorApp = Convert.ToBoolean(dr[0]);
            }

            return obj;
        }
    }
}

