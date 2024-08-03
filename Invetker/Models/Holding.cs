using System.ComponentModel.DataAnnotations;

namespace Invetker.Models
{
    public class HoldingViewModel
    {
        public string Symbol { get; set; }

        [Required]
        public decimal Position { get; set; }
        
        [Required]
        public decimal Amount { get; set; }

        public int ?AssetId { get; set; }

        public AssetType AssetType { get; set; }

        [Required]
        public decimal AvgPrice { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal Change { get; set; }

        [Required]
        public decimal DailyPL { get; set; }

        [Required]
        public decimal UnrealizedPL { get; set; }

        [Required]
        public decimal MarketVal { get; set; }
    }

    public class TopPositionViewModel
    {
        [Required]
        public int No { get; set; }

        [Required]
        public string Symbol { get; set; }

        [Required]
        public string Icon { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Position { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal DailyPL { get; set; }

        [Required]
        public decimal UnrealizedPL { get; set; }
    }
}