namespace AzureDevopsBacklog.Application.Models.ResponseModels
{
    public class SprintDetailResponseModel
    {
        public int Count { get; set; }
        public List<UserSprintDetail> Users { get; set; } = new();
    }
}
