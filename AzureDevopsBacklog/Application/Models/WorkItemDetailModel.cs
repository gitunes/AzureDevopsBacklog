using Newtonsoft.Json;

namespace AzureDevopsBacklog.Application.Models
{
    public class WorkItemDetailModel : WorkItemModel
    {
        [JsonProperty("rev")]
        public int Rev { get; set; }

        [JsonProperty("fields")]
        public FieldModel Fields { get; set; } = new();
    }
}
