using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FunctionAppContagem
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices((services) =>
                {
                    services.AddSingleton<Contador>();
                })
                .Build();

            host.Run();
        }
    }
}