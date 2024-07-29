namespace Invetker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCryptocurrencies : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CryptocurrenciesModels", "Logo", c => c.String(nullable: false));

            // Add some data
            Sql(@"
                DECLARE @CryptoId INT;
                INSERT INTO CryptocurrenciesModels (Name, Logo, Symbol, Description, MarketCap, circulatingSupply, maxSupply, CreatedAt) 
                VALUES (
                    'Bitcoin', 
                    'https://s2.coinmarketcap.com/static/img/coins/64x64/1.png', 
                    'BTC', 
                    'Bitcoin (BTC) is a consensus network that enables a new payment system and a completely digital currency. Powered by its users, it is a peer to peer payment network that requires no central authority to operate. On October 31st, 2008, an individual or group of individuals operating under the pseudonym ""Satoshi Nakamoto"" published the Bitcoin Whitepaper and described it as: a purely peer-to-peer version of electronic cash would allow online payments to be sent directly from one party to another without going through a financial institution.', 
                    100000,
                    100000,
                    100000,
                    CURRENT_TIMESTAMP
                );
                SET @CryptoId = SCOPE_IDENTITY();
                INSERT INTO AssetsModels (Type, SymbolId) 
                VALUES (0, @CryptoId);
            ");

            Sql(@"
                DECLARE @CryptoId INT;
                INSERT INTO CryptocurrenciesModels (Name, Logo, Symbol, Description, MarketCap, circulatingSupply, maxSupply, CreatedAt) 
                VALUES (
                    'Ethereum', 
                    'https://s2.coinmarketcap.com/static/img/coins/64x64/1027.png', 
                    'ETH', 
                    'Ethereum (ETH) is a smart contract platform that enables developers to build decentralized applications (dapps) conceptualized by Vitalik Buterin in 2013. ETH is the native currency for the Ethereum platform and also works as the transaction fees to miners on the Ethereum network. Ethereum is the pioneer for blockchain based smart contracts. When running on the blockchain a smart contract becomes like a self-operating computer program that automatically executes when specific conditions are met. On the blockchain, smart contracts allow for code to be run exactly as programmed without any possibility of downtime, censorship, fraud or third-party interference. It can facilitate the exchange of money, content, property, shares, or anything of value. The Ethereum network went live on July 30th, 2015 with 72 million Ethereum premined.', 
                    100000,
                    100000,
                    100000,                    
                    CURRENT_TIMESTAMP
                );
                SET @CryptoId = SCOPE_IDENTITY();
                INSERT INTO AssetsModels (Type, SymbolId) 
                VALUES (0, @CryptoId);
            ");
        }
        
        public override void Down()
        {
            DropColumn("dbo.CryptocurrenciesModels", "Logo");
        }
    }
}
