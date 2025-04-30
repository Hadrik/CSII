namespace Cv3;

public class FilesMw(RequestDelegate next)
{
    public async Task Invoke(HttpContext ctx, IMyLogger logger)
    {
        string path = ctx.Request.Path;
        const string dir = "./files";
        
        var finalPath = Path.Combine(dir, path.TrimStart('/'));;

        if (File.Exists(finalPath))
        {
            logger.Log($"File {finalPath} was accessed.");
            ctx.Response.ContentType = "image/png";
            await ctx.Response.SendFileAsync(finalPath);
        }
        
        await next(ctx); 
    }
}