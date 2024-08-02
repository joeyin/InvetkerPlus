namespace Invetker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDataSet : DbMigration
    {
        public override void Up()
        {
            Sql(@"
            DELETE FROM CryptocurrenciesModels
            WHERE (Symbol = 'BTC' OR Symbol = 'ETH') AND MarketCap = 100000
        ");

            Sql(@"
    DELETE FROM StocksModels
    WHERE Symbol = 'TSLA';
");

            Sql(@"
    DELETE FROM StocksModels
    WHERE Symbol = 'AAPL';
");



            Sql(@"
    DECLARE @CryptoId INT;
    INSERT INTO CryptocurrenciesModels (Name, Logo, Symbol, Description, MarketCap, CirculatingSupply, MaxSupply, CreatedAt) 
    VALUES (
        'Bitcoin', 
        'https://s2.coinmarketcap.com/static/img/coins/64x64/1.png', 
        'BTC', 
        'Bitcoin (BTC) is a consensus network that enables a new payment system and a completely digital currency. Powered by its users, it is a peer to peer payment network that requires no central authority to operate. On October 31st, 2008, an individual or group of individuals operating under the pseudonym ""Satoshi Nakamoto"" published the Bitcoin Whitepaper and described it as: a purely peer-to-peer version of electronic cash would allow online payments to be sent directly from one party to another without going through a financial institution.', 
        1314762780000,
        19730424,
        21000000,
        CURRENT_TIMESTAMP
    );
    SET @CryptoId = SCOPE_IDENTITY();
    INSERT INTO AssetsModels (Type, SymbolId) 
    VALUES (0, @CryptoId);
");


            Sql(@"
    DECLARE @CryptoId INT;
    INSERT INTO CryptocurrenciesModels (Name, Logo, Symbol, Description, MarketCap, CirculatingSupply, MaxSupply, CreatedAt) 
    VALUES (
        'Ethereum', 
        'https://s2.coinmarketcap.com/static/img/coins/64x64/1027.png', 
        'ETH', 
        'Ethereum (ETH) is a decentralized platform that runs smart contracts: applications that run exactly as programmed without any possibility of downtime, fraud or third-party interference.', 
        411382776000,
        120228312,
        0,
        CURRENT_TIMESTAMP
    );
    SET @CryptoId = SCOPE_IDENTITY();
    INSERT INTO AssetsModels (Type, SymbolId) 
    VALUES (0, @CryptoId);
");


            Sql(@"
    DECLARE @CryptoId INT;
    INSERT INTO CryptocurrenciesModels (Name, Logo, Symbol, Description, MarketCap, CirculatingSupply, MaxSupply, CreatedAt) 
    VALUES (
        'Binance', 
        'https://s2.coinmarketcap.com/static/img/coins/64x64/1839.png', 
        'BNB', 
        'Binance Coin (BNB) is a cryptocurrency used to pay fees on the Binance cryptocurrency exchange. Fees paid in Binance Coin on the exchange receive a discount.', 
        85357400000,
        145937872,
        170532785,
        CURRENT_TIMESTAMP
    );
    SET @CryptoId = SCOPE_IDENTITY();
    INSERT INTO AssetsModels (Type, SymbolId) 
    VALUES (0, @CryptoId);
");


            Sql(@"
    DECLARE @CryptoId INT;
    INSERT INTO CryptocurrenciesModels (Name, Logo, Symbol, Description, MarketCap, CirculatingSupply, MaxSupply, CreatedAt) 
    VALUES (
        'Solana', 
        'https://s2.coinmarketcap.com/static/img/coins/64x64/5426.png', 
        'SOL', 
        'Solana (SOL) is a highly functional open source project that banks on blockchain technology''s permissionless nature to provide decentralized finance (DeFi) solutions.', 
        84354560000,
        464502432,
        489270616,
        CURRENT_TIMESTAMP
    );
    SET @CryptoId = SCOPE_IDENTITY();
    INSERT INTO AssetsModels (Type, SymbolId) 
    VALUES (0, @CryptoId);
");

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
    INSERT INTO AssetsModels (Type, SymbolId) VALUES (1, @StockId);

    -- Insert historical data for Tesla
    INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
    VALUES (@StockId, 720.00, 10000, CURRENT_TIMESTAMP);
    INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
    VALUES (@StockId, 725.00, 10500, DATEADD(day, 1, CURRENT_TIMESTAMP));
    INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
    VALUES (@StockId, 730.00, 11000, DATEADD(day, 2, CURRENT_TIMESTAMP));
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
    INSERT INTO AssetsModels (Type, SymbolId) VALUES (1, @StockId);

    -- Insert historical data for Apple
    INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
    VALUES (@StockId, 150.00, 20000, CURRENT_TIMESTAMP);
    INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
    VALUES (@StockId, 155.00, 20500, DATEADD(day, 1, CURRENT_TIMESTAMP));
    INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
    VALUES (@StockId, 160.00, 21000, DATEADD(day, 2, CURRENT_TIMESTAMP));
");


            Sql(@"
    DECLARE @StockId INT;
    INSERT INTO StocksModels (Name, Logo, Symbol, Description, CreatedAt) 
    VALUES (
        'Google LLC', 
        'https://upload.wikimedia.org/wikipedia/commons/thumb/2/2f/Google_2015_logo.svg/1024px-Google_2015_logo.svg.png', 
        'GOOGL', 
        'Google, a major technology company, is known for its search engine and its broad range of products and services including Android, advertising services, and YouTube. Google''s advancements in AI and cloud computing make it a leader in the tech industry, continuously expanding its influence and capabilities across new markets.', 
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
        'Microsoft Corporation', 
        'https://upload.wikimedia.org/wikipedia/commons/thumb/9/96/Microsoft_logo_%282012%29.svg/800px-Microsoft_logo_%282012%29.svg.png', 
        'MSFT', 
        'Microsoft, headquartered in Redmond, Washington, develops, licenses, and supports a wide range of software products, services, and devices. The company''s best-known products include the Windows operating systems, the Microsoft Office suite, and the Internet Explorer and Edge web browsers. Its flagship hardware products are the Xbox video game consoles and the Microsoft Surface tablet lineup.', 
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
        'Amazon.com, Inc.', 
        'https://upload.wikimedia.org/wikipedia/commons/thumb/a/a9/Amazon_logo.svg/1024px-Amazon_logo.svg.png', 
        'AMZN', 
        'Amazon is known for its disruption of well-established industries through technological innovation and mass scale. It is the world''s largest e-commerce marketplace, AI assistant provider, and cloud computing platform as measured by revenue and market capitalization.', 
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
        'Facebook, Inc.', 
        'https://upload.wikimedia.org/wikipedia/commons/thumb/8/89/Facebook_Logo_%282019%29.svg/800px-Facebook_Logo_%282019%29.svg.png', 
        'FB', 
        'Facebook is an American online social media and social networking service company based in Menlo Park, California. It is one of the world''s most valuable companies and is considered one of the Big Five technology companies along with Microsoft, Amazon, Apple, and Google.', 
        CURRENT_TIMESTAMP
    );
    SET @StockId = SCOPE_IDENTITY();
    INSERT INTO AssetsModels (Type, SymbolId) 
    VALUES (1, @StockId);
");
        }
        
        public override void Down()
        {
        }
    }
}
