using AzureDevopsBacklog.Infrastructure.Interfaces;

namespace AzureDevopsBacklog.Infrastructure.Configuration
{
    public sealed record SmtpClientSettings : ISmtpClientSettings
    {
        public string Host { get; init; }
        public int Port { get; init; }
        public string FromEmail { get; init; }
        public string UserName { get; init; }
        public string Password { get; init; }
    }
}
