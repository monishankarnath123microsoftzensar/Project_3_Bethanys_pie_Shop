namespace FinalPieShopTake1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addaddressvalidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RegisterUsers", "Address", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RegisterUsers", "Address", c => c.String());
        }
    }
}
