using Invetker.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;


namespace Invetker.Controllers
{

    public class AssetsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET api/Assets/GetAssetIdByCrypto/cryptoId
        [HttpGet]
        [Route("api/Assets/GetAssetIdByCrypto/{cryptoId}")]
        public int GetAssetIdByCrypto(int cryptoId)
        {

                var asset = db.Assets.FirstOrDefault(a => a.Type == AssetType.Crypto && a.SymbolId == cryptoId);

                if (asset == null)
                    return 0;

                return asset.AssetId;
            
        }

        // GET api/Assets/GetAssetIdByStock/stockId
        [HttpGet]
        [Route("api/Assets/GetAssetIdByStock/{stockId}")]
        public int GetAssetIdByStock(int stockId)
        {

                var asset = db.Assets.FirstOrDefault(a => a.Type == AssetType.Stock && a.SymbolId == stockId);
                if (asset == null)
                    return 0;  

                return asset.AssetId; 

        }

        [HttpGet]
        [Route("api/Assets/GetLatestVolume/{assetId}")]
        private int GetLatestVolume(int assetId)
        {
            var latestVolume = db.Histories
                .Where(h => h.AssetId == assetId)
                .OrderByDescending(h => h.Timestamp)
                .FirstOrDefault();
            return latestVolume?.Volume ?? 0;
        }

        [HttpGet]
        [Route("api/Assets/GetLatestPrice/{assetId}")]
        private decimal GetLatestPrice(int assetId)
        {
            var latestPrice = db.Histories
                .Where(h => h.AssetId == assetId)
                .OrderByDescending(h => h.Timestamp)
                .FirstOrDefault();
            return latestPrice?.Price ?? 0m;
        }

        [HttpGet]
        [Route("api/Assets/GetPriceChange/{assetId}")]
        public decimal GetPriceChange(int assetId)
        {
            var prices = db.Histories
                .Where(h => h.AssetId == assetId)
                .OrderByDescending(h => h.Timestamp)
                .Select(h => h.Price)
                .Take(2)
                .ToList();
            if (prices.Count < 2) return 0m;
            return (prices[0] - prices[1]);
        }

        [HttpGet]
        [Route("api/Assets/CalculatePercentageChange/{assetId}")]
        public decimal CalculatePercentageChange(int assetId)
        {
            var prices = db.Histories
                .Where(h => h.AssetId == assetId)
                .OrderByDescending(h => h.Timestamp)
                .Select(h => h.Price)
                .Take(2)
                .ToList();

            if (prices.Count < 2 || prices[1] == 0)
                return 0m; 

            var priceChange = prices[0] - prices[1];
            var percentageChange = priceChange / prices[1];
            return percentageChange;
        }

        [HttpGet]
        [Route("api/Assets/GetPriceRecord/{assetId}")]
        public IHttpActionResult GetPriceRecord(int assetId)
        {
            var priceRecords = db.Histories
                .Where(h => h.AssetId == assetId)
                .Select(h => new HistoriesDto { Id = h.Id, AssetId = h.AssetId, Price = h.Price, Volume = h.Volume, Timestamp = h.Timestamp })
                .OrderByDescending(h => h.Timestamp)
                .ToList();
            return Ok(priceRecords);
        }

