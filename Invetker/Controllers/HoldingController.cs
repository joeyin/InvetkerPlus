using Invetker.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;

namespace Invetker.Controllers
{
    [Authorize]
    public class HoldingController : ApiController
    {
        TransactionController transactionController;
        AssetsController assetsController;

        private ApplicationDbContext db = new ApplicationDbContext();

        public HoldingController()
        {
            transactionController = new TransactionController();
            assetsController = new AssetsController();
        }

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
        public async Task<IHttpActionResult> List()
        {
            string userId = User.Identity.GetUserId();

            List<HoldingViewModel> Transactions = (transactionController.List() as OkNegotiatedContentResult<List<TransactionViewModel>>).Content
                .GroupBy(d => new { d.Symbol, d.AssetType, d.Action, d.AssetId })
                .Select(d => new HoldingViewModel {
                    Symbol = d.Key.Symbol,
                    AssetId = d.Key.AssetId,
                    AssetType = d.Key.AssetType,
                    Position = d.Sum(s => s.Action == ActionType.Sold ? s.Quantity * -1 : s.Quantity),
                    Amount = d.Sum(s => s.Action == ActionType.Sold ? ((s.Price * s.Quantity) + s.Fee) * -1 : (s.Price * s.Quantity) + s.Fee)
                })
                .ToList();

            List<HoldingViewModel> Holdings = Transactions
            .GroupBy(t => new { t.Symbol, t.AssetId, t.AssetType })
            .Select(g => new HoldingViewModel()
            {
                Symbol = g.Key.Symbol,
                AssetId = g.Key.AssetId,
                AssetType = g.Key.AssetType,
                Position = g.Sum(t => t.Position),
                Amount = g.Sum(t => t.Amount),
                Price = assetsController.GetLatestPrice((int)g.Key.AssetId)
            })
            .ToList();

            foreach (HoldingViewModel i in Holdings)
            {
                i.Price = assetsController.GetLatestPrice((int) i.AssetId);
                i.Change = assetsController.CalculatePercentageChange((int) i.AssetId);
                i.AvgPrice = i.Amount / i.Position;
                i.DailyPL = i.Position * assetsController.GetPriceChange((int)i.AssetId);
                i.MarketVal = i.Position * assetsController.GetLatestPrice((int)i.AssetId);
                i.UnrealizedPL = i.MarketVal - i.Amount;
            }

            return Ok(Holdings);
        }

        /// <summary>
        /// Returns top portfolio positions.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all positions in the database.
        /// </returns>
        /// <example>
        /// GET: api/Holding/Top
        /// </example>
        [HttpGet]
        [Route("api/Holding/Top")]
        [ResponseType(typeof(TopPositionViewModel))]
        public async Task<IHttpActionResult> Top()
        {
            var holdingControllerList = await (this.List() as Task<System.Web.Http.IHttpActionResult>);
            HoldingViewModel[] Holdings = (holdingControllerList as OkNegotiatedContentResult<List<HoldingViewModel>>).Content.ToArray();
            HoldingViewModel[] Sorted = Holdings.OrderByDescending(i => i.Symbol).ToArray();
            HoldingViewModel[] Portfolio = Sorted.Where(p => p.UnrealizedPL > 0).ToArray();

            List<TopPositionViewModel> TopPositions = new List<TopPositionViewModel>();

            int no = 1;
            foreach (HoldingViewModel i in Portfolio)
            {

                string Description = "";
                string Icon = "";

                if (i.AssetType == AssetType.Crypto)
                {
                    var result = db.Cryptocurrencies.Where(c => i.Symbol == c.Symbol).Select(s => new {
                        Description = s.Description, 
                        Logo = s.Logo, 
                    }).FirstOrDefault();
                    
                    Description = result.Description;
                    Icon = result.Logo;
                } else
                {
                    var result = db.Stocks.Where(c => i.Symbol == c.Symbol).Select(s => new {
                        Description = s.Description,
                        Logo = s.Logo,
                    }).FirstOrDefault();

                    Description = result.Description;
                    Icon = result.Logo;
                }

                TopPositions.Add(new TopPositionViewModel()
                {
                    No = no,
                    Position = i.Position,
                    Symbol = i.Symbol,
                    Price = i.Price,
                    DailyPL = i.DailyPL,
                    UnrealizedPL = i.UnrealizedPL,
                    Description = Description,
                    Icon = Icon
                });


                no++;
            }

            return Ok(TopPositions);
        }
    }
}