using System.ComponentModel.DataAnnotations;

namespace Invetker.Models
{
    public class AssetsModels
    {
        [Key]
        public int AssetId { get; set; }

        [Required]
        public AssetType Type { get; set; }

        [Required]
        public int SymbolId { get; set; }
    }

    public enum AssetType
    {
        Crypto,
        Stock
    }

    public class MostActiveStocksDto
    {
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public int Volume { get; set; }
        public double DollarVolume { get; set; }  // Volume * Price
    }

    public class MostActiveCryptosDto
    {
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public int Volume { get; set; }
        public double DollarVolume { get; set; }
    }



    public class AssetActivityDto
    {
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public int Volume { get; set; }
        public decimal PriceChange { get; set; }
        public decimal PriceChangeP { get; set; }
        public double DollarVolume { get; set; }

        public float MarketCap { get; set; }
    }

}