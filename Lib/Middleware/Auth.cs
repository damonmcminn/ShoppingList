using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace ShoppingListApi.Lib.Middleware
{
    public class Auth
    {
        private readonly RequestDelegate _next;

        private readonly string _authHeader;
        private readonly string _authKey;

        public Auth(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _authHeader = appSettings.Value.AuthHeader;
            _authKey = Environment.GetEnvironmentVariable(appSettings.Value.AuthKeyVariableName);
        }

        public async Task Invoke(HttpContext httpContext)
        {
            StringValues key;
            httpContext.Request.Headers.TryGetValue(_authHeader, out key);
            var isAuthorized = key == _authKey;

            await (isAuthorized ? _next(httpContext) : RespondUnauthorized(httpContext));
        }

        private static Task RespondUnauthorized(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = (int) HttpStatusCode.Unauthorized;

            return httpContext.Response.WriteAsync("");
        }
    }
}