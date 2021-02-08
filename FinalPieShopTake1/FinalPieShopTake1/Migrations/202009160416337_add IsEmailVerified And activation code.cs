namespace FinalPieShopTake1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsEmailVerifiedAndactivationcode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisterUsers", "IsEmailVerfied", c => c.Boolean(nullable: false));
            AddColumn("dbo.RegisterUsers", "ActivationCode", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegisterUsers", "ActivationCode");
            DropColumn("dbo.RegisterUsers", "IsEmailVerfied");
        }
    }
}
