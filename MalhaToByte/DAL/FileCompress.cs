using System;

namespace MalhaToByte.DAL
{
    public class FileCompress
    {
        public int Id { get; set; }
        public string ContractName { get; set; }
        public string DocumentCpf { get; set; }
        public byte[] FileEncryption { get; set; }
    }
}
