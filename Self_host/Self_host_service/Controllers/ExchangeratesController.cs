using Self_host_service.DB;
using Self_host_service.DTOs;
using Self_host_service.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Self_host_service.Controllers
{
    public class ExchangeratesController : ApiController
    {
        //public Exchangerate[] exchangerates { get; set; }


        private EntityDbContext db = new EntityDbContext();

        // Typed lambda expression for Select() method. 
        private static readonly Expression<Func<Exchangerate, ExchangerateDto>> AsExchangerateDto =
            x => new ExchangerateDto
            {
                Base = x.Base,
                Date = x.Date,
                rates = x.rates,
            };

        // GET api/Exchangerates
        public IQueryable<ExchangerateDto> GetExchangerates()
        {
            return db.ExchangerateList.Include(b => b.Base).Select(AsExchangerateDto);
        }


        // GET api/Exchangerates/2
        [ResponseType(typeof(ExchangerateDto))]
        public async Task<IHttpActionResult> GetExchangerate(int id)
        {
            ExchangerateDto exchangerate = await db.ExchangerateList.Include(b => b.Base)
                .Where(b => b.Id== id)
                .Select(AsExchangerateDto)
                .FirstOrDefaultAsync();
            if (exchangerate == null)
            {
                return NotFound();
            }

            return Ok(exchangerate);
        }


        
    }
}
