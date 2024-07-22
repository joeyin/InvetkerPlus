using System;
using System.ComponentModel.DataAnnotations;

namespace Invetker.Models
{
    public class CryptocurrenciesModels
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Symbol { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public float MarketCap { get; set; }

        [Required]
        public float circulatingSupply { get; set; }

        [Required]
        public float maxSupply { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}