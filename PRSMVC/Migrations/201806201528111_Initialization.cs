namespace PRSMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialization : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PurchaseRequests", "Total");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseRequests", "Total", c => c.Double(nullable: false));
        }
    }
}
