using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Invetker.Models;

namespace Invetker.Controllers
{
    public class StocksDataController : Controller
    {
        private static readonly HttpClient client = new HttpClient();

        static StocksDataController()
        {
            client.BaseAddress = new System.Uri("https://localhost:44337/");  // API base URL
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ActionResult> GetStocksData()
        {
            HttpResponseMessage response = await client.GetAsync("api/Stock/List");
            if (response.IsSuccessStatusCode)
            {
                var stocks = await response.Content.ReadAsAsync<List<StocksModels>>();
                return View(stocks);  // Assume this returns a view that displays stocks
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return Content("Error: " + errorContent);
            }
        }
    }
}
