using E_Commerce.Services_Abstraction.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Attributes
{
    internal class RedisCacheAttribute : ActionFilterAttribute
    {
        private readonly int _durationInMin;

        public RedisCacheAttribute(int durationInMin = 5)
        {
            _durationInMin = durationInMin;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Key => Request | Value => Response Data 
            // Get Cache Service from DI Container
            var CacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var CacheKey = CreateCacheKey(context.HttpContext.Request);

            // Check if Cache Data Exist
            var CachedData = await CacheService.GetAsync(CacheKey);

            // if Exists, Return Cached Data and skip Executing the Endpoint
            if (CachedData is not null)
            {
                context.Result = new ContentResult
                {
                    Content = CachedData,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }

            // if not Exists, Execute the Endpoint and Store the result Data in Cache if 200 OK Response
            var ExecutedContext = await next.Invoke();
            if (ExecutedContext.Result is OkObjectResult result)
            {
                await CacheService.SetAsync(CacheKey, result.Value!, TimeSpan.FromMinutes(_durationInMin));
            }
        }

        // /api/Products
        // /api/Products?TypeID=1
        // /api/Products?BrandID=2
        // /api/Products?TypeID=1&BrandID=2
        // /api/Products?BrandID=2&TypeID=1
        private string CreateCacheKey(HttpRequest request)
        {
            var KeyBuilder = new StringBuilder();
            KeyBuilder.Append($"{request.Path}"); // /api/Products
            foreach (var (Key, Value) in request.Query.OrderBy(x => x.Key))// /api/Products|TypeID-1|BrandID-2
            {
                KeyBuilder.Append($"|{Key}-{Value}");
            }
            return KeyBuilder.ToString();
        }
    }
}
