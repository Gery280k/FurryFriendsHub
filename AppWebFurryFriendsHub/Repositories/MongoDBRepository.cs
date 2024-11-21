using MongoDB.Driver;

namespace AppWebFurryFriendsHub.Repositories
{
    public class MongoDBRepository
    {
        public MongoClient Client = new MongoClient();
        public IMongoDatabase db;

        public MongoDBRepository()
        {
            Client = new MongoClient("mongodb+srv://admin:admin@unitech.6n11b.mongodb.net/?retryWrites=true&w=majority&appName=Unitech");
            db = Client.GetDatabase("FurryFriendsHub");

            //actualizacion a nube
        }
    }
}
