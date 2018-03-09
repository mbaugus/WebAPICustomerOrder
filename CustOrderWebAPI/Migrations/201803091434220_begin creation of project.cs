namespace CustOrderWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class begincreationofproject : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        CreditLimit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 80),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Fulfilled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "CustomerID" });
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
        }
    }
}