        [HttpGet]
        [Route("api/Assets/GetMostActiveStocks")]
        public IHttpActionResult GetMostActiveStocks()
        {
            try
            {
                List<AssetActivityDto> mostActiveStocks = new List<AssetActivityDto>();
                var stocks = db.Stocks.ToList();

                foreach (var stock in stocks)
                {
                    var assetId = GetAssetIdByStock(stock.Id);
                    var latestVolume = GetLatestVolume(assetId);
                    var latestPrice = GetLatestPrice(assetId);
                    var priceChange = GetPriceChange(assetId);
                    var priceChangeP = CalculatePercentageChange(assetId);

                    mostActiveStocks.Add(new AssetActivityDto
                    {
                        Symbol = stock.Symbol,
                        Price = latestPrice,
                        Volume = latestVolume,
                        PriceChange = priceChange,
                        PriceChangeP = priceChangeP,
                        DollarVolume = (double)(latestPrice * latestVolume)
                    });
                }

                var topActiveStocks = mostActiveStocks.OrderByDescending(m => m.DollarVolume).Take(5).ToList();
                return Ok(topActiveStocks);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/Assets/GetMostActiveCryptos")]
        public IHttpActionResult GetMostActiveCryptos()
        {
            try
            {
                List<AssetActivityDto> mostActiveCryptos = new List<AssetActivityDto>();
                var cryptos = db.Cryptocurrencies.ToList();

                foreach (var crypto in cryptos)
                {
                    var assetId = GetAssetIdByCrypto(crypto.Id);
                    var latestVolume = GetLatestVolume(assetId);
                    var latestPrice = GetLatestPrice(assetId);
                    var priceChange = GetPriceChange(assetId);
                    var priceChangeP = CalculatePercentageChange(assetId);

                    mostActiveCryptos.Add(new AssetActivityDto
                    {
                        Symbol = crypto.Symbol,
                        Price = latestPrice,
                        Volume = latestVolume,
                        PriceChange = priceChange,
                        PriceChangeP = priceChangeP,
                        DollarVolume = (double)(latestPrice * latestVolume)
                    });
                }

                var topActiveCryptos = mostActiveCryptos.OrderByDescending(m => m.DollarVolume).Take(5).ToList();
                return Ok(topActiveCryptos);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpGet]
        [Route("api/Assets/GetTopGainerStocks")]
        public IHttpActionResult GetTopGainerStocks()
        {
            try
            {
                List<AssetActivityDto> topGainerStocks = new List<AssetActivityDto>();
                var stocks = db.Stocks.ToList();

                foreach (var stock in stocks)
                {
                    var assetId = GetAssetIdByStock(stock.Id);
                    var latestVolume = GetLatestVolume(assetId);
                    var latestPrice = GetLatestPrice(assetId);
                    var priceChange = GetPriceChange(assetId);
                    var priceChangeP = CalculatePercentageChange(assetId);

                    topGainerStocks.Add(new AssetActivityDto
                    {
                        Symbol = stock.Symbol,
                        Price = latestPrice,
                        Volume = latestVolume,
                        PriceChange = priceChange,
                        PriceChangeP = priceChangeP,
                        DollarVolume = (double)(latestPrice * latestVolume)
                    });
                }

                var topGainers = topGainerStocks.OrderByDescending(m => m.PriceChangeP).Take(5).ToList();
                return Ok(topGainers);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/Assets/GetTopGainerCryptos")]
        public IHttpActionResult GetTopGainerCryptos()
        {
            try
            {
                List<AssetActivityDto> topGainerCryptos = new List<AssetActivityDto>();
                var cryptos = db.Cryptocurrencies.ToList();

                foreach (var crypto in cryptos)
                {
                    var assetId = GetAssetIdByCrypto(crypto.Id);
                    var latestVolume = GetLatestVolume(assetId);
                    var latestPrice = GetLatestPrice(assetId);
                    var priceChange = GetPriceChange(assetId);
                    var priceChangeP = CalculatePercentageChange(assetId);

                    topGainerCryptos.Add(new AssetActivityDto
                    {
                        Symbol = crypto.Symbol,
                        Price = latestPrice,
                        Volume = latestVolume,
                        PriceChange = priceChange,
                        PriceChangeP = priceChangeP,
                        DollarVolume = (double)(latestPrice * latestVolume)
                    });
                }

                var topGainers = topGainerCryptos.OrderByDescending(m => m.PriceChangeP).Take(5).ToList();
                return Ok(topGainers);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/Assets/GetTopVolumeStocks")]
        public IHttpActionResult GetTopVolumeStocks()
        {
            try
            {
                List<AssetActivityDto> topVolumeStocks = new List<AssetActivityDto>();
                var stocks = db.Stocks.ToList();

                foreach (var stock in stocks)
                {
                    var assetId = GetAssetIdByStock(stock.Id);
                    var latestVolume = GetLatestVolume(assetId);
                    var latestPrice = GetLatestPrice(assetId);
                    var priceChange = GetPriceChange(assetId);
                    var priceChangeP = CalculatePercentageChange(assetId);

                    topVolumeStocks.Add(new AssetActivityDto
                    {
                        Symbol = stock.Symbol,
                        Price = latestPrice,
                        Volume = latestVolume,
                        PriceChange = priceChange,
                        PriceChangeP = priceChangeP,
                        DollarVolume = (double)(latestPrice * latestVolume)
                    });
                }

                var topVolumes = topVolumeStocks.OrderByDescending(m => m.Volume).Take(5).ToList();
                return Ok(topVolumes);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/Assets/GetTopVolumeCryptos")]
        public IHttpActionResult GetTopVolumeCryptos()
        {
            try
            {
                List<AssetActivityDto> topVolumeCryptos = new List<AssetActivityDto>();
                var cryptos = db.Cryptocurrencies.ToList();

                foreach (var crypto in cryptos)
                {
                    var assetId = GetAssetIdByCrypto(crypto.Id);
                    var latestVolume = GetLatestVolume(assetId);
                    var latestPrice = GetLatestPrice(assetId);
                    var priceChange = GetPriceChange(assetId);
                    var priceChangeP = CalculatePercentageChange(assetId);

                    topVolumeCryptos.Add(new AssetActivityDto
                    {
                        Symbol = crypto.Symbol,
                        Price = latestPrice,
                        Volume = latestVolume,
                        PriceChange = priceChange,
                        PriceChangeP = priceChangeP,
                        DollarVolume = (double)(latestPrice * latestVolume)
                    });
                }

                var topVolumes = topVolumeCryptos.OrderByDescending(m => m.Volume).Take(5).ToList();
                return Ok(topVolumes);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/Assets/GetTopCryptoByMarketCap")]
        public IHttpActionResult GetTopCryptoByMarketCap()
        {
            try
            {
                var cryptos = db.Cryptocurrencies.ToList();

                var topCryptosByMarketCap = cryptos
                    .OrderByDescending(c => c.MarketCap)
                    .Take(5)
                    .Select(c => new AssetActivityDto
                    {
                        Symbol = c.Symbol,
                        MarketCap = c.MarketCap,
                        Price = GetLatestPrice(GetAssetIdByCrypto(c.Id)),
                        Volume = GetLatestVolume(GetAssetIdByCrypto(c.Id)),
                        DollarVolume = (double)(GetLatestPrice(GetAssetIdByCrypto(c.Id)) * GetLatestVolume(GetAssetIdByCrypto(c.Id))),
                        PriceChange = GetPriceChange(GetAssetIdByCrypto(c.Id)),
                        PriceChangeP = CalculatePercentageChange(GetAssetIdByCrypto(c.Id))
                    })
                    .ToList();

                return Ok(topCryptosByMarketCap);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }





        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}