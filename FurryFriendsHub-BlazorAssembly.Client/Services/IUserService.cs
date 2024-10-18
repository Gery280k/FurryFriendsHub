using FurryFriendsHub_BlazorAssembly.Shared;

namespace FurryFriendsHub_BlazorAssembly.Client.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserDetail(string id);
        Task SaveUser(User user);
        Task DeleteUser(string id);
        Task <User>AuthenticateUser(UserAuthDTO userAuthDTO);
    }
}
