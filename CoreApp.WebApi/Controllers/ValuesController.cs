using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.Account.Model;
using CoreApp.IpWhitelist;
using Microsoft.AspNetCore.Mvc;

namespace CoreApp.WebApi.Controllers
{

    //[ServiceFilter(typeof(IpWhitelist.IpWhitelistFilter))]
    [SkipIpWhitelistFilter]
    [Route("api/[controller]")]
    [ApiExplorerSettings(IgnoreApi = false, GroupName = nameof(ValuesController))]

    public class ValuesController : BaseApiController
    {
        private readonly AuthorizeProfile _authorizeProfile;
        public ValuesController(AuthorizeProfile authorizeProfile)
        {
            _authorizeProfile = authorizeProfile;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2", UserId.ToString() , _authorizeProfile.UserId.ToString() };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
