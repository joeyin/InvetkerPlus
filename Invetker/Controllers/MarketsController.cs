using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Invetker.Models;

namespace Invetker.Controllers
{
    [Authorize]
    public class MarketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MarketsController()
        {
            _context = ApplicationDbContext.Create();
        }

        // GET: Markets
        public async Task<ActionResult> Index()
        {
            var cryptocurrencies = await _context.Cryptocurrencies
                .Select(c => new CryptocurrenciesModelsDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Symbol = c.Symbol,
                    Description = c.Description,
                    MarketCap = c.MarketCap,
                    CirculatingSupply = c.CirculatingSupply,
                    MaxSupply = c.MaxSupply,
                    CreatedAt = c.CreatedAt
                })
                .ToListAsync();

            // Debugging: Print the retrieved cryptocurrencies
            Debug.WriteLine("Cryptocurrencies:");
            foreach (var crypto in cryptocurrencies)
            {
                Debug.WriteLine($"ID: {crypto.Id}, Name: {crypto.Name}, Symbol: {crypto.Symbol}");
            }

            ViewData["CryptocurrenciesModels"] = cryptocurrencies;

            return View();
        }
    }
}
