using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Invetker.Models
{
    public class TransactionsModels
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public AssetType AssetType { get; set; }

        [Required]
        public int AssetId { get; set; }

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
        public AssetType AssetType { get; set; }

        [Required]
        public int AssetId { get; set; }

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

        StocksModels Stocks { get; set; }

    }

    public class TransactionEditViewModel
    {
        [Key]
        public int Id { get; set; }

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

    public class TransactionViewModel
    {
        public int Id { get; set; }

        public int AssetId { get; set; }

        public int ?SymbolId { get; set; }

        public AssetType AssetType { get; set; }

        public string Symbol { get; set; }

        public ActionType Action { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Fee { get; set; }

        public DateTime Datetime { get; set; }

        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public enum ActionType
    {
        Bought,
        Sold,
        //Deposit,
        //Withdrawal,
    }
}