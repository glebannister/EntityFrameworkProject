using GlobalMarket.Core.Exceptions;

namespace GlobalMarket.API.Middleware
{
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (NotFoundException ex)
            {
                await Results.NotFound(ex.Message).ExecuteAsync(context);
            }
            catch (ConflictException ex)
            {
                await Results.Conflict(ex.Message).ExecuteAsync(context);
            }
            catch (UnauthorizedException)
            {
                await Results.Unauthorized().ExecuteAsync(context);
            }
        }
    }
}
