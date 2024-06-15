using Invetker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Security;

namespace Invetker.Controllers
{
    public class TransactionController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all transactions in the system by the current user.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all transactions in the database.
        /// </returns>
        /// <example>
        /// GET: api/transactions
        /// </example>
        [HttpGet]
        [ResponseType(typeof(Transaction))]
        public IHttpActionResult Index()
        {
            string userId = User.Identity.GetUserId();

            List<Transaction> Transactions = db.Transactions.Where(t => t.UserId == userId).OrderByDescending(t => t.Datetime).ToList();
            List<Transaction> TransactionDtos = new List<Transaction>();

            Transactions.ForEach(t => TransactionDtos.Add(new Transaction()
            {
                Ticker = t.Ticker,
                Quantity = t.Quantity,
                Action = t.Action,
                Price = t.Price,
                Fee = t.Fee,
                Datetime = t.Datetime,
                Notes = t.Notes,
                CreatedAt = t.CreatedAt,
            }));

            return Ok(TransactionDtos);
        }

        /// <summary>
        /// Adds a transaction to the system
        /// </summary>
        /// <param name="transaction">JSON FORM DATA of an transaction</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Transaction ID, Transaction Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/Transaction/Add
        /// FORM DATA: Transaction JSON Object
        /// </example>
        [ResponseType(typeof(Transaction))]
        [HttpPost]
        public IHttpActionResult Add(TransactionAddViewModel transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Transaction newTransaction = new Transaction();
            newTransaction.UserId = User.Identity.GetUserId();
            newTransaction.Ticker = transaction.Ticker;
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
    }
}