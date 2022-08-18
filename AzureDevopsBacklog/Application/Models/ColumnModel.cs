using Newtonsoft.Json;

namespace AzureDevopsBacklog.Application.Models
{
    public class ColumnModel
    {
        [JsonProperty("referenceName")]
        public string ReferenceName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
