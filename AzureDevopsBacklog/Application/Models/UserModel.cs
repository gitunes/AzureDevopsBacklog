using System.Text.Json.Serialization;

namespace AzureDevopsBacklog.Application.Models
{
    public class UserModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("uniqueName")]
        public string UniqueName { get; set; }
    }
}
