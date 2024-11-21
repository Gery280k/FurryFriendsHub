using AppWebFurryFriendsHub.Shared;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace AppWebFurryFriendsHub.Client.Services
{
	public class productService : IProductService
	{
		private readonly HttpClient _httpClient;
		public productService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public Task DeleteProduct(string id)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Product>> GetAllProducts()
		{
			return await JsonSerializer.DeserializeAsync<IEnumerable<Product>>
			(await _httpClient.GetStreamAsync($"api/product"),
			new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

		}

        public async Task<List<Product>> GetProductByCategory(int categoryId)
        {
            return await _httpClient.GetFromJsonAsync<List<Product>>($"api/product/category/{categoryId}");
        }

        public Task<User> GetProductDetail(string id)
		{
			throw new NotImplementedException();
		}

		public Task SaveProduct(Product product)
		{
			throw new NotImplementedException();
		}

        public async Task<List<Product>> SearchProducts(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return new List<Product>();
            }

            try
            {
                // Llama a la API y convierte el JSON en una lista de productos
                var products = await _httpClient.GetFromJsonAsync<List<Product>>($"/api/product/search?keyword={searchTerm}");
                return products ?? new List<Product>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error al buscar productos: {ex.Message}");
                return new List<Product>();
            }
        }
    }
}
