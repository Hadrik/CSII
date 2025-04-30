namespace Cv3;

public class FirstPageMw(RequestDelegate next)
{
    public async Task Invoke(HttpContext ctx)
    {
        if (ctx.Request.Path.StartsWithSegments("/error"))
        {
            throw new NotImplementedException();
        }
        
        ctx.Response.Headers.Add("Content-Type", "text/html; charset=utf-8");
        
        await ctx.Response.WriteAsync($@"
            <html>
                <head>
                    <title>First page</title>
                </head>
                <body>
                    <h1>First page</h1>
                    <p>{ctx.Request.Path}</p>
                </body>
            </html>
        ");
    }
}