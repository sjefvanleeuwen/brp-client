using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
namespace HaalCentraal.BrpBevragen.Provider
{
    public static class BrpClientBuilder
    {
        public static void AddBrpClient(this IServiceCollection services, Action<BrpClientOptions> configure)
        {
            BrpClientOptions options = new BrpClientOptions();
            configure(options);

           services.AddHttpClient<IBrpClient, BrpClient>(client => {
               client.BaseAddress = new Uri(options.ApiUrl);
               client.DefaultRequestHeaders.Add("x-api-key", options.ApiKey);
           });
        }
    }
}
