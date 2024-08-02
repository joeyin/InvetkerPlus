namespace Invetker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class histories : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PricesModels", newName: "HistoriesModels");
            AddColumn("dbo.HistoriesModels", "Volume", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HistoriesModels", "Volume");
            RenameTable(name: "dbo.HistoriesModels", newName: "PricesModels");
        }
    }
}
