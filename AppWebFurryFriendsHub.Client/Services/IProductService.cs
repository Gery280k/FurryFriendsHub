using AppWebFurryFriendsHub.Shared;
namespace AppWebFurryFriendsHub.Client.Services
{
	public interface IProductService
	{
		Task<IEnumerable<Product>> GetAllProducts();
		Task<User> GetProductDetail(string id);
		Task SaveProduct(Product product);
		Task DeleteProduct(string id);
		Task<List<Product>> SearchProducts(string searchTerm);
		Task<List<Product>> GetProductByCategory(int categoryId);

    }
}
