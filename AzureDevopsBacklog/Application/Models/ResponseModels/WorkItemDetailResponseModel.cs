using System.Text.Json.Serialization;

namespace AzureDevopsBacklog.Application.Models.ResponseModels
{
    public class WorkItemDetailResponseModel
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("value")]
        public List<WorkItemDetailModel> WorkItemDetails { get; set; } = new();
    }
}
