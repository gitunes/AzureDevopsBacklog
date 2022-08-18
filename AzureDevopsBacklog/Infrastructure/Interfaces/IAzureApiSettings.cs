namespace AzureDevopsBacklog.Infrastructure.Interfaces
{
    public interface IAzureApiSettings
    {
        public string BaseUrl { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
    }
}
