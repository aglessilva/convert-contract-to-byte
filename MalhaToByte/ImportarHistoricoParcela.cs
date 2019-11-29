using ConvertToByte.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConvertToByte
{

    class TipDamp
    {
        public TipDamp()
        {
            MIECDAMP_CONTRATO = "";
            MIECDAMP_DT_ABERT = "";
            MIECDAMP_TP_OPER = "";
            MIECDAMP_TT_FGTS = "";
            MIECDAMP_AMB_OPER = "";
            MIECDAMP_CTA_EMPR = "";
            MIECDAMP_PIS_PASEP = "";
            MIECDAMP_CTA_TRAB = "";
            MIECDAMP_VL_UTILZ = "";
            MIECDAMP_STATUS = "";
            MIECDAMP_TP_REQUS = "";
            MIECDAMP_FILLER = "";
            MIECDAMP_NR_DAMP = "";
        }
        public string MIECDAMP_CONTRATO { get; set; }
        public string MIECDAMP_DT_ABERT { get; set; }
        public string MIECDAMP_TP_OPER { get; set; }
        public string MIECDAMP_TT_FGTS { get; set; }
        public string MIECDAMP_AMB_OPER { get; set; }
        public string MIECDAMP_CTA_EMPR { get; set; }
        public string MIECDAMP_PIS_PASEP { get; set; }
        public string MIECDAMP_CTA_TRAB { get; set; }
        public string MIECDAMP_VL_UTILZ { get; set; }
        public string MIECDAMP_STATUS { get; set; }
        public string MIECDAMP_TP_REQUS { get; set; }
        public string MIECDAMP_FILLER { get; set; }
        public string MIECDAMP_NR_DAMP { get; set; }
    }

    class ImportarHistoricoParcela
    {
        static void Main(string[] args)
        {
            int contador = 0;
            List<TipDamp> lstLinha = new List<TipDamp>();
            string[] linha = null;

            using (StreamReader sr = new StreamReader(@"C:\Users\x194262\Desktop\EXTRATORPDF\config\CTO068A_DEV.txt", Encoding.Default))
            {
                sr.ReadLine();

                TipDamp objDamp = null;    
                while (!sr.EndOfStream)
                {
                    linha = sr.ReadLine().Split(';');

                    objDamp = new TipDamp()
                    {
                        MIECDAMP_CONTRATO = linha[3].Trim(),
                        MIECDAMP_DT_ABERT = Convert.ToDateTime(linha[1].Trim()).ToString("yyyy-MM-dd"),
                        MIECDAMP_TP_OPER = linha[2].Trim(),
                        MIECDAMP_TT_FGTS = Regex.Replace(linha[4].Trim(), @"[^0-9$]", ""),
                        MIECDAMP_AMB_OPER = linha[5].Trim(),
                        MIECDAMP_CTA_EMPR = linha[8].Trim(),
                        MIECDAMP_PIS_PASEP = linha[9].Trim(),
                        MIECDAMP_CTA_TRAB = linha[10].Trim(),
                        MIECDAMP_VL_UTILZ = Regex.Replace(linha[11].Trim(), @"[^0-9$]", ""),
                        MIECDAMP_STATUS = linha[6].Trim(),
                        MIECDAMP_TP_REQUS = linha[7].Trim(),
                        MIECDAMP_FILLER = linha[0].Trim().Substring(6,2),
                        MIECDAMP_NR_DAMP = linha[2].Trim(),

                    };

                        lstLinha.Add(objDamp);
                        contador++;
                    }
                
            }

            using (StreamWriter streamWriter = new StreamWriter(@"C:\Users\x194262\Desktop\EXTRATORPDF\config\TESTE_RELADAMP.txt"))
            {
                string linhaFormatada = string.Empty;

                lstLinha.ForEach(g => {

                    linhaFormatada += g.MIECDAMP_CONTRATO.Trim() + g.MIECDAMP_DT_ABERT.Trim() + g.MIECDAMP_TP_OPER.Trim().PadRight(50, ' ');
                    linhaFormatada += g.MIECDAMP_TT_FGTS.Trim().PadLeft(18, '0') + g.MIECDAMP_AMB_OPER.Trim().PadRight(50, ' ');
                    linhaFormatada += g.MIECDAMP_CTA_EMPR.Trim().PadLeft(16, '0') + g.MIECDAMP_PIS_PASEP.Trim().PadLeft(11, '0');
                    linhaFormatada += g.MIECDAMP_CTA_TRAB.Trim().Substring(3) + g.MIECDAMP_VL_UTILZ.Trim().PadLeft(18,'0');
                    linhaFormatada += g.MIECDAMP_STATUS.Trim().PadRight(30, ' ') + g.MIECDAMP_TP_REQUS.Trim().PadRight(12, ' ');
                    linhaFormatada += g.MIECDAMP_FILLER.Trim();

                    streamWriter.WriteLine(linhaFormatada);
                    linhaFormatada = string.Empty;
                });
            }


        }
    }
}
