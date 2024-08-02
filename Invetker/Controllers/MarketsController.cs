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
            ViewData["TopCryptocurrencies"] = await GetTopCryptocurrenciesData();
            ViewData["MostActiveStocks"] = await GetMostActiveStocksData();
            ViewData["TopGainerStocks"] = await GetTopGainerStocksData();
            ViewData["TopVolumeStocks"] = await GetTopVolumeStocksData();
            ViewData["MostActiveCryptos"] = await GetMostActiveCryptos();
            ViewData["TopGainerCryptos"] = await GetTopGainerCryptos();
            ViewData["TopVolumeCryptos"] = await GetTopVolumeCryptos();
            return View();
        }

        private async Task<IEnumerable<AssetActivityDto>> GetTopCryptocurrenciesData()
        {
            HttpResponseMessage response = await client.GetAsync("api/Assets/GetTopCryptoByMarketCap");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<AssetActivityDto>>();
            }
            return new List<AssetActivityDto>();
        }

        private async Task<IEnumerable<AssetActivityDto>> GetMostActiveStocksData()
        {
            HttpResponseMessage response = await client.GetAsync("api/Assets/GetMostActiveStocks");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<AssetActivityDto>>();
            }
            return new List<AssetActivityDto>();
        }

        private async Task<IEnumerable<AssetActivityDto>> GetTopGainerStocksData()
        {
            HttpResponseMessage response = await client.GetAsync("api/Assets/GetTopGainerStocks");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<AssetActivityDto>>();
            }
            return new List<AssetActivityDto>();
        }

        private async Task<IEnumerable<AssetActivityDto>> GetTopVolumeStocksData()
        {
            HttpResponseMessage response = await client.GetAsync("api/Assets/GetTopVolumeStocks");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<AssetActivityDto>>();
            }
            return new List<AssetActivityDto>();
        }


        private async Task<IEnumerable<AssetActivityDto>> GetMostActiveCryptos()
        {
            HttpResponseMessage response = await client.GetAsync("api/Assets/GetMostActiveCryptos");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<AssetActivityDto>>();
            }
            return new List<AssetActivityDto>();
        }

        private async Task<IEnumerable<AssetActivityDto>> GetTopGainerCryptos()
        {
            HttpResponseMessage response = await client.GetAsync("api/Assets/GetTopGainerCryptos");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<AssetActivityDto>>();
            }
            return new List<AssetActivityDto>();
        }

        private async Task<IEnumerable<AssetActivityDto>> GetTopVolumeCryptos()
        {
            HttpResponseMessage response = await client.GetAsync("api/Assets/GetTopVolumeCryptos");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<AssetActivityDto>>();
            }
            return new List<AssetActivityDto>();
        }
    }
}
