using Microsoft.AspNetCore.Builder;

namespace ShoppingListApi.Lib.Middleware
{
    public static class AuthExtensions
    {
        public static IApplicationBuilder UseAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Auth>();
        }
    }
}