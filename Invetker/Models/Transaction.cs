using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Invetker.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public string Ticker { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public ActionType Action { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal Fee { get; set; }

        [Required]
        // Transaction time
        public DateTime Datetime { get; set; }

        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class TransactionAddViewModel
    {
        public string UserId { get; set; }

        [Required]
        public string Ticker { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public ActionType Action { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal Fee { get; set; }

        // Transaction time
        public DateTime Datetime { get; set; }

        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }

    public enum ActionType
    {
        Bought,
        Sold,
        Deposit,
        Withdrawal,
    }
}