using System;
using BlazorServerApp.Models;
using MongoDB.Driver;

namespace BlazorServerApp.Services;

public class UserService
{
    public readonly IMongoCollection<User> _users; 

    UserService(MongoClient client)
    {
        var database = client.GetDatabase("FurryFriendsHub");

        _users = database.GetCollection<User>("Users");

    }

    public async Task<User> AutenticateUser(String email, String passwordHash)
    {
       return await  _users.Find(u => u.Email == email && u.PasswordHash == passwordHash).FirstOrDefaultAsync();
  
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _users.Find(User => true).ToListAsync();
    }

    public async Task InsertUsuario(User newUser)
    {
       await _users.InsertOneAsync(newUser);
    }



}
