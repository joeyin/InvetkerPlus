namespace Invetker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDataSet3 : DbMigration
    {
        public override void Up()
        {


            Sql(@"BEGIN TRANSACTION;

DECLARE @CryptoId INT, @AssetId INT;
INSERT INTO CryptocurrenciesModels (Name, Logo, Symbol, Description, MarketCap, CirculatingSupply, MaxSupply, CreatedAt) 
VALUES (
    'Bitcoin', 
    'https://s2.coinmarketcap.com/static/img/coins/64x64/1.png', 
    'BTC', 
    'Bitcoin (BTC) is a consensus network that enables a new payment system and a completely digital currency. Powered by its users, it is a peer to peer payment network that requires no central authority to operate.', 
    1314762780000,
    19730424,
    21000000,
    CURRENT_TIMESTAMP
);
SET @CryptoId = SCOPE_IDENTITY();
INSERT INTO AssetsModels (Type, SymbolId) VALUES (0, @CryptoId);
SET @AssetId = SCOPE_IDENTITY();

INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
VALUES (@AssetId, 50000, 3500, DATEADD(day, -2, CURRENT_TIMESTAMP));
INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
VALUES (@AssetId, 51000, 3650, DATEADD(day, -1, CURRENT_TIMESTAMP));
INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
VALUES (@AssetId, 52000, 3750, CURRENT_TIMESTAMP);

DECLARE @EthereumId INT;
INSERT INTO CryptocurrenciesModels (Name, Logo, Symbol, Description, MarketCap, CirculatingSupply, MaxSupply, CreatedAt) 
VALUES (
    'Ethereum', 
    'https://s2.coinmarketcap.com/static/img/coins/64x64/1027.png', 
    'ETH', 
    'Ethereum (ETH) is a decentralized platform that runs smart contracts.', 
    411382776000,
    120228312,
    0,
    CURRENT_TIMESTAMP
);
SET @EthereumId = SCOPE_IDENTITY();
INSERT INTO AssetsModels (Type, SymbolId) VALUES (0, @EthereumId);
SET @AssetId = SCOPE_IDENTITY();

INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
VALUES (@AssetId, 3000, 5000, DATEADD(day, -2, CURRENT_TIMESTAMP));
INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
VALUES (@AssetId, 3100, 5100, DATEADD(day, -1, CURRENT_TIMESTAMP));
INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
VALUES (@AssetId, 3200, 5200, CURRENT_TIMESTAMP);

DECLARE @TeslaId INT;
INSERT INTO StocksModels (Name, Logo, Symbol, Description, CreatedAt) 
VALUES (
    'Tesla, Inc. Common Stock', 
    'https://upload.wikimedia.org/wikipedia/commons/thumb/e/e8/Tesla_logo.png/1200px-Tesla_logo.png', 
    'TSLA', 
    'Tesla is a vertically integrated battery electric vehicle automaker.', 
    CURRENT_TIMESTAMP
);
SET @TeslaId = SCOPE_IDENTITY();
INSERT INTO AssetsModels (Type, SymbolId) VALUES (1, @TeslaId);
SET @AssetId = SCOPE_IDENTITY();

INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
VALUES (@AssetId, 720.00, 10000, DATEADD(day, -2, CURRENT_TIMESTAMP));
INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
VALUES (@AssetId, 725.00, 10500, DATEADD(day, -1, CURRENT_TIMESTAMP));
INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
VALUES (@AssetId, 730.00, 11000, CURRENT_TIMESTAMP);

DECLARE @AppleId INT;
INSERT INTO StocksModels (Name, Logo, Symbol, Description, CreatedAt) 
VALUES (
    'Apple Inc.', 
    'https://upload.wikimedia.org/wikipedia/commons/thumb/f/fa/Apple_logo_black.svg/976px-Apple_logo_black.svg.png', 
    'AAPL', 
    'Apple is among the largest companies in the world, offering a broad portfolio of products.', 
    CURRENT_TIMESTAMP
);
SET @AppleId = SCOPE_IDENTITY();
INSERT INTO AssetsModels (Type, SymbolId) VALUES (1, @AppleId);
SET @AssetId = SCOPE_IDENTITY();

INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
VALUES (@AssetId, 160.00, 21000, DATEADD(day, -2, CURRENT_TIMESTAMP));
INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
VALUES (@AssetId, 155.00, 20500, DATEADD(day, -1, CURRENT_TIMESTAMP));
INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
VALUES (@AssetId, 150.00, 20000, CURRENT_TIMESTAMP);

DECLARE @GoogleId INT;
INSERT INTO StocksModels (Name, Logo, Symbol, Description, CreatedAt) 
VALUES (
    'Google LLC', 
    'https://upload.wikimedia.org/wikipedia/commons/thumb/2/2f/Google_2015_logo.svg/1024px-Google_2015_logo.svg.png', 
    'GOOGL', 
    'Google, a major technology company, is known for its search engine and its broad range of products and services including Android, advertising services, and YouTube. Google''s advancements in AI and cloud computing make it a leader in the tech industry, continuously expanding its influence and capabilities across new markets.', 
    CURRENT_TIMESTAMP
);
SET @GoogleId = SCOPE_IDENTITY();
INSERT INTO AssetsModels (Type, SymbolId) VALUES (1, @GoogleId);
SET @AssetId = SCOPE_IDENTITY();

INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
VALUES (@AssetId, 2760.00, 1550, DATEADD(day, -2, CURRENT_TIMESTAMP));
INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
VALUES (@AssetId, 2745.00, 1525, DATEADD(day, -1, CURRENT_TIMESTAMP));
INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
VALUES (@AssetId, 2730.00, 1500, CURRENT_TIMESTAMP);

DECLARE @MicrosoftId INT;
INSERT INTO StocksModels (Name, Logo, Symbol, Description, CreatedAt) 
VALUES (
    'Microsoft Corporation', 
    'https://upload.wikimedia.org/wikipedia/commons/thumb/9/96/Microsoft_logo_%282012%29.svg/800px-Microsoft_logo_%282012%29.svg.png', 
    'MSFT', 
    'Microsoft, headquartered in Redmond, Washington, develops, licenses, and supports a wide range of software products, services, and devices.', 
    CURRENT_TIMESTAMP
);
SET @MicrosoftId = SCOPE_IDENTITY();
INSERT INTO AssetsModels (Type, SymbolId) VALUES (1, @MicrosoftId);
SET @AssetId = SCOPE_IDENTITY();

INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
VALUES (@AssetId, 314.00, 2100, DATEADD(day, -2, CURRENT_TIMESTAMP));
INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
VALUES (@AssetId, 312.00, 2050, DATEADD(day, -1, CURRENT_TIMESTAMP));
INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
VALUES (@AssetId, 310.00, 2000, CURRENT_TIMESTAMP);

DECLARE @AmazonId INT;
INSERT INTO StocksModels (Name, Logo, Symbol, Description, CreatedAt) 
VALUES (
    'Amazon.com, Inc.', 
    'https://upload.wikimedia.org/wikipedia/commons/thumb/a/a9/Amazon_logo.svg/1024px-Amazon_logo.svg.png', 
    'AMZN', 
    'Amazon is known for its disruption of well-established industries through technological innovation and mass scale.', 
    CURRENT_TIMESTAMP
);
SET @AmazonId = SCOPE_IDENTITY();
INSERT INTO AssetsModels (Type, SymbolId) VALUES (1, @AmazonId);
SET @AssetId = SCOPE_IDENTITY();

INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
VALUES (@AssetId, 3350.00, 2600, DATEADD(day, -2, CURRENT_TIMESTAMP));
INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
VALUES (@AssetId, 3325.00, 2550, DATEADD(day, -1, CURRENT_TIMESTAMP));
INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
VALUES (@AssetId, 3300.00, 2500, CURRENT_TIMESTAMP);

DECLARE @FacebookId INT;
INSERT INTO StocksModels (Name, Logo, Symbol, Description, CreatedAt) 
VALUES (
    'Facebook, Inc.', 
    'https://upload.wikimedia.org/wikipedia/commons/thumb/8/89/Facebook_Logo_%282019%29.svg/800px-Facebook_Logo_%282019%29.svg.png', 
    'FB', 
    'Facebook is an American online social media and social networking service company based in Menlo Park, California.', 
    CURRENT_TIMESTAMP
);
SET @FacebookId = SCOPE_IDENTITY();
INSERT INTO AssetsModels (Type, SymbolId) VALUES (1, @FacebookId);
SET @AssetId = SCOPE_IDENTITY();

INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
VALUES (@AssetId, 280.00, 3200, DATEADD(day, -2, CURRENT_TIMESTAMP));
INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
VALUES (@AssetId, 275.00, 3100, DATEADD(day, -1, CURRENT_TIMESTAMP));
INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
VALUES (@AssetId, 270.00, 3000, CURRENT_TIMESTAMP);

DECLARE @BinanceId INT;
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
SET @BinanceId = SCOPE_IDENTITY();
INSERT INTO AssetsModels (Type, SymbolId) VALUES (0, @BinanceId);
SET @AssetId = SCOPE_IDENTITY();

INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
VALUES (@AssetId, 510, 10500, DATEADD(day, -2, CURRENT_TIMESTAMP));
INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
VALUES (@AssetId, 505, 10250, DATEADD(day, -1, CURRENT_TIMESTAMP));
INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
VALUES (@AssetId, 500, 10000, CURRENT_TIMESTAMP);

DECLARE @SolanaId INT;
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
SET @SolanaId = SCOPE_IDENTITY();
INSERT INTO AssetsModels (Type, SymbolId) VALUES (0, @SolanaId);
SET @AssetId = SCOPE_IDENTITY();

INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
VALUES (@AssetId, 160, 5500, DATEADD(day, -2, CURRENT_TIMESTAMP));
INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
VALUES (@AssetId, 155, 5250, DATEADD(day, -1, CURRENT_TIMESTAMP));
INSERT INTO HistoriesModels (AssetId, Price, Volume, Timestamp)
VALUES (@AssetId, 150, 5000, CURRENT_TIMESTAMP);

COMMIT TRANSACTION;");
        }
        
        public override void Down()
        {
        }
    }
}
