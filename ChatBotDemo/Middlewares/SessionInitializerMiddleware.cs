using ChatBotDemo.Models.Helpers;
using Microsoft.Extensions.Caching.Distributed;

namespace ChatBotDemo.Middlewares
{
    public class SessionInitializerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDistributedCache _cache;
        private const string cacheKeysStr = "CacheKeys";
        public SessionInitializerMiddleware(RequestDelegate next, IDistributedCache cache)
        {
            _next = next;
            _cache = cache;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            if (string.IsNullOrEmpty(context.Session.GetString(SessionConstants.SESSION_ID)))
            {
                context.Session.SetString(SessionConstants.SESSION_ID, context.Session.Id);
            }
            await _next(context); // Continue to the next middleware/request handler
        }
    }
}
