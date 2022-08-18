using Newtonsoft.Json;

namespace AzureDevopsBacklog.Application.Models
{
    public class UserModel : BaseModel
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("uniqueName")]
        public string UniqueName { get; set; }
    }
}
