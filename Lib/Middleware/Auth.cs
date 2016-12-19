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

            if (isAuthorized)
            {
                await _next(httpContext);
                return;
            }

            httpContext.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
            await httpContext.Response.WriteAsync("");
        }
    }
}