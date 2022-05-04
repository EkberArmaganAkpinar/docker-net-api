using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<RequestMiddleware> logger;
        public RequestMiddleware(RequestDelegate Next, ILogger<RequestMiddleware> Logger)
        {
            next = Next;
            logger = Logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            logger.LogInformation($"Query Keys:{httpContext.Request.Path}");
            await next.Invoke(httpContext);
        }
    }
}
