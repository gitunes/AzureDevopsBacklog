using System.Text.Json.Serialization;

namespace AzureDevopsBacklog.Application.Models
{
    public class WorkItemDetailModel : WorkItemModel
    {
        [JsonPropertyName("rev")]
        public int Rev { get; set; }

        [JsonPropertyName("fields")]
        public FieldModel Fields { get; set; } = new();
    }
}
