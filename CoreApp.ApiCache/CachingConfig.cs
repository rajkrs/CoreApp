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
        /*
         * CREATE TABLE [dbo].[TestCache](
    [Id] [nvarchar](449) NOT NULL,
    [Value] [varbinary](max) NOT NULL,
    [ExpiresAtTime] [datetimeoffset](7) NOT NULL,
    [SlidingExpirationInSeconds] [bigint] NULL,
    [AbsoluteExpiration] [datetimeoffset](7) NULL,
 CONSTRAINT [pk_Id] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
       IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, 
       ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
 
CREATE NONCLUSTERED INDEX [Index_ExpiresAtTime] ON [dbo].[TestCache]
(
    [ExpiresAtTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
       SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, 
       ONLINE = OFF, ALLOW_ROW_LOCKS = ON, 
       ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

 
         * */

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
