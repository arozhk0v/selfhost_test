namespace Self_host_service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Exchangerates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Base = c.String(),
                        Date = c.DateTime(nullable: false),
                        rates_CAD = c.Double(nullable: false),
                        rates_GBP = c.Double(nullable: false),
                        rates_USD = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Exchangerates");
        }
    }
}
