using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;

namespace ConvetPdfToLayoutAlta
{
    public static class Ambiente
    {
        public static Dictionary<string, string> dicionario16 = new Dictionary<string, string>();
        public static Dictionary<string, string> dicionario18 = new Dictionary<string, string>();
        public static Dictionary<string, string> dicionario20 = new Dictionary<string, string>();
        public static Dictionary<string, string> dicionario25 = new Dictionary<string, string>();
        public static List<KeyValuePair<string,string>> listGTBem = new List<KeyValuePair<string, string>>();

        public static void  GetContratoNumeroBem()
        {
            string con =$@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={Directory.GetCurrentDirectory()}\config\QUERY_FOR_FILTER_FOR_QUERY__000.xlsx; Extended Properties='Excel 8.0;HDR=Yes;'";
            using (OleDbConnection connection = new OleDbConnection(con))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("select GTDT073_CD_CNTR_ORIG, GTDTRGB_NUM_BIEN from  [QUERY_FOR_FILTER_FOR_QUERY__000$]", connection);
                using (OleDbDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        listGTBem.Add(new KeyValuePair<string, string>(dr[0].ToString(),dr[1].ToString()));
                    }
                }
            }
        }
    }
}

