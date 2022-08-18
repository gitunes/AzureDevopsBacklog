using Newtonsoft.Json;

namespace AzureDevopsBacklog.Application.Models
{
    public class WorkItemDetailResponseModel
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("value")]
        public List<WorkItemDetailModel> WorkItemDetails { get; set; } = new();
    }
}
