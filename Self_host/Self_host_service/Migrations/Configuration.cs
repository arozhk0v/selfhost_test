namespace Self_host_service.Migrations
{
    using Self_host_service.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Self_host_service.DB.EntityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Self_host_service.DB.EntityDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.ExchangerateList.AddOrUpdate(new Exchangerate[] {
                new Exchangerate() { Id = 1, Base = "EUR", Date = DateTime.Now, rates = new Rates {CAD = 1.4308, GBP = 0.85725, USD = 1.0631} }
                });

        }
    }
}
