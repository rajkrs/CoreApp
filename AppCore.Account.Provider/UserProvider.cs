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
using CoreApp.Account.Repository;

namespace CoreApp.Account.Provider
{
    public class UserProvider : IUserProvider
    {
        private readonly IUserRepository _userRepository;
        public UserProvider(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public Task CreateUserAsync(UserInfo userInfo)
            => _userRepository.CreateUserAsync(userInfo);

        public Task UpdateUserAsync(UserInfo user)
                    => _userRepository.UpdateUserAsync(user);


        public  Task DeleteUserAsync(UserInfo userInfo)
                    => _userRepository.DeleteUserAsync(userInfo);


        public  Task<IEnumerable<UserInfo>> GetAllUsersAsync()
                   => _userRepository.GetAllUsersAsync();


        public  Task<UserInfo> GetUserByIdAsync(long userID)
                   => _userRepository.GetUserByIdAsync(userID);


        public Task<UserDetails> GetUserWithDetailsAsync(long userId)
                    => _userRepository.GetUserWithDetailsAsync(userId);

        
    }
}
