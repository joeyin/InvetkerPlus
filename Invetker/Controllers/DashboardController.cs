using Invetker.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Http.Results;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Invetker.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        TransactionController transactionController;
        HoldingController holdingController;

        public DashboardController()
        {
            transactionController = new TransactionController();
            holdingController = new HoldingController();
        }

        // GET: Dashboard
        public async Task<ActionResult> Index()
        {
            ViewBag.SliderCollapsed = Request.Cookies["slider-collapsed"]?.Value;

            ViewData["Transactions"] = (transactionController.List() as OkNegotiatedContentResult<List<TransactionsModels>>).Content;

            var holdingControllerList = await (holdingController.List() as Task<System.Web.Http.IHttpActionResult>);
            ViewData["Holdings"] = (holdingControllerList as OkNegotiatedContentResult<List<HoldingViewModel>>).Content;

            var holdingControllerTop = await (holdingController.Top() as Task<System.Web.Http.IHttpActionResult>);
            ViewData["TopHoldings"] = (holdingControllerTop as OkNegotiatedContentResult<List<TopPositionViewModel>>).Content;

            return View();
        }

        // GET: Dashboard/Transactions
        public ActionResult Transactions()
        {
            ViewBag.SliderCollapsed = Request.Cookies["slider-collapsed"]?.Value;

            ViewData["Transactions"] = (transactionController.List() as OkNegotiatedContentResult<List<TransactionsModels>>).Content;

            return View();
        }
    }
}
