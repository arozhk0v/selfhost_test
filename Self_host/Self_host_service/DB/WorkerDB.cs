using Newtonsoft.Json;
using Self_host_service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_host_service.DB
{
    public class WorkerDB
    {
        public static string Json { get; set; }

        public static Exchangerate exchangerate { get; set; }


        /// <summary>
        /// Метод удаления всех строк в таблице
        /// </summary>
        public static void TableClear(string TableName)
        {
            EntityDbContext context = new EntityDbContext();

            context.Database.ExecuteSqlCommand("TRUNCATE TABLE [" + TableName + "]");
        }


        /// <summary>
        /// Добавление нового элемента в таблицу
        /// </summary>
        /// <param name="url"></param>
        public static void AddNewExchangerate(string url)
        {
            //предположение об уникальности
            bool unique = true;

            Json = RequestAPI.GET(url);
            exchangerate = JsonConvert.DeserializeObject<Exchangerate>(Json);

            EntityDbContext context = new EntityDbContext();

            //проверка на уникальность данных
            foreach (var a in context.ExchangerateList)
            {
                if (a.Date == exchangerate.Date) unique = false;
            }

            // Добавить в DbSet
            if (unique) context.ExchangerateList.Add(exchangerate);

            // Сохранить изменения в базе данных
            context.SaveChanges();
        }


        /// <summary>
        /// Добавление новых элементов в таблицу.
        /// </summary>
        public static void AddNewExchangerates(DateTime date1, DateTime date2)
        {
            if (date1 != date2)
            {

                for (var currentDay = date1; currentDay <= date2; currentDay = currentDay.AddDays(1))
                {
                    AddNewExchangerate("http://api.fixer.io/" + currentDay.Year + "-" + currentDay.ToString("MM") + "-" + currentDay.ToString("dd"));
                }
            }
            else AddNewExchangerate("http://api.fixer.io/" + date1.Year + "-" + date1.ToString("MM") + "-" + date1.ToString("dd"));

        }

        /// <summary>
        /// Добавление котировок текущего месяца.
        /// </summary>
        public static void AddExchangeratesCurrentMounth()
        {
            var mounth = DateTime.Now.Month;
            var year = DateTime.Now.Year;
            var date = new DateTime(year, mounth, 1);

            AddNewExchangerates(date, DateTime.Today);
        }

    }
}
