using Invetker.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Razor.Generator;
using System.Web.Razor.Tokenizer.Symbols;

namespace Invetker.Controllers
{
    [Authorize]
    public class TransactionController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private string userId;

        public TransactionController()
        {
            userId = User.Identity.GetUserId();
        }

        /// <summary>
        /// Returns all transactions in the system by the current user.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all transactions in the database.
        /// </returns>
        /// <example>
        /// GET: api/Transaction/list
        /// </example>
        [HttpGet]
        [ResponseType(typeof(TransactionsModels))]
        public IHttpActionResult List()
        {
            List<TransactionViewModel> StockTransactions = db.Transactions
            .Where(t => t.UserId == userId && t.AssetType == AssetType.Stock)
            .Join(
                db.Assets,
                t => t.AssetId,
                asset => asset.AssetId,
                (t, asset) => new {
                    t.Id,
                    asset.SymbolId,
                    asset.AssetId,
                    t.Datetime,
                    t.Action,
                    t.Quantity,
                    t.Price,
                    t.Fee
                }
            )
            .Join(
                db.Stocks,
                ta => ta.SymbolId,
                s => s.Id,
                (ta, s) => new TransactionViewModel
                {
                    Id = ta.Id,
                    AssetId = ta.AssetId,
                    AssetType = AssetType.Stock,
                    Symbol = s.Symbol,
                    Action = ta.Action,
                    Quantity = ta.Quantity,
                    Price = ta.Price,
                    Fee = ta.Fee,
                    DateTime = ta.Datetime
                }
            )
            .ToList();

            List<TransactionViewModel> CryptoTransactions = db.Transactions
            .Where(t => t.UserId == userId && t.AssetType == AssetType.Crypto)
            .Join(
                db.Assets,
                t => t.AssetId,
                asset => asset.AssetId,
                (t, asset) => new {
                    t.Id,
                    asset.SymbolId,
                    asset.AssetId,
                    t.Datetime,
                    t.Action,
                    t.Quantity,
                    t.Price,
                    t.Fee
                }
            )
            .Join(
                db.Stocks,
                ta => ta.SymbolId,
                s => s.Id,
                (ta, s) => new TransactionViewModel
                {
                    Id = ta.Id,
                    AssetId = ta.AssetId,
                    AssetType = AssetType.Crypto,
                    Symbol = s.Symbol,
                    Action = ta.Action,
                    Quantity = ta.Quantity,
                    Price = ta.Price,
                    Fee = ta.Fee,
                    DateTime = ta.Datetime
                }
            )
            .ToList();

            List<TransactionViewModel> combinedTransactions = StockTransactions
                .Cast<TransactionViewModel>()
                .Union(CryptoTransactions.Cast<TransactionViewModel>())
                .OrderByDescending(t => t.DateTime)
                .ToList();

            return Ok(combinedTransactions);
        }

        /// <summary>
        /// Create a transaction to the system
        /// </summary>
        /// <param name="transaction">JSON FORM DATA of an transaction</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Transaction ID, Transaction Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/Transaction
        /// FORM DATA: Transaction JSON Object
        /// </example>
        [ResponseType(typeof(TransactionsModels))]
        [HttpPost]
        public IHttpActionResult Index(TransactionAddViewModel transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TransactionsModels newTransaction = new TransactionsModels();
            newTransaction.UserId = User.Identity.GetUserId();
            newTransaction.AssetType = transaction.AssetType;
            newTransaction.AssetId = transaction.AssetId;
            newTransaction.Quantity = transaction.Quantity;
            newTransaction.Action = transaction.Action;
            newTransaction.Price = transaction.Price;
            newTransaction.Fee = transaction.Fee;
            newTransaction.Datetime = transaction.Datetime;
            newTransaction.Notes = transaction.Notes;
            newTransaction.CreatedAt = DateTime.Now;

            db.Transactions.Add(newTransaction);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = newTransaction.Id }, newTransaction);
        }

        /// <summary>
        /// Update a particular transaction in the system with PUT Data input
        /// </summary>
        /// <param name="id">Represents the Transaction ID primary key</param>
        /// <param name="transaction">JSON FORM DATA of a transaction</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// PUT: api/Transactions/5
        /// FORM DATA: Transaction JSON Object
        /// </example>
        [Route("api/Transaction/{id}")]
        [ResponseType(typeof(TransactionsModels))]
        [HttpPut]
        public IHttpActionResult Update(int id, TransactionEditViewModel transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string userId = User.Identity.GetUserId();
            TransactionsModels Row = db.Transactions.Where(t => t.Id == id).Where(t => t.UserId == userId).FirstOrDefault();

            if (Row == null)
            {
                //return NotFound();
                return Content(HttpStatusCode.NotFound, "");
            }

            Row.AssetType = transaction.AssetType;
            Row.AssetId = transaction.AssetId;
            Row.Quantity = transaction.Quantity;
            Row.Action = transaction.Action;
            Row.Price = transaction.Price;
            Row.Fee = transaction.Fee;
            Row.Datetime = transaction.Datetime;
            Row.Notes = transaction.Notes;

            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Returns transaction in the system by the current user.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: An transaction in the system matching up to the transcation ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the transaction</param>
        /// <example>
        /// GET: api/Transaction/1
        /// </example>
        [Route("api/Transaction/{id}")]
        [ResponseType(typeof(TransactionsModels))]
        [HttpGet]
        public IHttpActionResult Find(int id)
        {
            string userId = User.Identity.GetUserId();
            TransactionsModels Row = db.Transactions.Where(t => t.Id == id).Where(t => t.UserId == userId).FirstOrDefault();
            TransactionsModels Transaction = new TransactionsModels();

            Debug.WriteLine(666);

            if (Row == null)
            {
                //return NotFound();
                return Content(HttpStatusCode.NotFound, "");
            }

            Transaction = new TransactionsModels
            {
                AssetId = Row.AssetId,
                Quantity = Row.Quantity,
                Action = Row.Action,
                Price = Row.Price,
                Fee = Row.Fee,
                Datetime = Row.Datetime,
                Notes = Row.Notes,
                CreatedAt = Row.CreatedAt,
            };
            Debug.WriteLine(666);
            return Ok();
        }

        /// <summary>
        /// Delete a transaction from the system by the current user.
        /// </summary>
        /// <param name="id">The primary key of the transaction</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/Transaction/5
        /// FORM DATA: (empty)
        /// </example>
        [Route("api/Transaction/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            string userId = User.Identity.GetUserId();
            TransactionsModels Row = db.Transactions.Where(t => t.Id == id).Where(t => t.UserId == userId).FirstOrDefault();

            if (Row == null)
            {
                //return NotFound();
                return Content(HttpStatusCode.NotFound, "");
            }

            db.Transactions.Remove(Row);
            db.SaveChanges();

            return Ok();
        }
    }
}