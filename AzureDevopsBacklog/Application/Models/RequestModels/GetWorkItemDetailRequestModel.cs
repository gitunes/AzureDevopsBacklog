using System.Text.Json.Serialization;

namespace AzureDevopsBacklog.Application.Models.RequestModels
{
    public class GetWorkItemDetailRequestModel
    {
        public string Query { get; set; }
    }
}
