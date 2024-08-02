using System;
using System.ComponentModel.DataAnnotations;

namespace Invetker.Models
{
    public class StocksModels
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Logo { get; set; }

        [Required]
        public string Symbol { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class StocksModelsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}