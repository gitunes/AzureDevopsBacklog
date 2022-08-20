using Newtonsoft.Json;

namespace AzureDevopsBacklog.Application.Models
{
    public class WorkItemListModel
    {
        [JsonProperty("queryType")]
        public string QueryType { get; set; }

        [JsonProperty("queryResultType")]
        public string QueryResultType { get; set; }

        [JsonProperty("asOf")]
        public string QueryCallDate { get; set; }

        //[JsonProperty("columns")]
        //public List<ColumnModel> Columns { get; set; } = new();

        [JsonProperty("workItems")]
        public List<WorkItemModel> WorkItems { get; set; } = new();
    }
}
