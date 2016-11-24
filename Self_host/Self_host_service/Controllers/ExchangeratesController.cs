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

    [RoutePrefix("api/exchangerates")]
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
        [Route("")]
        public IQueryable<ExchangerateDto> GetExchangerates()
        {
            return db.ExchangerateList.Include(b => b.Base).Select(AsExchangerateDto);
        }


        // GET api/Exchangerates/2
        [Route("{id:int}")]
        [ResponseType(typeof(ExchangerateDto))]
        public async Task<IHttpActionResult> GetExchangerate(int id)
        {
            ExchangerateDto exchangerate = await db.ExchangerateList.Include(b => b.Base)
                .Where(b => b.Id == id)
                .Select(AsExchangerateDto)
                .FirstOrDefaultAsync();
            if (exchangerate == null)
            {
                return NotFound();
            }

            return Ok(exchangerate);
        }


        [Route("date/{date1:datetime:regex(\\d{4}.\\d{2}.\\d{2})}-{date2:datetime:regex(\\d{4}.\\d{2}.\\d{2})}")]
        public IQueryable<ExchangerateDto> GetPartExchangerates(DateTime date1, DateTime date2)
        {
            //проверка на присутствие котировок за определенную дату
            bool absent = false;
            var exchangerate = db.ExchangerateList;
            
            for (var currentDay = date1; currentDay <= date2; currentDay = currentDay.AddDays(1))
            {
                foreach (var p in exchangerate)
                {
                    if (currentDay == p.Date)
                    {
                        absent = true;
                        break;
                    }
                }
                if (!absent) WorkerDB.AddNewExchangerate(currentDay);
                absent = false;
            }


            return db.ExchangerateList.Include(b => b.Base)
                .Where(b => DbFunctions.TruncateTime(b.Date) >= DbFunctions.TruncateTime(date1) && 
                    DbFunctions.TruncateTime(b.Date) <= DbFunctions.TruncateTime(date2)).Select(AsExchangerateDto);
        }



        
    }
}
