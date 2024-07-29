namespace Invetker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTransactions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransactionsModels", "AssetType", c => c.Int(nullable: false));
            DropColumn("dbo.TransactionsModels", "Ticker");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TransactionsModels", "Ticker", c => c.String(nullable: false));
            DropColumn("dbo.TransactionsModels", "AssetType");
        }
    }
}
