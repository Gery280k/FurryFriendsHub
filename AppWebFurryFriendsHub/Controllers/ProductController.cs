using AppWebFurryFriendsHub.Repositories;
using AppWebFurryFriendsHub.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AppWebFurryFriendsHub.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : Controller
	{
		private IProductCollection db = new ProductCollection();

		[HttpGet]
		public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await db.GetAllProducts());
        }
        [HttpGet("search")]
		public async Task<ActionResult<List<Product>>> SearchProducts([FromQuery] string keyword)
        {
            var products = await db.SearchProducts(keyword);

            if (products == null || products.Count == 0)
            {
                return NotFound();  
            }

            return Ok(products);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<List<Product>>> GetProductsByCategory(string categoryId)
        {
            var products = await db.GetProductsByCategory(categoryId);
            if (products == null || products.Count == 0)
            {
                return NotFound();
            }
            return Ok(products);
        }

    }
}
