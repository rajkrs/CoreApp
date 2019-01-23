using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreApp.IpWhitelist;

namespace CoreApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoValuesController : ControllerBase
    {
        // GET: api/NoValues
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/NoValues/5
        [SkipIpWhitelistFilter]
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return $"value {id}";
        }

        // POST: api/NoValues
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/NoValues/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
