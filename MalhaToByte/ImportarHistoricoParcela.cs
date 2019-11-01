using ConvertToByte.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertToByte
{
    class ImportarHistoricoParcela
    {
        static void Main(string[] args)
        {
            int contador = 0;
            List<string> lstLinha = new List<string>();
            using (StreamReader sr = new StreamReader(@"C:\TombamentoV1_01\ALTA\TL16PARC.TXT", Encoding.Default))
            {
               // sr.ReadLine(); sr.ReadLine();

                string linha = string.Empty;

                    while (!sr.EndOfStream)
                    {
                        linha = sr.ReadLine();

                        lstLinha.Add(linha);
                        contador++;
                    }
                
            }
        }
    }
}
