namespace FinalPieShopTake1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValidationinRegisterUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RegisterUsers", "FName", c => c.String(nullable: false));
            AlterColumn("dbo.RegisterUsers", "LName", c => c.String(nullable: false));
            AlterColumn("dbo.RegisterUsers", "PhoneNo", c => c.Long(nullable: false));
            AlterColumn("dbo.RegisterUsers", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.RegisterUsers", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RegisterUsers", "Password", c => c.String());
            AlterColumn("dbo.RegisterUsers", "Email", c => c.String());
            AlterColumn("dbo.RegisterUsers", "PhoneNo", c => c.Int(nullable: false));
            AlterColumn("dbo.RegisterUsers", "LName", c => c.String());
            AlterColumn("dbo.RegisterUsers", "FName", c => c.String());
        }
    }
}
