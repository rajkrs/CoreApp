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
using Microsoft.EntityFrameworkCore;

namespace CoreApp.Account.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly IMapper _mapper;
        public UserRepository(AccountDbContext repositoryContext, IMapper mapper)
        : base(repositoryContext)
        {
            _mapper = mapper;
        }


        public async Task CreateUserAsync(UserInfo userInfo)
        {
            var user = _mapper.Map<User>(userInfo);
            Add(user);
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
            return _mapper.Map<IEnumerable<UserInfo>>(await GetAllAsync());

        }

        public async Task<UserInfo> GetUserByIdAsync(long userID)
        {
            var user = await GetAll(o => o.ID.Equals(userID)).ToListAsync();
            return _mapper.Map<UserInfo>(user);
                
        }

        public async Task<UserDetails> GetUserWithDetailsAsync(long userId)
        {
            return (await QueryAsync<UserDetails>("")).FirstOrDefault();
        }
    }
}
