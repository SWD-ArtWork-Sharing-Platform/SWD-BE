using gate.Extension;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.AppAuthentication();
            if (builder.Environment.EnvironmentName.ToString().ToLower().Equals("production"))
            {
                builder.Configuration.AddJsonFile("ocelot.Production.json", optional: false, reloadOnChange: true);
            }
            else
            {
                builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
            }
            builder.Services.AddOcelot(builder.Configuration);


            var app = builder.Build();
            app.MapGet("/", () => "Hello World!");
            app.UseOcelot().GetAwaiter().GetResult();
            app.Run();
        }
    }
}
