using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AppWebFurryFriendsHub.Shared
{
	public class Product
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; } 

		public string name { get; set; }
		public string description { get; set; }
		public string categoryId { get; set; } 
		public decimal price { get; set; }
		public string imageUrl { get; set; }
		public bool isNew { get; set; }
		public bool isFeatured { get; set; }

		public DateTime createdAt { get; set; }
		public DateTime updatedAt { get; set; }

		public int stock { get; set; } 
	}
}
