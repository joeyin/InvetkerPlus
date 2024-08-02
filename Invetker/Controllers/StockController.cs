using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Invetker.Models;

namespace Invetker.Controllers
{
    [RoutePrefix("api/Stock")]
    public class StockController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Returns all stocks in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all stocks in the database.
        /// </returns>
        /// <example>
        /// GET: api/Stock/List
        /// </example>
        [HttpGet]
        [Route("List")]
        public IHttpActionResult List()
        {
            var stocks = db.Stocks.ToList();
            return Ok(stocks);  // This will serialize the list of stocks to JSON
        }
    }
}

