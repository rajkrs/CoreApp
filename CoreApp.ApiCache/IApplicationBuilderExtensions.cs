using Microsoft.AspNetCore.Builder;


namespace CoreApp.ApiCache
{
    public static class IApplicationBuilderExtensions
    {
        

        public static IApplicationBuilder UseCoreAppDistributedCaching(
            this IApplicationBuilder app)
        {
            return app.UseMiddleware<ICacheMiddleware>();
        }


    }
}
