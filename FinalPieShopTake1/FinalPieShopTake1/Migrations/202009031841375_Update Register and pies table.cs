namespace FinalPieShopTake1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRegisterandpiestable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pies", "Rating", c => c.Single(nullable: false));
            AddColumn("dbo.RegisterUsers", "ConfirmPassword", c => c.String(nullable: false));
            AlterColumn("dbo.RegisterUsers", "Address", c => c.String(nullable: false, maxLength: 40));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RegisterUsers", "Address", c => c.String(nullable: false));
            DropColumn("dbo.RegisterUsers", "ConfirmPassword");
            DropColumn("dbo.Pies", "Rating");
        }
    }
}
