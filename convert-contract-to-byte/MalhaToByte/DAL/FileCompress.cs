using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MalhaToByte.DAL
{
    public class FileCompress
    {
        public int Id { get; set; }
        public int IdCompany { get; set; }
        public string ContractName { get; set; }
        public string CpfName { get; set; }
        public byte[] FileEncryption { get; set; }
        public DateTime DateInput { get; set; }
    }


    public class PathFiles
    {
        public int IdCompany { get; set; }
        public string PathFileCompany { get; set; }
    }
}
