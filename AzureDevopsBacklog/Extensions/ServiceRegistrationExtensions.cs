using AzureDevopsBacklog.Infrastructure.Configuration;
using AzureDevopsBacklog.Infrastructure.Interfaces;
using AzureDevopsBacklog.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace AzureDevopsBacklog.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.RegisterAzureApiSettings();
            services.AddSingleton<IRestService, RestService>();
            services.AddSingleton<IWorkItemService, WorkItemService>();
        }

        public static void RegisterAzureApiSettings(this IServiceCollection services)
        {
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            ArgumentNullException.ThrowIfNull(serviceProvider, nameof(serviceProvider));

            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
            services.Configure<AzureApiSettings>(configuration.GetRequiredSection(nameof(AzureApiSettings)));
            services.TryAddSingleton<IAzureApiSettings>(provider => provider.GetRequiredService<IOptions<AzureApiSettings>>().Value);
        }
    }
}
