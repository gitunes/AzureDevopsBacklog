using Newtonsoft.Json;

namespace AzureDevopsBacklog.Application.Models
{
    public class WorkItemModel : BaseModel
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
