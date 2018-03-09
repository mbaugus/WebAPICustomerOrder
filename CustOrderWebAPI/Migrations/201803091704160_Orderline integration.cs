namespace CustOrderWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Orderlineintegration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        LineNbr = c.Int(nullable: false),
                        Product = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        LineTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OrderLines");
        }
    }
}
