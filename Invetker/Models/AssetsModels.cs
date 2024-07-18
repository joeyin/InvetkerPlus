using System.ComponentModel.DataAnnotations;

namespace Invetker.Models
{
    public class AssetsModel
    {
        [Key]
        public int AssetId { get; set; }

        [Required]
        public AssetType Action { get; set; }

        [Required]
        public int SymbolId { get; set; }
    }

    public enum AssetType
    {
        Crypto,
        Equity,
    }
}