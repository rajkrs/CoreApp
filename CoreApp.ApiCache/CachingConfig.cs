using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.ApiCache
{

    public interface ICachingConfig {

    }
    public class SqlCachingConfig : ICachingConfig
    {
        //dotnet sql-cache create "Data Source=(local)\\SQL2016;Initial Catalog=CoreAppCache;Integrated Security=True;" dbo TestCache

        public string ConnectionString { get; set; } = "Data Source=(local)\\SQL2016;Initial Catalog=CoreAppCache;Integrated Security=True;";
        public string Schema { get; set; } = "dbo";
        public string TableName { get; set; } = "TestCache";
    }
    public class CustomCachingConfig : ICachingConfig
    {
        public string ServiceUrl { get; set; } = "http://localhost:9875/api/";

    }
    public class RedisCachingConfig : ICachingConfig
    {
        public string Configuration { get; set; } = "localhost";
        public string InstanceName { get; set; } = "SampleInstance";

    }



}
