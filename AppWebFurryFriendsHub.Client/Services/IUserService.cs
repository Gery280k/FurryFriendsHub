using AppWebFurryFriendsHub.Shared;

namespace AppWebFurryFriendsHub.Client.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserDetail(string id);
        Task SaveUser(User user);
        Task DeleteUser(string id);
        Task<User> AuthenticateUser(UserAuthDTO userAuthDTO);
    }
}
