using Bullhorn.CommandLine.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Bullhorn.CommandLine
{
    public static class Program
    {
        public static async Task Main()
        {
            var services = new ServiceCollection();

            var startup = new Startup();

            startup.ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetService<PlayGroundService>();

            await service.TestApiAsync();
        }
    }
}
