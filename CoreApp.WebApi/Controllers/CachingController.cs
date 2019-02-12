using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.ApiCache;
using CoreApp.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [OutputCachingFilter]
    public class CachingController : ControllerBase
    {
        // GET: api/Caching
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [SkipOutputCachingFilter]
        // GET: api/Caching/5
        [HttpGet("{id}-{name}")]
        public string Get(int id,string name)
        {
            return "value";
        }

        // POST: api/Caching
        [HttpPost]
        public ActionResult Post([FromBody]UserLogin userLogin)
        {
            System.Threading.Thread.Sleep(10000);
            return Ok(userLogin);
        }

        // PUT: api/Caching/5
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
