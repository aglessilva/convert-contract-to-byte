using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversorToByte.DTO
{
    public class Users
    {
        public string UserLogin { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public bool IsGestorApp { get; set; }
        public bool IsAtivo { get; set; }
    }
}
