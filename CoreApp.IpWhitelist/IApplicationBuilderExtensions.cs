using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.IpWhitelist
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseIpWhitelist(
            this IApplicationBuilder builder, string ipSafeList)
        {
            return builder.UseMiddleware<IpWhitelistMiddleware>(ipSafeList);
        }

        public static IApplicationBuilder UseIpWhitelist(
            this IApplicationBuilder builder, List<string> ipSafeList)
        {
            return builder.UseMiddleware<IpWhitelistMiddleware>(ipSafeList);
        }


    }
}
