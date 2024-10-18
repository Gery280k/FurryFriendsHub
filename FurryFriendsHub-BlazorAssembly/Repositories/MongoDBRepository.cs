
using MongoDB.Driver;

namespace FurryFriendsHub_BlazorAssembly.Repositories
{
    public class MongoDBRepository
    {
        public MongoClient Client = new MongoClient();
        public IMongoDatabase db;

        public MongoDBRepository() 
        { 
            Client = new MongoClient("mongodb://localhost:27017/");
            db = Client.GetDatabase("FurryFriendsHub");
        }
    }
}
