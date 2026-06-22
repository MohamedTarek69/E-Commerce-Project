using E_Commerce.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace E_Commerce.Web.CustomMiddleWares
{
    public class ExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlerMiddleWare> logger;

        public ExceptionHandlerMiddleWare(RequestDelegate Next, ILogger<ExceptionHandlerMiddleWare> logger)
        {
            next=Next;
            this.logger=logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);

                await HandleNotFoundEndPointAsync(context);
            }
            catch (Exception ex)
            {
                // Logging
                // Console.WriteLine(ex.Message);
                logger.LogError(ex, "An unhandled exception occurred.");

                // return Custom Error Response
                var Problem = new ProblemDetails
                {
                    Title =  ex switch
                    {
                        NotFoundException => "Resource not found!",
                        _ => "An unexpected error occurred!"
                    },
                    Detail = ex.Message,
                    Instance = context.Request.Path,
                    Status = ex switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        _ => StatusCodes.Status500InternalServerError
                    }

                };

                context.Response.StatusCode = Problem.Status.Value;
                await context.Response.WriteAsJsonAsync(Problem);

            }
        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext context)
        {
            if (context.Response.StatusCode==StatusCodes.Status404NotFound && !context.Response.HasStarted)
            {
                var Problem = new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "The resource you are looking for is not found!",
                    Detail = $"The requested URL {context.Request.Path} was not found on this server.",
                    Instance = context.Request.Path
                };
                await context.Response.WriteAsJsonAsync(Problem);
            }
        }
    }
}
