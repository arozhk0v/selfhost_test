using Self_host_service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Self_host_service.Controllers
{
    public class ExchangeratesController : ApiController
    {
        Exchangerate[] exchangerates = new Exchangerate[]  
        {  
            new Exchangerate { Ccy = "EUR", Base_ccy = "UAN", Buy = 28.50000m, Sale = 28.80000m },  
            new Exchangerate { Ccy = "RUR", Base_ccy = "UAN", Buy = 0.39000m, Sale = 0.42000m },  
            new Exchangerate { Ccy = "USD", Base_ccy = "UAN", Buy = 26.20000m, Sale = 26.40000m }
        };


        /// <summary>
        /// Метод для получения всех строк
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Exchangerate> GetAllExchangerates()
        {
            return exchangerates;
        }

        /// <summary>
        /// Метод для получения значения по ccy.
        /// </summary>
        /// <param name="ccy">Название валюты.</param>
        /// <returns></returns>
        public Exchangerate GetExchangerateByCcy(string ccy)
        {
            var excchangerate = exchangerates.FirstOrDefault((p) => p.Ccy == ccy);
            if (excchangerate == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
                Console.WriteLine("chetch");
            }
            return excchangerate;
        }

        public IEnumerable<Exchangerate> GetExchangerateByCategory(string base_ccy)
        {
            return exchangerates.Where(p => string.Equals(p.Base_ccy, base_ccy,
                    StringComparison.OrdinalIgnoreCase));
        }
    }
}
