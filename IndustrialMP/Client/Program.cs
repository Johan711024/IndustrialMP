using IndustrialMP.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace IndustrialMP.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

           // builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://industrialmpapi.azurewebsites.net") });

            builder.Services.AddHttpClient<IManagementCentralClient, ManagementCentralClient>(client => client.BaseAddress = new Uri("https://industrialmpapi.azurewebsites.net")  );

            await builder.Build().RunAsync();
        }
    }
}