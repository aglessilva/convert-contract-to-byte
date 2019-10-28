using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConvertToByte
{
    class QuebraArquivo
    {
        static void Main(string[] args)
        {
            int contador = 1, ctrl = 1;
            StreamWriter sw = new StreamWriter($@"C:\TombamentoV1_01\ALTA\ARQUIVO_{ctrl}.txt", true, Encoding.Default);

            using (StreamReader sr = new StreamReader(@"C:\TombamentoV1_01\ALTA\TL16PARC.txt"))
            {
                string linha = string.Empty;
                while (!sr.EndOfStream)
                {
                    linha = sr.ReadLine();
                   // Thread.Sleep(1);
                    sw.WriteLine(linha);
                    if (contador == 300000)
                    {
                        ctrl++;
                        sw = new StreamWriter($@"C:\TombamentoV1_01\ALTA\ARQUIVO_{ctrl}.txt", true, Encoding.Default);
                        Thread.Sleep(5);
                        contador = 1;
                    }

                    contador++;
                }

            }
            
        }
    }
}