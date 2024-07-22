namespace Invetker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CryptoModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssetsModels",
                c => new
                    {
                        AssetId = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        SymbolId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AssetId);
            
            CreateTable(
                "dbo.CryptocurrenciesModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Symbol = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        MarketCap = c.Single(nullable: false),
                        circulatingSupply = c.Single(nullable: false),
                        maxSupply = c.Single(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NewsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AssetId = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AssetsModels", t => t.AssetId, cascadeDelete: true)
                .Index(t => t.AssetId);
            
            CreateTable(
                "dbo.PricesModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AssetId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Timestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AssetsModels", t => t.AssetId, cascadeDelete: true)
                .Index(t => t.AssetId);
            
            CreateTable(
                "dbo.StocksModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Symbol = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PricesModels", "AssetId", "dbo.AssetsModels");
            DropForeignKey("dbo.NewsModels", "AssetId", "dbo.AssetsModels");
            DropIndex("dbo.PricesModels", new[] { "AssetId" });
            DropIndex("dbo.NewsModels", new[] { "AssetId" });
            DropTable("dbo.StocksModels");
            DropTable("dbo.PricesModels");
            DropTable("dbo.NewsModels");
            DropTable("dbo.CryptocurrenciesModels");
            DropTable("dbo.AssetsModels");
        }
    }
}
