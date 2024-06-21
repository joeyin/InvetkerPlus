using Flurl;
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
        public decimal Amount { get; set; }

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
        public string Ticker { get; set; }

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