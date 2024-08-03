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
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using System.Web.Security;
using YahooFinanceApi;
using static Invetker.Models.PolygonIO;

namespace Invetker.Controllers
{
    [Authorize]
    public class HoldingController : ApiController
    {
        TransactionController transactionController;
        AssetsController assetsController;

        private ApplicationDbContext db = new ApplicationDbContext();
        private const string URL = "https://api.polygon.io/v3/reference/tickers/";
        private const string API_KEY = "wnoNdymQNypqwkKazeJLbnZRUMS27Fzp";

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
                    Position = d.Sum(s => s.Action == ActionType.Sold ? s.Quantity * -1 : s.Quantity),
                    Amount = d.Sum(s => s.Action == ActionType.Sold ? ((s.Price * s.Quantity) + s.Fee) * -1 : (s.Price * s.Quantity) + s.Fee)
                })
                .ToList();

            List<HoldingViewModel> Holdings = Transactions
            .GroupBy(t => new { t.Symbol, t.AssetId })
            .Select(g => new HoldingViewModel()
            {
                Symbol = g.Key.Symbol,
                AssetId = g.Key.AssetId,
                Position = g.Sum(t => t.Position),
                Amount = g.Sum(t => t.Amount)
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
            //HoldingViewModel[] Portfolio = Sorted.Where(i => i.UnrealizedPL > 0).ToArray();
            HoldingViewModel[] Portfolio = Sorted.ToArray();

            List<TopPositionViewModel> TopPositions = new List<TopPositionViewModel>();

            int no = 1;
            foreach (HoldingViewModel i in Portfolio)
            {
                // Get data from polygon.io
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URL);

                HttpResponseMessage response = client.GetAsync(i.Symbol + "?apiKey=" + API_KEY).Result;
                var json = await response.Content.ReadAsStringAsync();
                var data = JObject.Parse(json);

                string Description = "";
                string Icon = "";

                // Only 5 api calls per minute, if more then 5 will get error
                if (data["results"] != null)
                {
                    Description = (string)JObject.Parse(json)["results"]["description"];
                    Icon = (string)JObject.Parse(json)["results"]["branding"]["logo_url"] + "?apiKey=" + API_KEY;
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