using Newtonsoft.Json;

namespace AzureDevopsBacklog.Application.Models
{
    public class BaseModel
    {
        [JsonProperty("id")]
        public object Id { get; set; }
    }
}
