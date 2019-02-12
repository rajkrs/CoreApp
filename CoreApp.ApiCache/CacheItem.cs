using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CoreApp.ApiCache
{
    [Serializable()]
    public class CacheItem
    {
        public Dictionary<string,string> Headers { get; set; }
        public byte[] Body { get; set; }
        public string ContentType { get; set; }

        public DateTime LastModifiedOn { get; set; }
    }
}
