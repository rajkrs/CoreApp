using CoreApp.Account.Model;
using CoreApp.Account.ViewModel;
using AutoMapper;
using AutoMapper.Mappers;
using CoreApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Account.Provider
{
    public class UserProvider : RepositoryBase<User>, IUserProvider
    {
        private readonly IMapper _mapper;
        public UserProvider(AccountDbContext repositoryContext, IMapper mapper)
        : base(repositoryContext)
        {
            _mapper = mapper;
        }


        public async Task CreateUserAsync(UserInfo userInfo)
        {
            var user = _mapper.Map<User>(userInfo);
            Create(user);
            await SaveAsync();
        }

        public async Task UpdateUserAsync(UserInfo user)
        {
            Update(_mapper.Map<User>(user));
            await SaveAsync();
        }

        public async Task DeleteUserAsync(UserInfo userInfo)
        {
            var user = _mapper.Map<User>(userInfo);
            Delete(user);
            await SaveAsync();
        }

        public async Task<IEnumerable<UserInfo>> GetAllUsersAsync()
        {
            return _mapper.Map<IEnumerable<UserInfo>>(await FindAllAsync());

        }

        public async Task<UserInfo> GetUserByIdAsync(long userID)
        {
            var user = await FindByConditionAsync(o => o.ID.Equals(userID));
            return _mapper.Map<UserInfo>(user);

        }

        public Task<IEnumerable<UserDetails>> GetUserWithDetailsProcAsync(long userId)
        {
            return QueryAsync<UserDetails>("");
        }

        Task<UserInfo> IUserProvider.GetUserWithDetailsAsync(long userId)
        {
            throw new NotImplementedException();
        }
    }
}
