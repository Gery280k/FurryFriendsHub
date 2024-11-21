using AppWebFurryFriendsHub.Shared;
using MongoDB.Bson;
using MongoDB.Driver;
using ZstdSharp.Unsafe;

namespace AppWebFurryFriendsHub.Repositories
{
	public class ProductCollection : IProductCollection
	{
		internal MongoDBRepository _repository = new MongoDBRepository();
		private IMongoCollection<Product> _products;

		public ProductCollection() 
		{
			_products = _repository.db.GetCollection<Product>("Products");
		}
		public async Task DeleteProduct(string id)
		{
			var filter = Builders<Product>.Filter.Eq(s => s.Id, id);
			await _products.DeleteOneAsync(filter);
		}

		public async Task<List<Product>> GetAllProducts()
		{
			return await _products.FindAsync(new BsonDocument()).Result.ToListAsync();
		}

		public async Task<List<Product>> GetFeaturedProducts()
		{
			throw new NotImplementedException();
		}



		public async Task<Product> GetProductById(string id)
		{
			return await _products.FindAsync(new BsonDocument { { "_id", id } }).Result.FirstAsync();
		}

        public async Task<List<Product>> GetProductsByCategory(string categoryId)
        {
 
            if (string.IsNullOrEmpty(categoryId))
            {
                return new List<Product>(); // Devuelve una lista vacía si no se proporciona categoryId
            }

            var filter = Builders<Product>.Filter.Eq(p => p.categoryId, categoryId);
            var products = await _products.Find(filter).ToListAsync();

            return products;
        }


        public Task<List<Product>> GetProductsByPriceRange(decimal minPrice, decimal maxPrice)
		{
			throw new NotImplementedException();
		}

		public async Task InsertProduct(Product product)
		{
			product.createdAt = DateTime.Now;
			await _products.InsertOneAsync(product);
		}

		public async Task<List<Product>> SearchProducts(string keyword)
		{
            if (string.IsNullOrWhiteSpace(keyword))
            {
               return new List<Product>();
            }

			var filter = Builders<Product>.Filter.Regex("name", new BsonRegularExpression(keyword, "i"));
			return await _products.Find(filter).ToListAsync();
        }

		public async Task UpdateProduct(Product product)
		{
			var filter = Builders<Product>.Filter.Eq(p => p.Id, product.Id);
			await _products.ReplaceOneAsync(filter, product);

		}
	}
}
