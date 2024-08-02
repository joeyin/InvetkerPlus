using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Invetker.Models;

namespace Invetker.Controllers
{
    public class MarketsController : Controller
    {
        private HttpClient client = new HttpClient();

        public MarketsController()
        {
            client.BaseAddress = new System.Uri("https://localhost:44337/"); // API base URL
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ActionResult> Index()
        {
            ViewData["StocksModels"] = await GetStocksData();
            ViewData["CryptocurrenciesModels"] = await GetCryptocurrenciesData(); // Ensure you have a similar API setup for cryptocurrencies
            return View();
        }

        private async Task<IEnumerable<StocksModels>> GetStocksData()
        {
            HttpResponseMessage response = await client.GetAsync("api/Stock/List");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<StocksModels>>();
            }
            return new List<StocksModels>();
        }

        private async Task<IEnumerable<CryptocurrenciesModels>> GetCryptocurrenciesData()
        {
            HttpResponseMessage response = await client.GetAsync("api/Cryptocurrency/List");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<CryptocurrenciesModels>>();
            }
            return new List<CryptocurrenciesModels>();
        }
    }
}
