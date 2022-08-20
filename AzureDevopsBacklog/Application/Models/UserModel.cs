using Newtonsoft.Json;

namespace AzureDevopsBacklog.Application.Models
{
    public class UserModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("uniqueName")]
        public string UniqueName { get; set; }
    }
}
