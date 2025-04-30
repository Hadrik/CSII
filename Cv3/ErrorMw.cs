namespace Cv3;

public class ErrorMw(RequestDelegate next)
{
    public async Task Invoke(HttpContext ctx, ExceptionHandler handler)
    {
        try
        {
            await next(ctx);
        }
        catch (Exception e)
        {
            handler.Handle(e);
            
            ctx.Response.StatusCode = 500;
            await ctx.Response.WriteAsync("Internal server error");
        }
    }
}