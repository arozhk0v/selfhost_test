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


        //static void GetDateExchangerates()
        //{
        //    HttpResponseMessage resp = client.GetAsync("api/exchangerates/1").Result;
        //    resp.EnsureSuccessStatusCode();

        //    var a = resp.Content.ReadAsAsync<IQueryable<Self_host_service.Controllers.ExchangeratesController.GetExchangerates>>().Result;
        //}



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

        static void Main(string[] args)
        {
            client.BaseAddress = new Uri("http://localhost:8080");

            ListAllExchangerates();
            //ListProduct(1);
            //ListProducts("toys");

            // новый api - https://api.privatbank.ua/p24api/pubinfo?exchange&coursid=5

            Console.WriteLine("Press Enter to quit.");
            Console.ReadLine();
        }
    }
}
