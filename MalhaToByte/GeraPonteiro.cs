using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConvertToByte
{
    public enum Direcao
    {
        Esquerda = 0,
        Direita = 1
    }

    public static class Contrato
    {
        public static string RetornaPosicao(this string txt, int _tamanho, char complemento, Direcao direcao)
        {
            string vlr = string.Empty;

            if(direcao == Direcao.Direita)
                vlr = txt.PadRight(_tamanho, complemento);
            else
                vlr = txt.PadLeft(_tamanho, complemento);

            return vlr;
        }
    }



    class Cliente
    {
        public string NumeroContrato { get; set; }
        public string Valor { get; set; }
        public string Prazo { get; set; }
        public string DataContrato { get; set; }
    }



    class GeraPonteiro
    {
        static void Main(string[] args)
        {
            Cliente cli = new Cliente();

            string Valor = "100000";
            string _numero = "1234";

            cli.NumeroContrato = _numero;
            cli.Valor = Valor;

            cli.Valor =  cli.Valor.RetornaPosicao(18, ' ', Direcao.Direita);

            cli.NumeroContrato = cli.NumeroContrato.RetornaPosicao(20, '0', Direcao.Esquerda);

            Valor = cli.Valor;
            Valor += cli.NumeroContrato;
            
        }
    }
}
