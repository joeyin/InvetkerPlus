using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Invetker.Models
{
    // This table will automatically trigger daily to store users' performances
    public class Performance
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }


        // Total user deposit
        public decimal TotalDeposit { get; set; }

        // Count total property amount
        public decimal NetLiquidityValue { get; set; }

        // Unresized portfolio / Total user deposit
        public decimal Rate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}