namespace Cv3;

public class BrowserAuthMw(RequestDelegate next)
{
    public async Task Invoke(HttpContext ctx)
    {
        var ua = ctx.Request.Headers.UserAgent.First();
        
        if (ua.Contains("Chrome") && !ua.Contains("Edge"))
        {
            await next(ctx);
        }
        else
        {
            ctx.Response.StatusCode = 403;
            await ctx.Response.WriteAsync("Forbidden");
        }
    }
}