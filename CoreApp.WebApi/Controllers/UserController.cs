using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.Account.Provider;
using CoreApp.Account.ViewModel;
using CoreApp.IpWhitelist;
using CoreApp.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreApp.WebApi.Controllers
{
    [SkipIpWhitelistFilter]
    [Route("api/user")]
    [ApiController]

    public class UserController : ControllerBase
    {

        IUserProvider _userProvider;
        public UserController(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }


        [HttpGet("all")]
        public async Task<IEnumerable<UserInfo>> GetUserInfo()
        {
            return await _userProvider.GetAllUsersAsync();
        }

 

    }
}