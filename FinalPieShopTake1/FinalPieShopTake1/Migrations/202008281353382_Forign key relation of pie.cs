namespace FinalPieShopTake1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Forignkeyrelationofpie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pies", "PieCategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Pies", "PieCategoryId");
            AddForeignKey("dbo.Pies", "PieCategoryId", "dbo.PieCategories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pies", "PieCategoryId", "dbo.PieCategories");
            DropIndex("dbo.Pies", new[] { "PieCategoryId" });
            DropColumn("dbo.Pies", "PieCategoryId");
        }
    }
}
