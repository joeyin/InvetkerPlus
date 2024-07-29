using Invetker.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace Invetker.Controllers
{
    [Authorize]
    public class StockController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all stocks in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all transactions in the database.
        /// </returns>
        /// <example>
        /// GET: api/Stock/list
        /// </example>
        [HttpGet]
        [ResponseType(typeof(StocksModels))]
        public IHttpActionResult List()
        {
            List<StocksModels> Stocks = db.Stocks.ToList();
            return Ok(Stocks);
        }

        // GET: StocksModelsController/Details/5
        /*
        public async Task<IHttpActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }
        */
    }
}
