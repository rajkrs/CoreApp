using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreApp.WebApi.Controllers
{
   
    [ApiController]
    //Add Authorize here
    public class BaseApiController : ControllerBase
    {
        public int UserId = 0;
        public BaseApiController()
        {
            var claims = HttpContextProvider.Current.User.Claims;
            var userData = claims.FirstOrDefault(c => c.Type == "UserID");

            int.TryParse(userData?.Value, out UserId);
            
        }
    }
}