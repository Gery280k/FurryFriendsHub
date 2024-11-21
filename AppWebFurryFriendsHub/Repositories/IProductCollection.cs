using AppWebFurryFriendsHub.Shared;

namespace AppWebFurryFriendsHub.Repositories
{
	public interface IProductCollection
	{
		Task InsertProduct(Product product);
		Task UpdateProduct(Product product);
		Task DeleteProduct(string id);
		Task<List<Product>> GetAllProducts();


		Task<Product> GetProductById(string id);
		Task<List<Product>> GetProductsByCategory(string categoryId);

        Task<List<Product>> GetProductsByPriceRange(decimal minPrice, decimal maxPrice);
		Task<List<Product>> SearchProducts(string keyword);
		Task<List<Product>> GetFeaturedProducts();
    }
}
