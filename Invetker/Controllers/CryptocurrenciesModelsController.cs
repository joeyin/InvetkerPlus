using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using Invetker.Models;

namespace Invetker.Controllers
{
    [Authorize]
    public class CryptocurrenciesModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CryptocurrenciesModelsController()
        {
            _context = ApplicationDbContext.Create();
        }

        // GET: CryptocurrenciesModels
        public async Task<ActionResult> Index()
        {
            var cryptocurrencies = await _context.Cryptocurrencies.ToListAsync();
            return View(cryptocurrencies);
        }

        // GET: CryptocurrenciesModels/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var cryptocurrency = await _context.Cryptocurrencies.FindAsync(id);
            if (cryptocurrency == null)
            {
                return HttpNotFound();
            }
            return View(cryptocurrency);
        }

        // GET: CryptocurrenciesModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CryptocurrenciesModels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CryptocurrenciesModels cryptocurrency)
        {
            if (ModelState.IsValid)
            {
                _context.Cryptocurrencies.Add(cryptocurrency);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(cryptocurrency);
        }

        // GET: CryptocurrenciesModels/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var cryptocurrency = await _context.Cryptocurrencies.FindAsync(id);
            if (cryptocurrency == null)
            {
                return HttpNotFound();
            }
            return View(cryptocurrency);
        }

        // POST: CryptocurrenciesModels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CryptocurrenciesModels cryptocurrency)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(cryptocurrency).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cryptocurrency);
        }

        // GET: CryptocurrenciesModels/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var cryptocurrency = await _context.Cryptocurrencies.FindAsync(id);
            if (cryptocurrency == null)
            {
                return HttpNotFound();
            }
            return View(cryptocurrency);
        }

        // POST: CryptocurrenciesModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var cryptocurrency = await _context.Cryptocurrencies.FindAsync(id);
            _context.Cryptocurrencies.Remove(cryptocurrency);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
