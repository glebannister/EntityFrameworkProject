namespace GlobalMarket.API.Middleware
{
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            //try
            //{
            //    await next(context);
            //} catch (Exception ex)
            //{
            //    await Results.NotFound(ex.Message).ExecuteAsync(context);
            //}
        }
    }
}
