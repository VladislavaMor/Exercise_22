using Exercise_21.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Exercise_21
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var init = BuildWebHost(args);
            await DbInitializer.Initialize(init);
            init.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>().Build();
    }
}
