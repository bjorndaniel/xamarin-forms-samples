using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.Data;
using TodoAPI.Interfaces;

namespace TodoAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var seed = args.Any(x => x == "/seed");
            if (seed)
            {
                args = args.Except(new[] { "/seed" }).ToArray();
            }

            var host = BuildWebHost(args);
            using (var scope = host.Services.CreateScope())
            {
                var config = scope.ServiceProvider.GetService<IConfiguration>();
                if (seed)
                {
                    var context = scope.ServiceProvider.GetService<TodoAPIContext>();
                    if(context != null)
                    {
                        await context.Database.MigrateAsync(); 
                    }
                    var repository = scope.ServiceProvider.GetService<ITodoRepository>();
                    if(repository != null)
                    {
                        await repository.InitializeDataAsync();
                    }
                }
            }
            await host.RunAsync();
        }

        public static IWebHost BuildWebHost(string[] args) =>
             WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>().Build();
    }
}
