using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;

namespace Client
{
    class Program
    {
        static HttpClient client = new HttpClient();

        /// <summary>
        /// Получения котрировок для конкретного дня.
        /// </summary>
        /// <param name="date"></param> 
        static void GetDateExchangerates(DateTime date)
        {
            HttpResponseMessage resp = client.GetAsync("api/exchangerates").Result;
            resp.EnsureSuccessStatusCode();

            var exchangerates = resp.Content.ReadAsAsync<IEnumerable<Self_host_service.Models.Exchangerate>>().Result;
            foreach (var ex in exchangerates)
            {
                if (ex.Date == date)
                {
                    Console.WriteLine("{0} {1} {2} {3}", ex.Base, ex.rates.CAD, ex.rates.GBP, ex.rates.USD);
                }
            }

        }


        /// <summary>
        /// Получение всех котировок.
        /// </summary>
        static void ListAllExchangerates()
        {
            HttpResponseMessage resp = client.GetAsync("api/exchangerates").Result;
            resp.EnsureSuccessStatusCode();

            
            var exchangerates = resp.Content.ReadAsAsync<IEnumerable<Self_host_service.Models.Exchangerate>>().Result;
            foreach (var p in exchangerates)
            {
                Console.WriteLine("{0} {1} {2} {3} {4} {5}", p.Id, p.Base, p.Date, p.rates.CAD, p.rates.GBP, p.rates.USD);
            }
        }


        /// <summary>
        /// Получить диапазон значений.
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        static void GetPartDateExchangerates(DateTime date1, DateTime date2)
        {
            HttpResponseMessage resp = client.GetAsync(string.Format("api/exchangerates/date/{0}-{1}", date1.ToString("yyyy.MM.dd"), date2.ToString("yyyy.MM.dd"))).Result;
            resp.EnsureSuccessStatusCode();

            var exchangerates = resp.Content.ReadAsAsync<IEnumerable<Self_host_service.Models.Exchangerate>>().Result;
            foreach (var ex in exchangerates)
            {
                Console.WriteLine("{0} - {1} {2} {3} {4}", ex.Date.ToString("dd.MM.yyyy"), ex.Base, ex.rates.CAD, ex.rates.GBP, ex.rates.USD);
            }
        }

        static void Main(string[] args)
        {
            client.BaseAddress = new Uri("http://localhost:8080");
            
            DateTime dat1 = new DateTime(2016, 10, 20);
            DateTime dat2 = new DateTime(2016, 11, 03);

            GetPartDateExchangerates(dat1, dat2);
            //ListAllExchangerates();
            //ListProducts("toys");

            // новый api - https://api.privatbank.ua/p24api/pubinfo?exchange&coursid=5

            Console.WriteLine("Press Enter to quit.");
            Console.ReadLine();
        }
    }
}
