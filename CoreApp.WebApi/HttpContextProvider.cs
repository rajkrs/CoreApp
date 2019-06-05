namespace CoreApp.WebApi
{
    public class HttpContextProvider
    {
        private static Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor;
        public static void Configure(Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public static Microsoft.AspNetCore.Http.HttpContext Current
        {
            get
            {
                return _httpContextAccessor.HttpContext;
            }
        }
    }
}