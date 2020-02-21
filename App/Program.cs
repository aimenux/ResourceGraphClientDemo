using System;
using System.IO;
using App.Builders;
using App.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;

namespace App
{
    public static class Program
    {
        public static void Main()
        {
            var environment = Environment.GetEnvironmentVariable("ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var services = new ServiceCollection();
            services.Configure<Settings>(configuration.GetSection(nameof(Settings)));
            services.AddSingleton<IQueryRequestBuilder, QueryRequestBuilder>();
            services.AddSingleton<IResourceGraphProvider>(provider =>
            {
                var configSettings = provider.GetService<IOptions<Settings>>().Value;
                var credentials = GetServiceClientCredentials(configSettings);
                return new ResourceGraphProvider(credentials);
            });

            var serviceProvider = services.BuildServiceProvider();

            using (var resourceGraphProvider = serviceProvider.GetService<IResourceGraphProvider>())
            {
                var request = serviceProvider.GetService<IQueryRequestBuilder>().Build();
                var response = resourceGraphProvider.QueryResources(request);
                Console.WriteLine($"{nameof(response.Data)}:\n {response.Data}\n");
            }

            Console.WriteLine("Press any key to exit !");
            Console.ReadKey();
        }

        private static ServiceClientCredentials GetServiceClientCredentials(Settings settings)
        {
            var authenticationContext = new AuthenticationContext(settings.AuthorityUrl);
            var clientCredentials = new ClientCredential(settings.ClientId, settings.ClientSecret);
            var authenticationResult = authenticationContext
                .AcquireTokenAsync(settings.ResourceUrl, clientCredentials)
                .GetAwaiter()
                .GetResult();
            return new TokenCredentials(authenticationResult.AccessToken);
        }
    }
}
