using Invetker.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Http.Results;

namespace Invetker.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        TransactionController transactionController;

        public DashboardController()
        {
            transactionController = new TransactionController();
        }

        // GET: Dashboard
        public ActionResult Index()
        {
            ViewBag.SliderCollapsed = Request.Cookies["slider-collapsed"]?.Value;

            ViewData["Transactions"] = (transactionController.List() as OkNegotiatedContentResult<List<Transaction>>).Content;

            return View();
        }

        // GET: Dashboard/Transactions
        public ActionResult Transactions()
        {
            ViewBag.SliderCollapsed = Request.Cookies["slider-collapsed"]?.Value;

            ViewData["Transactions"] = (transactionController.List() as OkNegotiatedContentResult<List<Transaction>>).Content;

            return View();
        }
    }
}
