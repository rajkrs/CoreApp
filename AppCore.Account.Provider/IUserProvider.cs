using CoreApp.Account.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreApp.Account.Provider
{
    public interface IUserProvider
    {
        Task<IEnumerable<UserInfo>> GetAllUsersAsync();
        Task<UserInfo> GetUserByIdAsync(long userID);
        Task<UserInfo> GetUserWithDetailsAsync(long userId);
        Task CreateUserAsync(UserInfo user);
        Task UpdateUserAsync(UserInfo user);
        Task DeleteUserAsync(UserInfo user);
    }
}
    