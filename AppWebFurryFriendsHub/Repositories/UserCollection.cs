using MongoDB.Bson;
using MongoDB.Driver;
using AppWebFurryFriendsHub.Shared;

namespace AppWebFurryFriendsHub.Repositories
{
    public class UserCollection : IUserCollection
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private IMongoCollection<User> _users;

        public UserCollection()
        {
            _users = _repository.db.GetCollection<User>("Users");
        }

        public async Task<User> AuthenticateUser(UserAuthDTO userAuthDTO)
        {
            var user = await _users.FindAsync(u => u.email == userAuthDTO.Email && u.passwordHash == userAuthDTO.passwordHash).Result.FirstOrDefaultAsync();
            return user;
        }

        public async Task DeleteUser(string id)
        {
            var filter = Builders<User>.Filter.Eq(s => s.Id, id);
            await _users.DeleteOneAsync(filter);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _users.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<User> GetUserById(string id)
        {
            return await _users.FindAsync(new BsonDocument { { "_id", id } }).Result.FirstAsync();
        }

        public async Task InsertUser(User user)
        {
            user.createdAt = DateTime.Now;
            await _users.InsertOneAsync(user);
        }

        public async Task UpdateUser(User user)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, user.Id);
            await _users.ReplaceOneAsync(filter, user);
        }
    }
}
