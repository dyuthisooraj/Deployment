using HalcyonApparelsMVC.Interfaces;

namespace HalcyonApparelsMVC.Services
{
    public class SalesforceMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAuthenticate _authenticate;
        public SalesforceMiddleware(RequestDelegate next, IAuthenticate authenticate)
        {
            _next = next;
            _authenticate = authenticate;
        }

        public Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Session.GetString("Acces_token") == null)
            {
                var tempAcessToken = _authenticate.Authenticate();
                httpContext.Session.SetString("Acces_token", tempAcessToken);
            }


            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class SalesforceMiddlewareExtensions
    {
        public static IApplicationBuilder UseSalesforceMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SalesforceMiddleware>();
        }
    }
}
