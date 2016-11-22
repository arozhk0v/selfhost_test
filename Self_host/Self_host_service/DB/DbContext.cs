using Self_host_service.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_host_service.DB
{
    class EntityDbContext : DbContext
    {
        public DbSet<Exchangerate> ExchangerateList { get; set; }
    }
}
