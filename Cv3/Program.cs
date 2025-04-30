namespace Cv3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<ExceptionHandler>();
            builder.Services.AddScoped<IMyLogger, JsonLogger>();
            
            var app = builder.Build();

            app.UseMiddleware<BrowserAuthMw>();
            app.UseMiddleware<FormMw>();
            app.UseMiddleware<FilesMw>();
            app.UseMiddleware<FirstPageMw>();

            app.Run();
        }
    }
}
