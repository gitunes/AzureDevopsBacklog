using AzureDevopsBacklog.Infrastructure.Interfaces;

namespace AzureDevopsBacklog.Infrastructure.Configuration
{
    public sealed record class AzureApiSettings : IAzureApiSettings
    {
        public string BaseUrl { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
    }
}
