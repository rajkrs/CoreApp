using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.ApiCache
{
    public class ICacheMiddleware
    {
        #region Private Fields


        private readonly RequestDelegate _next;

        #endregion Private Fields

        #region Public Constructors

        public ICacheMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        #endregion Public Constructors



        #region Public Methods

        public async Task Invoke(HttpContext context)
        {

            //remove header set by end user directly.
            context.Request.Headers.Remove("Cache-Enabled");

            var originalBodyStream = context.Response.Body;

            using (var memStream = new MemoryStream())
            {
                context.Response.Body = memStream;
                await _next(context);

                memStream.Position = 0;

                var cacheEnabled = context.Request.Headers["Cache-Enabled"];
                if (cacheEnabled == "true")
                {
                    int minutes = 0;
                    int.TryParse(context.Request.Headers["Cache-Time-Minutes"], out minutes);
                    CacheTypes cacheType = (CacheTypes)Enum.Parse(typeof(CacheTypes), context.Request.Headers["Cache-Type"]);

                    var cacheProvider = context.RequestServices.GetService(typeof(IDistributedCache)) as IDistributedCache;
                    var cacheKeyProvider = context.RequestServices.GetService(typeof(ICacheKeyProvider)) as ICacheKeyProvider;

                    var key = cacheKeyProvider.CreateKey();
                    var cacheData = cacheProvider.Get(key);

                    if (cacheData == null)
                    {

                        var response = context.Response;
                        string responseBody = new StreamReader(memStream).ReadToEnd();
                        memStream.Position = 0;

                        var cacheItem = new CacheItem
                        {
                            ContentType = context.Response.ContentType,
                            Body = System.Text.ASCIIEncoding.ASCII.GetBytes(responseBody),
                            Headers = context.Response.Headers.ToDictionary(h => h.Key, v => v.Value.ToString()),
                            LastModifiedOn = DateTime.Now
                        };

                        switch (cacheType)
                        {
                            case CacheTypes.Absolute:
                                cacheProvider.Set(key, cacheKeyProvider.ToByteArray<CacheItem>(cacheItem), new DistributedCacheEntryOptions { AbsoluteExpiration = System.DateTimeOffset.UtcNow.AddMinutes(minutes) });
                                break;
                            case CacheTypes.Sliding:
                                cacheProvider.Set(key, cacheKeyProvider.ToByteArray<CacheItem>(cacheItem), new DistributedCacheEntryOptions { SlidingExpiration = System.TimeSpan.FromMinutes(minutes) });
                                break;
                            default:
                                break;
                        }


                    }


                }

                await memStream.CopyToAsync(originalBodyStream);

            }
        }

        #endregion Public Methods
    }
}