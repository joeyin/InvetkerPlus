namespace Invetker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDataSet2 : DbMigration
    {
        public override void Up()
        {
            Sql(@"
    DELETE FROM HistoriesModels;
    DELETE FROM AssetsModels;
    DELETE FROM CryptocurrenciesModels;
    DELETE FROM StocksModels;
");
        }
        
        public override void Down()
        {
        }
    }
}
