using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Invetker.Models
{
    public class PricesModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Assets")]
        public int AssetId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }
    }
    public class PricesModelDto
    {
        public int Id { get; set; }

        public int AssetId { get; set; }

        public decimal Price { get; set; }

        public DateTime Timestamp { get; set; }

        public string noted { get; set; }
    }
}