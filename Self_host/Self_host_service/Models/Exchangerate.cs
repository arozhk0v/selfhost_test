using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_host_service.Models
{
    public class Exchangerate
    {
        public int Id { get; set; }
        public string @Base { get; set; }

        public DateTime Date { get; set; }

        public Rates rates { get; set; }
    }
}
