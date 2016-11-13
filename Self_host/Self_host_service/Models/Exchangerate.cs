using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_host_service.Models
{
    public class Exchangerate
    {
        public string Ccy { get; set; }
        public string Base_ccy { get; set; }
        public decimal Buy { get; set; }
        public decimal Sale { get; set; }
    }
}
