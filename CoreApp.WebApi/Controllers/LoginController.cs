using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.IpWhitelist;
using CoreApp.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SkipIpWhitelistFilter]
    public class LoginController : ControllerBase
    {
        [HttpGet]
        public ActionResult<bool> Get([FromQuery]UserLogin userLogin)
        {
            return (userLogin.UserId == 1 && userLogin.Password == "1" );
        }

        [HttpGet("userinfo")]
        public ActionResult GetUserInfo(long userId)
        {
            return null;
        }


        [Produces("application/x-protobuf")]
        [HttpPost]
        public ActionResult<bool> Post([FromBody]UserLogin userLogin)
        {
            return (userLogin.UserId == 1 && userLogin.Password == "1");
        }

    }
}