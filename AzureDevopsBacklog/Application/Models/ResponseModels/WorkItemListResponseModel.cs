using System.Text.Json.Serialization;

namespace AzureDevopsBacklog.Application.Models.ResponseModels
{
    public class WorkItemListResponseModel
    {
        [JsonPropertyName("queryType")]
        public string QueryType { get; set; }

        [JsonPropertyName("queryResultType")]
        public string QueryResultType { get; set; }

        [JsonPropertyName("asOf")]
        public string QueryCallDate { get; set; }

        //[JsonPropertyName("columns")]
        //public List<ColumnModel> Columns { get; set; } = new();

        [JsonPropertyName("workItems")]
        public List<WorkItemModel> WorkItems { get; set; } = new();
    }
}
