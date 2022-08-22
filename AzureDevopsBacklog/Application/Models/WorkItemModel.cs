using Newtonsoft.Json;

namespace AzureDevopsBacklog.Application.Models
{
    public class WorkItemModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
