using Invetker.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;


namespace Invetker.Controllers
{

    public class AssetsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET api/Assets/GetAssetIdByCrypto/cryptoId
        [HttpGet]
        [Route("api/Assets/GetAssetIdByCrypto/{cryptoId}")]
        public IHttpActionResult GetAssetIdByCrypto(int cryptoId)
        {
            try
            {
                var asset = db.Assets.FirstOrDefault(a => a.Type == AssetType.Crypto && a.SymbolId == cryptoId);

                if (asset == null)
                    return NotFound();

                return Ok(asset.AssetId);
            }
            catch (Exception ex)
            {
                // Log exception details here
                return InternalServerError(ex);
            }
        }

        // GET api/Assets/GetAssetIdByStock/stockId
        [HttpGet]
        [Route("api/Assets/GetAssetIdByStock/{stockId}")]
        public IHttpActionResult GetAssetIdByStock(int stockId)
        {
            try
            {
                var asset = db.Assets.FirstOrDefault(a => a.Type == AssetType.Stock && a.SymbolId == stockId);
                if (asset == null)
                    return NotFound();  // Returns a 404 Not Found if no asset matches.

                // Logging the asset details to the console before returning the response
                Debug.WriteLine($"Asset Found: ID = {asset.AssetId}, Type = {asset.Type}, SymbolID = {asset.SymbolId}");

                return Ok(asset.AssetId);  // Returns the AssetId if found.
            }
            catch (Exception ex)
            {
                // Log exception details here using console or a more sophisticated logging approach
                Console.WriteLine($"Error retrieving asset: {ex.Message}");
                return InternalServerError(ex);  // Returns a 500 Internal Server Error on exception.
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}