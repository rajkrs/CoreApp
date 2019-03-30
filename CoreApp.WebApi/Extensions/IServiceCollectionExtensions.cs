using AutoMapper;
using AutoMapper.Mappers;
using CoreApp.Account.Provider;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace CoreApp
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreAppMapper(this IServiceCollection services)
        {
            Mapper.Initialize(x =>
            {
                x.AddConditionalObjectMapper().Where((s, d) => s.Name == d.Name + "ViewModel");
                x.AddConditionalObjectMapper().Where((s, d) => s.Name + "ViewModel" == d.Name);
                x.AddProfile<AccountMapperProfile>();
            });
            services.AddAutoMapper();
            return services;
        }
 
    }
}
