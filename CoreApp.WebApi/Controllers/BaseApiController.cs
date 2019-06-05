using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.Account.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreApp.WebApi.Controllers
{
   
    [ApiController]
    //Add Authorize here
    public class BaseApiController : ControllerBase
    {

        public int UserId = -1;
        public int OrgnizationId = -1;

        public BaseApiController()
        {
            var claims = HttpContextProvider.Current.User.Claims;
            var userIdData = claims.FirstOrDefault(c => c.Type == "UserId");
            var userOrgnizarionData = claims.FirstOrDefault(c => c.Type == "Organization");


           

            int.TryParse(userIdData?.Value, out UserId);
            int.TryParse(userOrgnizarionData?.Value, out OrgnizationId);

             

        }
    }
}