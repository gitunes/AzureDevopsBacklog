using System.Text.Json.Serialization;

namespace AzureDevopsBacklog.Application.Models
{
    public class ColumnModel
    {
        [JsonPropertyName("referenceName")]
        public string ReferenceName { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
