namespace CustOrderWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NomorecascadingonDeletingcustomer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "CustomerID" });
            AlterColumn("dbo.Orders", "CustomerID", c => c.Int());
            CreateIndex("dbo.Orders", "CustomerID");
            AddForeignKey("dbo.Orders", "CustomerID", "dbo.Customers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "CustomerID" });
            AlterColumn("dbo.Orders", "CustomerID", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "CustomerID");
            AddForeignKey("dbo.Orders", "CustomerID", "dbo.Customers", "Id", cascadeDelete: true);
        }
    }
}
