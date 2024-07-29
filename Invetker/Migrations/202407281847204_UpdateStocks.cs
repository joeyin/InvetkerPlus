namespace Invetker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStocks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StocksModels", "Logo", c => c.String(nullable: false));

            // Add some data
            Sql(@"
                DECLARE @StockId INT;
                INSERT INTO StocksModels (Name, Logo, Symbol, Description, CreatedAt) 
                VALUES (
                    'Tesla, Inc. Common Stock', 
                    'https://upload.wikimedia.org/wikipedia/commons/thumb/e/e8/Tesla_logo.png/1200px-Tesla_logo.png', 
                    'TSLA', 
                    'Tesla is a vertically integrated battery electric vehicle automaker and developer of autonomous driving software. The company has multiple vehicles in its fleet, which include luxury and midsize sedans, crossover SUVs, a light truck, and a semi-truck. Tesla also plans to begin selling more affordable vehicles, and a sports car. Global deliveries in 2023 were a little over 1.8 million vehicles. The company also sells batteries for stationary storage for residential and commercial properties including utilities and solar panels and solar roofs for energy generation. Tesla also owns a fast-charging network.', 
                    CURRENT_TIMESTAMP
                );
                SET @StockId = SCOPE_IDENTITY();
                INSERT INTO AssetsModels (Type, SymbolId) 
                VALUES (1, @StockId);
            ");

            Sql(@"
                DECLARE @StockId INT;
                INSERT INTO StocksModels (Name, Logo, Symbol, Description, CreatedAt) 
                VALUES (
                    'Apple Inc.', 
                    'https://upload.wikimedia.org/wikipedia/commons/thumb/f/fa/Apple_logo_black.svg/976px-Apple_logo_black.svg.png', 
                    'AAPL', 
                    'Apple is among the largest companies in the world, with a broad portfolio of hardware and software products targeted at consumers and businesses. Apple''s iPhone makes up a majority of the firm sales, and Apple''s other products like Mac, iPad, and Watch are designed around the iPhone as the focal point of an expansive software ecosystem. Apple has progressively worked to add new applications, like streaming video, subscription bundles, and augmented reality. The firm designs its own software and semiconductors while working with subcontractors like Foxconn and TSMC to build its products and chips. Slightly less than half of Apple''s sales come directly through its flagship stores, with a majority of sales coming indirectly through partnerships and distribution.', 
                    CURRENT_TIMESTAMP
                );
                SET @StockId = SCOPE_IDENTITY();
                INSERT INTO AssetsModels (Type, SymbolId) 
                VALUES (1, @StockId);
            ");
        }
        
        public override void Down()
        {
            DropColumn("dbo.StocksModels", "Logo");
        }
    }
}
