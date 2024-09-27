using Exercise_21.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
namespace Exercise_21
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var init = BuildWebHost(args);
            using (var scope = init.Services.CreateScope())
            {
                var s = scope.ServiceProvider;
                var c = s.GetRequiredService<PhoneBookContext>();
                DbInitializer.Initialize(c);
            }
            init.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>().Build();
    }
}
