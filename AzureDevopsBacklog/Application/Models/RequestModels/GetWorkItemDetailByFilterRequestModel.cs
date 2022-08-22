using Newtonsoft.Json;

namespace AzureDevopsBacklog.Application.Models.RequestModels
{
    public class GetWorkItemDetailByFilterRequestModel
    {
        public List<string>? Tags { get; set; }
        public string? State { get; set; }
        public bool? IsHold { get; set; }
        public CreateDate? CreateDate { get; set; } = new();
    }

    public class CreateDate
    {
        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }
    }
}
