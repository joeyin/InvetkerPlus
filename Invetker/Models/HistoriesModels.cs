using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Invetker.Models
{
    public class HistoriesModels
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("AssetsModels")]
        public int AssetId { get; set; }
        public virtual AssetsModels AssetsModels { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Volume {  get; set; }

        [Required]
        public DateTime Timestamp { get; set; }
    }

}