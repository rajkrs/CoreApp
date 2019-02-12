using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace CoreApp.ApiCache
{
    public class OutputCachingFilter : ActionFilterAttribute
    {
        public int Minutes { get; set; } = 15;
        public CacheTypes CacheType { get; set; } = CacheTypes.Sliding;
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            if (context.Filters.OfType<SkipOutputCachingFilter>().Any()) return;

            var cacheProvider = context.HttpContext.RequestServices.GetService(typeof(IDistributedCache)) as IDistributedCache;
            var cacheKeyProvider = context.HttpContext.RequestServices.GetService(typeof(ICacheKeyProvider)) as ICacheKeyProvider;


            var key = cacheKeyProvider.CreateKey();

            var cacheData = cacheProvider.Get(key);
            if (cacheData != null)
            {
                var cacheItem = cacheKeyProvider.FromByteArray<CacheItem>(cacheData);
                foreach (var header in cacheItem.Headers)
                {
                    context.HttpContext.Response.Headers.Add(header.Key, header.Value);
                }
                context.HttpContext.Response.Headers.Add("Cache-Item", "true");
                context.HttpContext.Response.Headers.Add("Cache-On", cacheItem.LastModifiedOn.ToString("o"));
                context.HttpContext.Response.Body.WriteAsync(cacheItem.Body, 0, cacheItem.Body.Length);
                context.Result = new EmptyResult();
                
            }
            else
            {
                //remove direct incoming header from end user;
                context.HttpContext.Request.Headers.Add("Cache-Enabled", "true");
                context.HttpContext.Request.Headers.Add("Cache-Time-Minutes", Minutes.ToString());
                context.HttpContext.Request.Headers.Add("Cache-Type", CacheType.ToString());
            }

            base.OnActionExecuting(context);
            

        }

    }
   
}
