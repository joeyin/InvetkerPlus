using Invetker.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
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
    public class HoldingController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all current stock holding in the system by the current user.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all positions in the database.
        /// </returns>
        /// <example>
        /// GET: api/Holding/list
        /// </example>
        [HttpGet]
        [ResponseType(typeof(HoldingViewModel))]
        public IHttpActionResult List()
        {
            string userId = User.Identity.GetUserId();

            List<HoldingViewModel> Transactions = db.Transactions.GroupBy(d => new { d.Ticker, d.Action })
            .Select(
                g => new HoldingViewModel()
                {
                    Ticker = g.Key.Ticker,
                    Position = g.Sum(s => s.Action == ActionType.Sold ? s.Quantity * -1 : s.Quantity),
                    Amount = g.Sum(s => s.Action == ActionType.Sold ? ((s.Price * s.Quantity) + s.Fee) * -1 : (s.Price * s.Quantity) + s.Fee)
                }
            ).ToList();

            List<HoldingViewModel> Holdings = Transactions
            .GroupBy(t => t.Ticker)
            .Select(g => new HoldingViewModel()
            {
                Ticker = g.Key,
                Position = g.Sum(t => t.Position),
                Amount = g.Sum(t => t.Amount)
            }).ToList();

            foreach(HoldingViewModel i in Holdings)
            {
                i.AvgPrice = i.Amount / i.Position;
            }

            Debug.WriteLine(Holdings[0].AvgPrice);

            return Ok(Holdings);
        }
    }
}