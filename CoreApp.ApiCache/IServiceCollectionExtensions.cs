using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;

namespace CoreApp.ApiCache
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreAppDistributedCaching(this IServiceCollection services, ICachingConfig cachingConfig )
        {
            services.AddHttpContextAccessor();
            services.AddTransient<ICacheKeyProvider, CacheKeyProvider>();

            switch (cachingConfig)
            {
                case SqlCachingConfig sqlCachingConfig:
                    {
                        services.AddDistributedSqlServerCache(options =>
                        {
                            options.ConnectionString = sqlCachingConfig.ConnectionString;
                            options.SchemaName = sqlCachingConfig.Schema;
                            options.TableName = sqlCachingConfig.TableName;
                        });
                    }
                    break;
                case RedisCachingConfig redisCachingConfig:
                    {
                        services.AddDistributedRedisCache(options =>
                        {
                            options.Configuration = redisCachingConfig.Configuration;
                            options.InstanceName = redisCachingConfig.InstanceName;
                        });
                    }
                    break;

                case CustomCachingConfig customCachingConfig:
                    {
                        services.AddSingleton<IDistributedCache, CustomDistributedCache>();
                    }
                    break;
                    

                default:
                    break;
            }

            return services;
        }
    }
}
