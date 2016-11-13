using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Client
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static void ListAllExchangerate()
        {
            HttpResponseMessage resp = client.GetAsync("api/exchangerates").Result;
            resp.EnsureSuccessStatusCode();

            var exchangerates = resp.Content.ReadAsAsync<IEnumerable<Self_host_service.Models.Exchangerate>>().Result;
            foreach (var p in exchangerates)
            {
                Console.WriteLine("{0} {1} {2} ({3})", p.Ccy, p.Base_ccy, p.Buy, p.Sale);
            }
        }

        static void Main(string[] args)
        {
            client.BaseAddress = new Uri("http://localhost:8080");

            ListAllExchangerate();
            //ListProduct(1);
            //ListProducts("toys");

            Console.WriteLine("Press Enter to quit.");
            Console.ReadLine();
        }
    }
}
