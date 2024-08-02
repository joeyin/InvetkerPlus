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
}