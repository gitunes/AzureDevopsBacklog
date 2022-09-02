using System.Text.Json.Serialization;

namespace AzureDevopsBacklog.Application.Models
{
    public class WorkItemModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
