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
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Security;

namespace Invetker.Controllers
{
    [Authorize]
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
        /// GET: api/Transaction/list
        /// </example>
        [HttpGet]
        [ResponseType(typeof(TransactionsModels))]
        public IHttpActionResult List()
        {
            string userId = User.Identity.GetUserId();

            List<TransactionsModels> Transactions = db.Transactions.Where(t => t.UserId == userId).OrderByDescending(t => t.Datetime).ToList();
            List<TransactionsModels> TransactionDtos = new List<TransactionsModels>();

            Transactions.ForEach(t => TransactionDtos.Add(new TransactionsModels()
            {
                Id = t.Id,
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

            Row.Ticker = transaction.Ticker;
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

            if (Row == null)
            {
                //return NotFound();
                return Content(HttpStatusCode.NotFound, "");
            }

            Transaction = new TransactionsModels
            {
                Ticker = Row.Ticker,
                Quantity = Row.Quantity,
                Action = Row.Action,
                Price = Row.Price,
                Fee = Row.Fee,
                Datetime = Row.Datetime,
                Notes = Row.Notes,
                CreatedAt = Row.CreatedAt,
            };
            
            return Ok(Transaction);
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