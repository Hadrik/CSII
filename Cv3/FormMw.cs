namespace Cv3;

public class FormMw(RequestDelegate next)
{
    public async Task Invoke(HttpContext ctx)
    {
        if (!ctx.Request.Path.StartsWithSegments("/form"))
        {
            await next(ctx);
            return;
        }

        if (ctx.Request.Method == "POST")
        {
            if (ctx.Request.Form.TryGetValue("name", out var vals))
            {
                var name = vals.First();
                await ctx.Response.WriteAsync(name);
                return;
            }
        }
        
        ctx.Response.Headers.Add("Content-Type", "text/html; charset=utf-8");
        await ctx.Response.WriteAsync(@"
            <html>
                <head>
                    <title>Form</title>
                </head>
                <body>
                    <h1>Form</h1>
                    <form method='post'>
                        <input type='text' name='name' placeholder='Name'>
                        <input type='submit' value='Submit'>
                    </form>
                </body>
            </html>
        ");
    }
}