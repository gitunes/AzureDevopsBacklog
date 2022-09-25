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
            services.RegisterSmtpClientSettings();
            services.AddSingleton<IRestService, RestService>();
            services.AddSingleton<IWorkItemService, WorkItemService>();
            services.AddSingleton<ISprintService, SprintService>();
            services.AddSingleton<IReportService, ReportService>();
        }

        public static void RegisterAzureApiSettings(this IServiceCollection services)
        {
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            ArgumentNullException.ThrowIfNull(serviceProvider, nameof(serviceProvider));

            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
            services.Configure<AzureApiSettings>(configuration.GetRequiredSection(nameof(AzureApiSettings)));
            services.TryAddSingleton<IAzureApiSettings>(provider => provider.GetRequiredService<IOptions<AzureApiSettings>>().Value);
        }
        public static void RegisterSmtpClientSettings(this IServiceCollection services)
        {
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            ArgumentNullException.ThrowIfNull(serviceProvider, nameof(serviceProvider));

            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
            services.Configure<SmtpClientSettings>(configuration.GetRequiredSection(nameof(SmtpClientSettings)));
            services.TryAddSingleton<ISmtpClientSettings>(provider => provider.GetRequiredService<IOptions<SmtpClientSettings>>().Value);
        }
    }
}
