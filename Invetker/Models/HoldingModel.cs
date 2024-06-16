using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Invetker.Models
{
    public class HoldingViewModel
    {
        [Required]
        public string Ticker { get; set; }

        [Required]
        public decimal Position { get; set; }
        
        [Required]
        public decimal? Amount { get; set; }

        [Required]
        public decimal? AvgPrice { get; set; }

    }
}