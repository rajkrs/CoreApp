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
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly IUnitOfWork<AccountDbContext> _uow;

        private readonly IMapper _mapper;
        public UserRepository(IUnitOfWork<AccountDbContext> unitOfWork, IMapper mapper, AuthorizeProfile authorizeProfile) 
            : base(authorizeProfile)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }


        public async Task CreateUserAsync(UserInfo userInfo)
        {
            var user = _mapper.Map<User>(userInfo);
            _uow.GetRepository<User>().Add(user);
            await _uow.SaveChangesAsync();
            
        }

        public async Task UpdateUserAsync(UserInfo user)
        {
            _uow.GetRepository<User>().Update(_mapper.Map<User>(user));
            await _uow.SaveChangesAsync();
 
        }

        public async Task DeleteUserAsync(UserInfo userInfo)
        {
            _uow.GetRepository<User>().Delete(_mapper.Map<User>(userInfo));
            await _uow.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserInfo>> GetAllUsersAsync()
        {
            return _mapper.Map<IEnumerable<UserInfo>>(await _uow.GetRepository<User>().GetAllAsync());

        }

        public async Task<UserInfo> GetUserByIdAsync(long userID)
        {
            var user = await _uow.GetRepository<User>().GetAll(o => o.ID.Equals(userID)).ToListAsync();
            return _mapper.Map<UserInfo>(user);
                
        }

        public async Task<UserDetails> GetUserWithDetailsAsync(long userId)
        {
            var details = await _uow.GetRepository<UserDetails>().QueryAsync("21@!@!@", OrgnizationId);
            return details.FirstOrDefault();
        }
    }
}
