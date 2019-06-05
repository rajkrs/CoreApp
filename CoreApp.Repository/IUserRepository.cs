using CoreApp.Account.Model;
using CoreApp.Account.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreApp.Account.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserInfo>> GetAllUsersAsync();
        Task<UserInfo> GetUserByIdAsync(long userID);
        Task<UserDetails> GetUserWithDetailsAsync(long userId);
        Task CreateUserAsync(UserInfo user);
        Task UpdateUserAsync(UserInfo user);
        Task DeleteUserAsync(UserInfo user);
    }
}
    